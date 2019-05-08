//-------------------------------------
//Loader.cs
//Reads an ELF file and then laods the program segments into simulated RAM.
//-------------------------------------
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
namespace armsim
{

    // A struct that mimics memory layout of ELF file header
    // See http://www.sco.com/developers/gabi/latest/contents.html for details
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ELF
    {
        public byte EI_MAG0, EI_MAG1, EI_MAG2, EI_MAG3, EI_CLASS, EI_DATA, EI_VERSION;
        byte unused1, unused2, unused3, unused4, unused5, unused6, unused7, unused8, unused9;
        public ushort e_type;
        public ushort e_machine;
        public uint e_version;
        public uint e_entry;
        public uint e_phoff;
        public uint e_shoff;
        public uint e_flags;
        public ushort e_ehsize;
        public ushort e_phentsize;
        public ushort e_phnum;
        public ushort e_shentsize;
        public ushort e_shnum;
        public ushort e_shstrndx;
    }
    
    //A struct that mimics a Program Header Entry.
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ProgHeaderEnt
    {
        public uint p_type;
        public uint p_offset;
        public uint p_vaddr;
        public uint p_paddr;
        public uint p_filesz;
        public uint p_memsz;
        public uint p_flags;
        public uint p_align;
    }
}
namespace armsim { 
    

    //Does the work of reading an ELF file and then loading it into simulated ram.
    static class Loader
    {
        public static bool logg;
        //opens an ELF file, <filename>, and then begins reading it. Will load program segments into <ram>, a simulated memory array.
        //<test> will silence output to the log if running unit tests. <log> controls logging features.
        public static void Read(string filename, Memory ram, bool test, bool log,Memory reg)
        { 
            string elfFilename = filename;
            using (FileStream strm = new FileStream(elfFilename, FileMode.Open))
            {
                ELF elfHeader = new ELF();
                byte[] data = new byte[Marshal.SizeOf(elfHeader)];
                //ram.log.Text += "\r\nOpening " + elfFilename + "...";
                // Read ELF header data
                strm.Read(data, 0, data.Length);
                // Convert to struct
                elfHeader = ByteArrayToStructure<ELF>(data);
              
                
                Log("Loader.Read: Entry point: " + elfHeader.e_entry.ToString("X4"), test,log);
                
                reg.setRegister(15, 0x0000);
                
                Log("Loader.Read: Number of segments: " + elfHeader.e_phnum,test,log);
                
                // Read first program header entry
                int index = 1;
                strm.Seek(elfHeader.e_phoff, SeekOrigin.Begin);
                data = new byte[elfHeader.e_phentsize];
                strm.Read(data, 0, (int)elfHeader.e_phentsize);               
                //read additional program header entries
                ProgHeaderEnt phead = ByteArrayToStructure<ProgHeaderEnt>(data);               
                uint size = elfHeader.e_phoff + elfHeader.e_phentsize;
                uint size2 = phead.p_offset + phead.p_filesz;
                try
                {
                    byte[] segment = new byte[phead.p_filesz];
                    strm.Seek(phead.p_offset, SeekOrigin.Begin);
                    strm.Read(segment, 0, (int)phead.p_filesz);
                    uint addr = phead.p_vaddr;
                    uint end;
                    ram.begin = addr;
                    ram.begin2 = (uint)elfHeader.e_entry;
                    end = writeBytes(ram, segment, addr);
                    
                    Log("Loader.Read: Segment 1 - Address = " + phead.p_vaddr + ", Offset = " + phead.p_offset + ", Size = " + phead.p_filesz,test,log);                   
                    while (index < elfHeader.e_phnum)
                    {
                        strm.Seek(size, SeekOrigin.Begin);
                        data = new byte[elfHeader.e_phentsize];
                        strm.Read(data, 0, (int)elfHeader.e_phentsize);
                        phead = ByteArrayToStructure<ProgHeaderEnt>(data);
                        size = size + elfHeader.e_phentsize;
                        size2 = phead.p_offset + phead.p_filesz;
                        segment = new byte[phead.p_filesz];
                        strm.Seek(phead.p_offset, SeekOrigin.Begin);
                        strm.Read(segment, 0, (int)phead.p_filesz);
                        addr = phead.p_vaddr;
                        end = writeBytes(ram, segment, addr);
                        Log("Loader.Read: Segment " + (index + 1) + " - Address = " + phead.p_vaddr + ", Offset = " + phead.p_offset + ", Size = " + phead.p_filesz,test,log);                       
                        index++;
                    };

                    ram.end = end;
                  //  ram.log.Text += "\r\nCalculating checksum...";
                    int y = ram.Checksum();
                    if (ram.ReadWord(0) == 0)
                    {
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 0, true);
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 1, true);
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 2, true);
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 3, true);
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 4, true);
                        reg.setRegister(15, elfHeader.e_entry);
                        reg.setRegister(13, 0x7000);
                        reg.modetrack = 0;
                    }
                    else
                    {
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 0, true);
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 1, true);
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 2, false);
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 3, false);
                        reg.cpsr = Memory.SetBitinInt(reg.cpsr, 4, true);
                        reg.modetrack = 1;
                    }
                  //  ram.log.Text += "\r\nChecksum value: " + y;
                }
                catch (IndexOutOfRangeException e)
                {
                    Trace.WriteLine("RAM Overflowed. Not enough RAM was specified. Quitting.");
                    MessageBox.Show("RAM Overflowed. Not enough RAM was specified. Quitting.");
                    Environment.Exit(0);
                }
            }
        }

        // Converts a byte array to a struct
        static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(),
                typeof(T));
            handle.Free();
            return stuff;
        }
        //test if <file> is an ELF file by reading its magic numbers.
        static void isElfFile(ELF file)
        {
            if (!(file.EI_MAG0==127 && file.EI_MAG1 ==69 && file.EI_MAG2==76 && file.EI_MAG3 == 70))
            {
                MessageBox.Show("The file you chose is not in ELF format. The program will now quit.");
                Environment.Exit(0);
            }
        }

        //writes <message> to the text box <text> on the parent form. 
        public static void Log(string message, bool test,bool log)
        {
            if(logg)
            Trace.WriteLine(message);
            //STrace.TraceInformation(message);
            Trace.Flush();
            
            
        }

        //writes a byte array, <bytes> to the position in <ram> specified by <addr>
        static uint writeBytes(Memory ram, byte[] bytes, uint addr)
        {
            foreach (byte b in bytes)
            {
                ram.WriteByte(addr, b);
                addr++;
            }
            return addr;
        }
    }
}