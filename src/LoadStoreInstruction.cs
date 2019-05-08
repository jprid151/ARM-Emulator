using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//--------------------------------------
//LoadStore.cs
//Contains logic for LoadStoreInstruction
//--------------------------------------
namespace armsim
{//models load/store instruction in code
    class LoadStoreInstruction : Instruction
    {
        //determines if load or store
        bool type;
        //gives addressing mode
        bool preindex;
        //determines if writeback is needed
        bool writeBack;
        //determines if datasize is byte, hword, or word
        bool size;
        //determines if positve or negative offset
        bool updown;
        public LoadStoreInstruction(uint nval, ref Computer comp) : base(nval, ref comp)
        {

        }
        //determine if the instruction is a load or store, and then set the register and addressing modes
        public void Decode()
        {
            bool write = Memory.TestBit(21, value);
            writeBack = write;
            bool type1  = Memory.TestBit(20, value);
            type = type1;
            bool hsize = Memory.TestBit(22, value);
            size = hsize;
            bool pre = Memory.TestBit(24, value);
            preindex = pre;
            updown = Memory.TestBit(23, value);
            uint destregnum = Memory.ExtractBits(value, 12, 15);
            dest = new Operand(destregnum, 0, 0, 0, true);
            uint srcregnum = Memory.ExtractBits(value, 16, 19);
            source = new Operand(srcregnum, 0, 0, 0, true);
            bool isImm = Memory.TestBit(25, value);
            if (!isImm)
            {
                operand2 = new Operand(Memory.ExtractBits(value, 0, 11), 0, 0, 0, false);
            }
            else
            {
                uint regnum = Memory.ExtractBits(value, 0, 3);
                uint shiftType = Memory.ExtractBits(value, 5, 6);
                uint shiftAmt = Memory.ExtractBits(value, 7, 11);
                operand2 = new Operand(regnum, shiftAmt, shiftType, 0, true);
            }
            
        }
        //execute a load/store
        public override void Execute()
        {
            if (!ShouldExecute()) { return; }
            uint writeval;
            uint noffset=0;
            if (operand2.immNum != uint.MaxValue)
                {
                    uint memval;
                    uint offset = computer.Registers.getRegister((int)source.RegNum);
                if (type)
                {
                    if (updown)
                        noffset = offset + operand2.immNum;
                    else
                        noffset = offset - operand2.immNum;

                    if (noffset == 0x100001)
                    {
                        memval = computer.getChar();
                    }
                    else
                    {
                        if (preindex == true)
                        {


                            if (!size)
                            {
                                memval = computer.RAM.ReadWord(noffset);
                            }
                            else
                            {
                                memval = computer.RAM.ReadByte(noffset);
                            }
                        }
                        else
                        {
                            if (!size)
                            {
                                memval = computer.RAM.ReadWord(offset);
                            }
                            else
                            {
                                memval = computer.RAM.ReadByte(offset);
                            }
                        }
                    }
                        computer.Registers.setRegister((int)dest.RegNum, memval);
                   
                }
                else
                {
                    uint nuval = computer.Registers.getRegister((int)dest.RegNum);
                   
                    if (updown)
                        noffset = offset + operand2.immNum;
                    else
                        noffset = offset - operand2.immNum;
                    
                    if (!size)
                    {
                        if(preindex==true)
                        computer.RAM.WriteWord(noffset, nuval);
                        else { computer.RAM.WriteWord(offset, nuval); }
                    }
                    else
                    {
                        if (noffset == 0x100000)
                        {
                            char c = (char)computer.getReg(0);
                            computer.SendChar(c);
                        }
                        else
                        {
                            if(preindex==true)
                            computer.RAM.WriteByte(noffset, (byte)Memory.ExtractBits(nuval, 0, 7));
                            else
                            {
                                computer.RAM.WriteByte(offset, (byte)Memory.ExtractBits(nuval, 0, 7));
                            }
                        }
                    }

                }

                writeval = noffset;
                }
                else
                {
                    uint memval;
            
                uint offset1= computer.Registers.getRegister((int)source.RegNum);
                uint off3 = offset1;
                    uint offset2 = computer.Registers.getRegister((int)operand2.RegNum);
                    if (operand2.shiftAmt > 0)
                    {
                        if (operand2.shiftNum == 0)
                        {
                            offset2 = BarrelShifter.LogicLeftShift(offset2,(int) operand2.shiftAmt);
                        }
                        else if (operand2.shiftNum == 1)
                        {
                            offset2 = BarrelShifter.LogicalRightShift(offset2, (int)operand2.shiftAmt);
                        }
                        else if (operand2.shiftNum == 2)
                        {
                            offset2 = BarrelShifter.ArithRightShift(offset2, (int)operand2.shiftAmt);
                        }
                        else
                        {
                        offset2 = BarrelShifter.ROR(offset2, (int)operand2.shiftAmt);
                        }
                    }
                if (updown) { offset1 = offset1 + offset2; }
                else { offset1 = offset1 - offset2; }
                   
                if (type)
                {
                    if (offset1 == 0x100001)
                    {
                        memval = computer.getChar();
                    }
                    else
                    {
                        if (preindex == true)
                        {
                            if (!size)
                            {
                                memval = computer.RAM.ReadWord(offset1);
                            }
                            else
                            {
                                memval = computer.RAM.ReadByte(offset1);
                            }
                        }
                        else
                        {
                            if (!size)
                            {
                                memval = computer.RAM.ReadWord(off3);
                            }
                            else
                            {
                                memval = computer.RAM.ReadByte(off3);
                            }
                        }
                    }
                    computer.Registers.setRegister((int)dest.RegNum, memval);
                }
                else
                {
                    uint nuval = computer.Registers.getRegister((int)dest.RegNum);
                    if (preindex == true)
                    {
                        if (!size)
                        {
                            computer.RAM.WriteWord(offset1, nuval);
                        }
                        else
                        {
                            computer.RAM.WriteByte(offset1, (byte)Memory.ExtractBits(nuval, 0, 7));
                        }
                    }
                    else
                    {
                        if (!size)
                        {
                            computer.RAM.WriteWord(off3, nuval);
                        }
                        else
                        {
                            computer.RAM.WriteByte(off3, (byte)Memory.ExtractBits(nuval, 0, 7));
                        }
                    }
                }
                    writeval = offset1;
                }
                if (writeBack||!preindex)
                {
                    computer.Registers.setRegister((int)source.RegNum, writeval);
                }
            
        }
        //return an assembly representation of the instructions
        public override string ToAssembly()
        {
            string result = "";
            if (type)
            {
                result += "ldr "+GetOpcode();
            }
            else
            {
                result += "str "+GetOpcode();
            }
            result += "r" + dest.RegNum + ", ";
            result += "[r" + source.RegNum;
            if (operand2.immNum != uint.MaxValue)
            {
                if (operand2.immNum == 0)
                {
                    result += "]";
                }
                else
                {
                    if(updown)
                    result += ", #" + operand2.immNum + "]";
                    else
                    {
                        result += ", #-" + operand2.immNum + "]";
                    }
                }
                
            }
            else
            {
                if(updown)
                result += ", r"+operand2.RegNum;
                else
                    result += ", -r" + operand2.RegNum;
                if (operand2.shiftAmt > 0)
                {
                    result += ", ";
                    if (operand2.shiftNum == 0)
                    {
                        result += "lsl ";
                    }
                    else if (operand2.shiftNum == 1)
                    {
                        result += "lsr ";
                    }
                    else if (operand2.shiftNum == 2)
                    {
                        result += ", asr";
                    }
                    else
                    {
                        result += ", ror";
                    }
                    result += "#" + operand2.shiftAmt;
                    result += "]";
                }
                else
                {
                    result += "]";
                }
            }
            if (writeBack)
                result += "!";
            return result;
        }
     
        //determine if the instruction should execute based on the system flags (implemented in phase 4)
        
        //change the flags if the instruction calls for it (implemented in phase 4)
        
    }
}
