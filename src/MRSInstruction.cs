using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    class MRSInstruction: Instruction
    {
        public MRSInstruction(uint nval, ref Computer comp): base (nval, ref comp) { }
        bool reg;
        //executes the instruction.
        public override void Execute()
        {
            if (!reg)
            {
                computer.Registers.setRegister((int)dest.RegNum, computer.Registers.cpsr);
            }
            else
            {
                computer.Registers.setRegister((int)dest.RegNum, computer.Registers.getSPSR());
            }
        }
        //decode the instruction when called.
        public void Decode()
        {
            dest = new Operand(Memory.ExtractBits(value, 12, 15), 0, 0, 0, true);
            reg = Memory.TestBit(22, value);
        }//return the dissasembly of the  instruction
        public override string ToAssembly()
        {
            if (reg)
            {
                string result = "mrs r" + dest.RegNum;
                return result;
            }
            else
            {
                string result = "mrs r" + dest.RegNum;
                result += "CPSR";
                return result;
            }
        }
    }
}
