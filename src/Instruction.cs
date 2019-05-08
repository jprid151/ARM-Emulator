using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//----------------------------------
//Instruction.cs
//contians abstract INstruction class
//-----------------------------------
namespace armsim
{
    //Model that all instruction classes inherit from
    abstract class Instruction
    {
        //contians the machine language value for an instruction
        protected uint value;
        //contains source, dest, and op2'
        protected Operand source;
        protected Operand dest;
        protected Operand operand2;
        //contains a reference to the Computer
        protected Computer computer;
        protected uint flags;
        protected uint code;
        public Instruction(uint nval, ref Computer comp)
        {
            value = nval;
            computer = comp;
        }

        //models the Decode method that each familiy will contain.
        //public abstract void Decode();

        //return a string representation to the assembly.
        public abstract string ToAssembly();
        public void setCode(uint co)
        {
            code = Memory.ExtractBits(co,28,31);
        }
        //returns an Instruction based on the opcode contained in nval.
        static public Instruction Factory(uint nval, ref Computer comp)
        {
            //determine type of instruction by comparing bit values


           //return swi
            if(Memory.ExtractBits(nval,24,27)==0xF)
            {
                SwiInstruction swi = new SwiInstruction(nval, ref comp);
                swi.swivalue = Memory.ExtractBits(nval, 0, 23);
                return swi;
            }
            uint Mul1 = Memory.ExtractBits(nval, 21, 27);
            uint Mul2 = Memory.ExtractBits(nval, 4, 7);
            //return mulinstruction
            if (Mul1 == 0 && Mul2 == 9)
            {
                MulInstruction ins = new MulInstruction(nval, ref comp);
                ins.Decode();
                ins.setCode(nval);
                return ins;
            }
            if (nval == 0xE12fFf1e)
            {
                BranchInstruction b = new BranchInstruction(nval, ref comp);
                b.setCode(nval);
                b.bx = true;
                return b;
            }
            if (Memory.ExtractBits(nval, 23, 27) == 2 && Memory.ExtractBits(nval, 20, 21) == 0)
            {
                MRSInstruction mrs = new MRSInstruction(nval, ref comp);
                mrs.Decode();
                return mrs;
            }
            if (Memory.ExtractBits(nval, 23, 27) == 6 && Memory.ExtractBits(nval, 20, 21) == 2)
            {
                MSRInstruction msr = new MSRInstruction(nval, ref comp);
                msr.Decode();
                return msr;
            }
            if (Memory.ExtractBits(nval, 23, 27) == 2 && Memory.ExtractBits(nval, 20, 21) == 2)
            {
                MSRInstruction msr = new MSRInstruction(nval, ref comp);
                msr.Decode();
                return msr;
            }
                uint branch = Memory.ExtractBits(nval, 25, 27);
           
            if (branch == 5)
            {
                BranchInstruction bran = new BranchInstruction(nval, ref comp);
                bran.setCode(nval);
                if (Memory.TestBit(24,nval))
                {
                    bran.link = true;
                }
                else
                {
                    bran.link = false;
                }
              
                bran.offset = Memory.ExtractBits(nval, 0, 23);
                bran.strset = bran.offset;
                //bran.offset = (bran.offset & 0x2f000000);

                bran.offset = signExtension((uint)bran.offset);
                //bran.offset = bran.offset >> 6;
                bran.offset = bran.offset << 2;
                bran.offset = comp.getReg(15) + bran.offset;
                
                return bran;
            }
            

            uint LSM = Memory.ExtractBits(nval, 23, 27);
            //return ldm/stm instruction
            if(LSM == 18||LSM==17){
                LoadStoreMultipleInstruction ins = new LoadStoreMultipleInstruction(nval,ref comp);
                ins.Decode();
                ins.setCode(nval);
                return ins;
            }
            //return dataprocessing instruction
            uint type = Memory.ExtractBits(nval, 26, 27);
            if (type == 0)
            {
                DataProcessingInstruction ins = DataProcessingInstruction.Decode(nval, ref comp);
                ins.setCode(nval);
                return ins;
            }
            else 
            {//return loadstore instruction
                LoadStoreInstruction load = new LoadStoreInstruction(nval, ref comp);
                load.Decode();
                load.setCode(nval);
                return load;
            }
           
        }
        //perform sign extension for branch instructions.
        static public uint signExtension(uint instr)
        {
            uint value = (0x0000FFFF & instr);
            uint mask = 0x00008000;
            if ((mask & instr)>0)
            {
                value += 0xFFFF0000;
            }
            return value;
        }
        //models the Execute method used by subclasses
        public abstract void Execute();

        //determines if condition flags are set to allow executino
        public  bool ShouldExecute()
        {
            uint opcode = Memory.ExtractBits(computer.Registers.cpsr, 28, 31);
            if (code == 14)
            {
                return true;
            }
            if (code == 15)
            {
                return false;
            }
            if (code == 0 && Memory.TestBit(2, opcode))
            {
                return true;
            }
            else if (code == 1 && !Memory.TestBit(2, opcode))
            {
                return true;
            }
            else if (code == 2 && Memory.TestBit(1, opcode))
            {
                return true;
            }
            if (code == 3&& !Memory.TestBit(1, opcode))
            {
                return true;
            }
            if (code == 4 && Memory.TestBit(3, opcode))
            {
                return true;
            }
            if (code == 5 && !Memory.TestBit(3, opcode))
            {
                return true;
            }
            if (code == 6 && Memory.TestBit(0, opcode))
            {
                return true;
            }
            if (code == 7 && !Memory.TestBit(0, opcode))
            {
                return true;
            }
            if (code == 8 && Memory.TestBit(1, opcode)&&!Memory.TestBit(2, opcode))
            {
                return true;
            }
            if (code == 9 && (!Memory.TestBit(1, opcode) || Memory.TestBit(2, opcode)))
            {
                return true;
            }
            if (code == 10 && ((Memory.TestBit(3,opcode)&&Memory.TestBit(0,opcode))||((!Memory.TestBit(3, opcode) && !Memory.TestBit(0, opcode)))))
            {
                return true;
            }
            if (code == 11 && ((Memory.TestBit(3, opcode) && !Memory.TestBit(0, opcode)) || ((!Memory.TestBit(3, opcode) && Memory.TestBit(0, opcode))))) { 
                return true;
            }
            if (code == 12 && !Memory.TestBit(2, opcode) && ((Memory.TestBit(3,opcode) && Memory.TestBit(0,opcode))||((!Memory.TestBit(3, opcode) && !Memory.TestBit(0, opcode)))))
            {
                return true;
            }
            if (code == 13 && (Memory.TestBit(2, opcode) || ((Memory.TestBit(3, opcode) && !Memory.TestBit(0, opcode))) || ((!Memory.TestBit(3, opcode) && Memory.TestBit(0, opcode)))))
            {
                return true;
            }

            return false;
        }

        //changes flags if instruction mandates it
        public void ChangeFlags(uint v1, uint v2, uint value)
        {
            flags = Memory.SetBitinInt(flags, 31, Memory.TestBit(31, value));
            if (value == 0)
            {
                flags = Memory.SetBitinInt(flags, 30, true);
            }
            else
            {
                flags = Memory.SetBitinInt(flags, 30, false);
            }
           
                if (v2 > v1)
                {
                    flags = Memory.SetBitinInt(flags, 29, false);
                }
                else
                {
                    flags = Memory.SetBitinInt(flags, 29, true);
                }
            

            if (Memory.TestBit(31, v1) == false && Memory.TestBit(31, v2) == true && Memory.TestBit(31, value) == true)
            {
                flags = Memory.SetBitinInt(flags, 28, true);
            }
            else if (Memory.TestBit(31, v1) == true && Memory.TestBit(31, v2) == false && Memory.TestBit(31, value) == false)
            {
                flags = Memory.SetBitinInt(flags, 28, true);
            }
            else
            {
                flags = Memory.SetBitinInt(flags, 28, false);
            }
            int fi = 1;
            while (fi < 5)
            {
                computer.Registers.cpsr = Memory.SetBitinInt(computer.Registers.cpsr, 32 - fi, Memory.TestBit(32 - fi, flags));
                fi++;
            }
        }
        //returns opcodes for conditional execution dissasembly
        public string GetOpcode()
        {
            if(code == 0) { return "eq"; }
            if (code == 1) { return "ne"; }
            if (code == 2) { return "cs"; }
            if (code == 3) { return "cc"; }
            if (code == 4) { return "mi"; }
            if (code == 5) { return "pl"; }
            if (code == 6) { return "vs"; }
            if (code == 7) { return "vc"; }
            if (code == 8) { return "hi"; }
            if (code == 9) { return "ls"; }
            if (code == 10) { return "ge"; }
            if (code == 11) { return "lt"; }
            if (code == 12) { return "gt"; }
            if (code == 13) { return "le"; }
            else { return ""; }
            
        }
    }
    //models all operands including both immediates and registers
    //also contains shift information (type and amount)
    class Operand
    {
        //determines if operand is a memAddress or Register and which register to ge
        public uint RegNum;
        //determines memAddress
        public uint Address;
        //determines immediate value
        public uint immNum;
        //determines type of Shift, 0=lsl, 1=lsr, 2 = asr, 3 = ror
        public uint shiftNum;
        //determines amount of Shift
        public uint shiftAmt;
        //gives  mem offset if needed.
        public uint offset;
        //gives rotate amount
        public uint rotateAmt;
        //creates an operand and determines if it is an immediate or register, and how much it is shifted
        public Operand(uint num, uint shiftamt, uint shiftnum, uint rotate, bool isReg)
        {
            if (isReg)
            {
                RegNum = num;
                immNum = uint.MaxValue;
                shiftNum = shiftnum;
                shiftAmt = shiftamt;
                rotateAmt = uint.MaxValue;
            }
            else
            {
                RegNum = uint.MaxValue;
                immNum = num;
                shiftNum = shiftnum;
                shiftAmt = shiftamt;
                rotateAmt = rotate;
            }

        }
    }
}
