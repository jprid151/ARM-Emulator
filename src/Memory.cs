
//-----------------------------------------------------
//RAM.cs Contains code for a simulated RAM array and 
//methods to read words etc. from RAM
//----------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace armsim
{
      
    //contains a simulated RAM using a byte array and methods for reading/writing to RAM.
    class Memory
    {
        //shows end of RAM
        public uint end;
        //shows beginning of RAM
        public uint begin;
        //simulation of RAM array
        byte[] ram;
        public int bsize;
        bool sys;
        private uint r0, r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15;
        public uint cpsr;

        //svc mode banked regs
        private uint SP_svc, LR_svc;
        //irq mode sp regs
        private uint SP_irq, LR_irq;
        //banked cpsr regs
        private uint SPSR_svc, SPSR_irq;
        //tracks current input mode.
        public int modetrack;
        //tracks beginning of executables with no OS
        public uint begin2;
        //constructor for RAM class
        public Memory(int size, TextBox box)
        {
            ram = new byte[size];
            bsize = size;
            cpsr = 0;
            SP_irq = 0x7ff0;
            SP_svc = 0x78f0;
            SetBitinInt(cpsr, 0, true);
            SetBitinInt(cpsr, 1, false);
            SetBitinInt(cpsr, 2, false);
            SetBitinInt(cpsr, 3, true);
            SetBitinInt(cpsr, 4, true);
        }
        
        //returns a register specified by <index>
        public uint getRegister(int index)
        {
            switch (index)
            {
                case 0:
                    return r0;
                case 1:
                    return r1;
                case 2:
                    return r2;
                case 3:
                    return r3;
                case 4:
                    return r4;
                case 5:
                    return r5;
                case 6:
                    return r6;
                case 7:
                    return r7;
                case 8:
                    return r8;
                case 9:
                    return r9;
                case 10:
                    return r10;
                case 11:
                    return r11;
                case 12:
                    return r12;
                case 13:
                    if (modetrack == 0)
                        return r13;
                    else if (modetrack == 1)
                        return SP_svc;
                    else
                        return SP_irq;
                case 14:
                    if (modetrack == 0)
                        return r14;
                    else if (modetrack == 1)
                        return LR_svc;
                    else
                        return LR_irq;
                case 15:
                    return r15+4;
                default:
                    return 0;
            }
        }
        public uint getPC()
        {
            return r15;
        }
        //sets the register specified by <index> to <value>
        public void setRegister(int index,uint value)
        {
            switch (index)
            {
                case 0:
                     r0=value;
                     return;
                case 1:
                     r1=value;
                     return;
                case 2:
                     r2 = value;
                     return;
                case 3:
                    r3 = value;
                    return;
                case 4:
                    r4 = value;
                    return;
                case 5:
                    r5 = value;
                    return;
                case 6:
                    r6 = value;
                    return;
                case 7:
                    r7 = value;
                    return;
                case 8:
                    r8 = value;
                    return;
                case 9:
                    r9 = value;
                    return;
                case 10:
                    r10 = value;
                    return;
                case 11:
                    r11 = value;
                    return; ;
                case 12:
                    r12 = value;
                    return;
                case 13:
                    if (modetrack == 0)
                        r13 = value;
                    else if (modetrack == 1)
                    {
                        SP_svc = value;
                    }
                    else
                    {
                        SP_irq = value;
                    }
                    return;
                case 14:
                    if (modetrack==0)
                    r14 = value;
                    else if(modetrack==1)
                    {
                        LR_svc = value;
                    }
                    else
                    {
                        LR_irq = value;
                    }
                    return;
                case 15:
                    r15 = value;
                    return;
                default:
                    return;
            }
        }
        //returns a 32 bit unsigned word (int) from the RAM array, specified by <addr>. Will not accept an address that is not divisble by 4.
        public uint ReadWord(uint addr)
        {
            if (addr % 4 != 0) { return 2111111111; }
            if (!BoundsCheck(addr)){ return 2111111111; }
            return (uint)  (ram[addr+3] << 24 | ram[addr+2] << 16 | ram[addr+1] << 8 | ram[addr]);
        }
    
        //will write a 32 bit word, <val> at the address, <addr> specified by RAM> Will not accept an address that is not divisble by 4.
        public void WriteWord(uint addr, uint val) { if (addr % 4 != 0) { return; }
            if (addr % 4 != 0) { return; }
            if (!BoundsCheck(addr))
            {                return;}
            int index = 0;
            byte b1 = 0;
            int index2 = 0;
            uint bitindex = 0;
            while (index < 32)
            {
                //code snippet from https://stackoverflow.com/questions/4854207/get-a-specific-bit-from-byte
                var z1 = (val & (1 << index)) != 0;
                //end snippet
                int z;
                if (z1) { z = 1; } else { z = 0; }
                b1 = SetBitinByte(b1, index2, z);
                if ((index + 1) % 8 == 0)
                {
                    index2 = -1;
                    WriteByte(addr + bitindex, b1);
                    b1 = 0;
                    bitindex++;
                }
                index++;
                index2++;
                
            }
            
        }
        //returns a 16 bit unsigned halfword (short) from RAM at the position specifed by <addr>Rejects addresses not divisble by 2.
        public ushort ReadHalfWord(uint addr) { if (addr % 2 != 0) { return 0; }
            if (addr % 2 != 0) { return 65111; }
            if (!BoundsCheck(addr)) { return 65111; }
            return (ushort)(ram[addr+1] << 8 | ram[addr]);
        }
        //wries a 16 bit word, <val>, to the position in RAM specifed by <addr>. Rejects addresses not divisble by 2.
        public void WriteHalfWord(uint addr, ushort val)
        {
            if (addr % 2 != 0) { return; }
            if (!BoundsCheck(addr))
            {return; }
            int index = 0;
            byte b1 = 0;
            byte b2 = 0;
            int index2 = 0;
            while (index < 8)
            {
                var z1 = (val & (1 << index)) != 0;
                int z;
                if (z1) { z = 1; } else { z = 0; }
                b1 = SetBitinByte(b1, index2, z);
                index++;
                index2++;
            }
            index2 = 0;
            while (index < 16)
            {
                var z1 = (val & (1 << index)) != 0;
                int z;
                if (z1) { z = 1; } else { z = 0; }
                b2 = SetBitinByte(b2, index2, z);
                index++;
                index2++;
            }
            ram[addr] = b1;
            ram[addr+1] = b2;
        }

        //reads an 8-bit byte from the RAM array at <addr> and returns it.
        public byte ReadByte(uint addr) {
            //if (!BoundsCheck(addr)){ return null; }

            return ram[addr];
        }

        //writes an 8-bit byte value, <val>,to the position in RAM at <addr>;
        public void WriteByte(uint addr, byte b) {
            if (!BoundsCheck(addr)) { return; }
            ram[addr] = b;
        }
        
        //retreives the word at <addr> from RAM and checks if the bit at the position specified by <bit> 
        //is 0 or 1. Returns true if 1, false if 0.
        public bool? TestFlag(uint addr, int bit) {
            if (!BoundsCheck(addr)) { return null; }
            uint word = ReadWord(addr);            
            var x = (word & (1 << bit)) != 0;          
            if(x==false)return false;
            return true;
        }

        //similar to above method, however used later in this class as a helper method to ExtractBits.
        private static bool TestFlag(int bit, uint word)
        {                       
            var x = (word & (1 << bit)) != 0;           
            if (x == false) return false;
            return true;
        }

        public static bool TestBit(int bit, uint word)
        {
            var x = (word & (1 << bit)) != 0;
            if (x == false) return false;
            return true;
        }

        //reads a word from the <addr> in RAM and sets the bit at the position specifed by <bit> to the value of <flag>. 1 if <flag> is true, 0 if it
        //is false. Then writes the word back to RAM. No other bits than <bit> are affected.
        public void SetFlag(uint addr, int bit, bool flag){
            if (!BoundsCheck(addr)) { return; }
            uint word = ReadWord(addr);
            int bitval = 0;
            if (flag) bitval = 1;
            else bitval = 0;
            word = SetBitinInt(word, bit, bitval);
            WriteWord(addr, word);
        }

        //sets a specific bit in a given byte. Helper method used in WriteHalfWord and WriteWord.
        //returns a byte with the new bit value.
        byte SetBitinByte(byte val, int pos, int bitval)
        {       
            byte binval = (byte) Math.Pow(2, pos);        
            if (bitval == 1)
            {
                val = (byte) (val | binval); 
            }
            else
            {
                byte nval = (byte)~val;
                nval = (byte)(nval | binval);
                nval = (byte)(~nval);
                val = (byte)(val & nval);
            }
            return val;
        }

        //similar to above method, but used in SetFlag. Sets a bit value in the postion specified and returns the new int.
        uint SetBitinInt(uint val, int pos, int bitval)
        {           
            uint binval = (uint)(Math.Pow(2, pos));

            if (bitval == 1)
            {
                val = (val | binval);
            }
            else
            {
                uint nval =~val;

                nval = (nval | binval);
                nval = (~nval);
                val = (val & nval);
            }
            return val;
        }
        static public uint SetBitinInt(uint val, int pos, bool bitval)
        {
            uint binval = (uint)(Math.Pow(2, pos));

            if (bitval == true)
            {
                val = (val | binval);
            }
            else
            {
                uint nval = ~val;

                nval = (nval | binval);
                nval = (~nval);
                val = (val & nval);
            }
            return val;
        }
        //similar to above method, but made static so it can be used in ExtractBits.
        private static uint SetBitinIntForExtract(uint val, int pos, int bitval)
        {
            uint binval = (uint)(Math.Pow(2, pos));
            if (bitval == 1)
            {
                val = (uint)(val | binval);
            }
            else
            {

            }
            return val;
        }

        //takes a <word>, and extracts a given range of bits from it. Starts at <startbit>, ends at <endbit>.
        //returns a uint with the extracted bit value from <word>
        static public uint ExtractBits(uint word, int startbit, int endbit) {
            uint result = 0;           
            int index = 0;
            int bitpos = 0;
            while (index <= endbit)
            {
                if (index >= startbit)
                {
                    bool val = TestFlag(index, word);
                    int nubit = 0;
                    if (val) nubit = 1;
                    else nubit = 0;
                    result = SetBitinIntForExtract(result, bitpos, nubit);
                    bitpos++;
                }
                index++;
               
            }
            return result;

        }

        //completes a checksum algorithm on the RAM array and returns an int with the computed value.
        public int Checksum()
        {
            int cksum = 0;
            int addr = 0;
             while (addr < ram.Length)
             {
                 cksum += ram[addr] ^ addr;
                 addr++;
             }
            return cksum;
        }
        //checks if an addr is in bounds
        private bool BoundsCheck(uint addr)
        {
            if (addr >= ram.Length)
            {
                return false;
            }
            return true;
        }
        //swaps registers and mode for swi instructions. <sig> denotes the new mode to switch to. 0= SYS, 1 = SVC, 2=IRQ
        public void swapRegisters(int sig)
        {
            
            if(modetrack==0 && sig == 1)
            {
                //10011
                SPSR_svc = cpsr;
                LR_svc = getPC();
                SP_svc = r13;
                r15 = 0x8;
               cpsr= SetBitinInt(cpsr, 0, true);
                cpsr=SetBitinInt(cpsr, 1, true);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, false);
                cpsr = SetBitinInt(cpsr, 4, true);
               // SetBitinInt(cpsr, 7, true);
                modetrack =1;
            }
            if (modetrack == 0 && sig == 2)
            {//10010
                sys = true;
                SPSR_irq = cpsr;
                LR_irq = getPC()+4;
                SP_irq = r13;
                r15 = 0x18;
                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, false);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, false);
                cpsr = SetBitinInt(cpsr, 7, true);
                modetrack =2;
            }
            if (modetrack == 1 && sig == 0)
            {
                cpsr= SPSR_svc;
                setRegister(15, LR_svc);
                setRegister(13, SP_svc);
                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, true);
                cpsr = SetBitinInt(cpsr, 2, true);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, true);
                modetrack = 0;
            }
          
            if (modetrack == 2 && sig == 0)
            {
                /*  cpsr = SPSR_irq;
                  setRegister(15, LR_irq);
                  setRegister(13, SP_irq);
                  SetBitinInt(cpsr, 0, false);
                  SetBitinInt(cpsr, 1, false);
                  SetBitinInt(cpsr, 2, false);
                  SetBitinInt(cpsr, 3, false);
                  SetBitinInt(cpsr, 4, true);
                  modetrack = 0;*/
                if (!sys)
                {
                    cpsr = SPSR_irq;
                    setRegister(15, LR_irq);
                    setRegister(13, SP_irq);
                    cpsr = SetBitinInt(cpsr, 0, false);
                    cpsr = SetBitinInt(cpsr, 1, false);
                    cpsr = SetBitinInt(cpsr, 2, false);
                    cpsr = SetBitinInt(cpsr, 3, false);
                    cpsr = SetBitinInt(cpsr, 4, true);
                    modetrack = 1;
                }
                else
                {
                    sys = false;
                    cpsr=SPSR_irq;
                    r15 = LR_irq;
                    r13 = SP_irq;

                    cpsr = SetBitinInt(cpsr, 0, true);
                    cpsr = SetBitinInt(cpsr, 1, true);
                    cpsr = SetBitinInt(cpsr, 2, true);
                    cpsr = SetBitinInt(cpsr, 3, true);
                    cpsr = SetBitinInt(cpsr, 4, true);
                    cpsr = SetBitinInt(cpsr, 7, false);
                    modetrack = 2;
                }
            }
            if (modetrack == 1 && sig == 2)
            {
                SPSR_irq=cpsr;
                cpsr = SetBitinInt(cpsr, 7, true);
                LR_irq = getPC() + 4;
                setRegister(15, 0x18);
                SP_irq = getRegister(13);
                cpsr = SetBitinInt(cpsr, 0, false);
                cpsr = SetBitinInt(cpsr, 1, true);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, false);
                cpsr = SetBitinInt(cpsr, 4, true);
                modetrack = 2;
            }
            if (modetrack == 2 && sig == 1)
            {
                SPSR_svc = cpsr;
                LR_svc = getPC();
                SP_svc = getRegister(13);
                r15 = 0x8;
                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, false);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, true);
              //  SetBitinInt(cpsr, 7, true);
                modetrack = 1;
            }
            if (modetrack == 2 && sig == 0)
            {
                /* cpsr = SPSR_irq;
                 setRegister(15, LR_irq);
                 setRegister(13, SP_irq);
                 SetBitinInt(cpsr, 0, false);
                 SetBitinInt(cpsr, 1, false);
                 SetBitinInt(cpsr, 2, false);
                 SetBitinInt(cpsr, 3, false);
                 SetBitinInt(cpsr, 4, true);
                 Memory.SetBitinInt(this.cpsr, 7, false);
                 modetrack = 0;*/
                SPSR_svc = cpsr;
                LR_svc = getPC();
                SP_svc = getRegister(13);
                r15 = 0x8;
                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, false);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, true);
               // SetBitinInt(cpsr, 7, true);
                modetrack = 1;
            }
        }
        //return current CPSR.
        public uint getSPSR()
        {
            if (modetrack == 0)
            {
                return cpsr;
            }
            if(modetrack==1){
                return SPSR_svc;
            }
            else if(modetrack==2)
            {
                return SPSR_irq;
            }
            return 0;
        }
      
        //switches modes and swaps registers for msr instruction.
        public void swapStacks(uint sig)
        {
            if (modetrack == 0 && sig == 1)
            {
                //10011
                SPSR_svc = cpsr;
                LR_svc = r14;
                SP_svc = r13;

                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, true);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, false);
                cpsr = SetBitinInt(cpsr, 4, true);
                // SetBitinInt(cpsr, 7, true);
                modetrack = 1;
            }
            if (modetrack == 0 && sig == 2)
            {//10010
                sys = true;
                SPSR_irq = cpsr;
                LR_irq = r14;
                SP_irq = r13;

                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, false);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, false);
                cpsr = SetBitinInt(cpsr, 7, true);
                modetrack = 2;
            }
            if (modetrack == 1 && sig == 0)
            {
                cpsr = SPSR_svc;
                //r14 = LR_svc;
                r13 = SP_svc;
                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, true);
                cpsr = SetBitinInt(cpsr, 2, true);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, true);
                modetrack = 0;
            }
            if (modetrack == 0 && sig == 0)
            {
                cpsr = SPSR_svc;
               // r14 = LR_svc;
                r13 = SP_svc;
                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, true);
                cpsr = SetBitinInt(cpsr, 2, true);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, true);
                modetrack = 0;
            }
            if (modetrack == 2 && sig == 0)
            {
                /*  cpsr = SPSR_irq;
                  setRegister(15, LR_irq);
                  setRegister(13, SP_irq);
                  SetBitinInt(cpsr, 0, false);
                  SetBitinInt(cpsr, 1, false);
                  SetBitinInt(cpsr, 2, false);
                  SetBitinInt(cpsr, 3, false);
                  SetBitinInt(cpsr, 4, true);
                  modetrack = 0;*/
                if (!sys)
                {
                    cpsr = SPSR_irq;
                    r15 = LR_irq;
                    setRegister(13, SP_irq);
                    cpsr = SetBitinInt(cpsr, 0, false);
                    cpsr = SetBitinInt(cpsr, 1, false);
                    cpsr = SetBitinInt(cpsr, 2, false);
                    cpsr = SetBitinInt(cpsr, 3, false);
                    cpsr = SetBitinInt(cpsr, 4, true);
                    modetrack = 1;
                }
                else
                {
                    sys = false;
                    cpsr = SPSR_irq;
                  
                    r13 = SP_irq;
                     r15 = LR_irq;
                    cpsr = SetBitinInt(cpsr, 0, true);
                    cpsr = SetBitinInt(cpsr, 1, true);
                    cpsr = SetBitinInt(cpsr, 2, true);
                    cpsr = SetBitinInt(cpsr, 3, true);
                    cpsr = SetBitinInt(cpsr, 4, true);
                    cpsr = SetBitinInt(cpsr, 7, false);
                    modetrack = 2;
                }
            }
            if (modetrack == 1 && sig == 2)
            {
                SPSR_irq = cpsr;
                cpsr = SetBitinInt(cpsr, 7, true);
                LR_irq = getRegister(14);

                SP_irq = getRegister(13);
                cpsr = SetBitinInt(cpsr, 0, false);
                cpsr = SetBitinInt(cpsr, 1, true);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, false);
                cpsr = SetBitinInt(cpsr, 4, true);
                modetrack = 2;
            }
            if (modetrack == 2 && sig == 1)
            {
                SPSR_svc = cpsr;
                
                SP_svc = getRegister(13);

                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, false);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, true);
                //  SetBitinInt(cpsr, 7, true);
                modetrack = 1;
            }
            if (modetrack == 2 && sig == 0)
            {
                /* cpsr = SPSR_irq;
                 setRegister(15, LR_irq);
                 setRegister(13, SP_irq);
                 SetBitinInt(cpsr, 0, false);
                 SetBitinInt(cpsr, 1, false);
                 SetBitinInt(cpsr, 2, false);
                 SetBitinInt(cpsr, 3, false);
                 SetBitinInt(cpsr, 4, true);
                 Memory.SetBitinInt(this.cpsr, 7, false);
                 modetrack = 0;*/
                SPSR_svc = cpsr;
                
                SP_svc = getRegister(13);
               // LR_svc = getRegister(14);
                cpsr = SetBitinInt(cpsr, 0, true);
                cpsr = SetBitinInt(cpsr, 1, false);
                cpsr = SetBitinInt(cpsr, 2, false);
                cpsr = SetBitinInt(cpsr, 3, true);
                cpsr = SetBitinInt(cpsr, 4, true);
                // SetBitinInt(cpsr, 7, true);
                modetrack = 1;
            }
        }
        public void writeSPSR(uint val)
        {
            r13 = val;
        }
    }
}
