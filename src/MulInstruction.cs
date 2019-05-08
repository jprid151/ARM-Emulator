using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//-------------------------------------------
//MulInstruction.cs
//Handles mul instruction decoding/execution etc.
//--------------------------------------------
namespace armsim
{
    //Code representation of mul instruction
    class MulInstruction : DataProcessingInstruction
    {
       
        public MulInstruction(uint nval, ref Computer comp):base(nval,ref comp) {

        }
        //decode the mulinstruction from the value
        public void Decode()
        {
            uint destnum = Memory.ExtractBits(value, 16, 19);
            uint sourcenum = Memory.ExtractBits(value, 0, 3);
            uint op2num = Memory.ExtractBits(value, 8, 11);

            dest = new Operand(destnum, 0, 0, 0, true);
            source= new Operand(sourcenum, 0, 0, 0, true);
            operand2 = new Operand(op2num, 0, 0, 0, true);
        }
        //preform a multiplication operation
        public override void Execute()
        {
            if (!ShouldExecute()) { return; }
            uint val1 = computer.Registers.getRegister((int)source.RegNum);
            uint val2 = computer.Registers.getRegister((int)operand2.RegNum);
            uint result = val1 * val2;
            computer.Registers.setRegister((int)dest.RegNum, result);
        }
        //return a representation of the instruction
        public override string ToAssembly()
        {
            string result = "mul"+GetOpcode()+" ";
            result += "r" + dest.RegNum+ ", ";
            result += "r" + source.RegNum + ", ";
            result += "r" + operand2.RegNum+ " ";
            return result;
        }
        //change the flags if needed (phase 4)
        
        //determine if flags need to change (phase 4)
        
    }
}
