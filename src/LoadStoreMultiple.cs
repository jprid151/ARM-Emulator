using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//---------------------------------------------------------------------
//LoadStoreMultiple.cs
//Contains logic to decode/execute ldm/stm instructions
//---------------------------------------------------------------------
namespace armsim
{ //Code representation of Load/Store Multiple instruction
    class LoadStoreMultipleInstruction: Instruction
    {
        //determines if writeback is present
        public bool write;
        //determines if instruction is load or store
        public bool type;
        //determines which registers are updated
        public bool[] regs;
      
        public LoadStoreMultipleInstruction(uint nval, ref Computer comp) : base(nval, ref comp)
        {
            regs = new bool[16];
            int x = 0;
            while (x < 16)
            {
                regs[x] = false;
                x++;
            }
        }
        //decode the instruction based on its encoding
        public void Decode()
        {
            write = Memory.TestBit(21, value);
            type = Memory.TestBit(20, value);
            uint regNum = Memory.ExtractBits(value, 16, 19);
            source = new Operand(regNum, 0, 0, 0, true);

            int x = 0;
            while (x < 16)
            {
                bool b = Memory.TestBit(x, value);
                regs[x] = b;
                x++;
            }
        }
        //execute a load or store multiple operation
        public override void Execute()
        {
            if (!ShouldExecute()) { return; }
            uint regAddress = computer.Registers.getRegister((int)source.RegNum);
            if (!type)
            {
                int x = 15;
                while (x > -1)
                {
                    bool b = regs[x];
                    if (b)
                    {
                        regAddress -= 4;
                        uint regval = computer.Registers.getRegister(x);
                        
                        computer.RAM.WriteWord(regAddress, regval);
                        
                    }
                    x--;
                }
                if(write)
                computer.Registers.setRegister((int)source.RegNum, regAddress);
            }
            else
            {
                int x = 0;
                while (x <15)
                {
                    bool b = regs[x];
                    if (b)
                    {
                        uint memval = computer.RAM.ReadWord(regAddress);
                        
                        regAddress += 4;
                        computer.Registers.setRegister(x, memval);
                    }
                    x++;
                }
                if(write)
                computer.Registers.setRegister((int)source.RegNum, regAddress);
            }
        }
        //return a string representation of the instruction
        public override string ToAssembly()
        {
            string result = "";
            if (type)
            {
                result += "ldmfd"+GetOpcode()+" r"+source.RegNum;
            }
            else
            {
                result += "stmfd" +GetOpcode()+" r"+source.RegNum;
            }
            if (write)
                result += "!";
            result += ", {";
            int x = 0;
            while (x < 16)
            {
                bool b = regs[x];
                if (b == true)
                {
                    result += "r" + x + ",";
                }
                x++;
            }
            result = result.Remove(result.LastIndexOf(","));
            result += "}";
            return result;
        }
        //phase 4
      
        //phase 4
       
    }
}
