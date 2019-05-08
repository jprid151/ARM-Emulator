//-----------------------------------------
//SwiInstruction.cs
//Used to decode a swiinstruction
//-----------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{ //Code representation of swi instruction
    class SwiInstruction:Instruction
    {
        // contains value of swi instruction.
        public uint swivalue;
        public SwiInstruction(uint nval, ref Computer comp): base(nval,ref comp)
        {

        }

        //execute the swi instruction.
        public override void Execute() {

           
            if (swivalue == 0) { computer.Registers.swapRegisters(1);  }
            else if (swivalue == 0x6a) { computer.Registers.swapRegisters(1); }
            else
            {
                Kill();
            }

        }

        
        //tells the computer to halt if needed.
        public void Kill()
        {
            computer.halt = true;
        }

    
        //return a string for disassembly
        public override string ToAssembly()
        {
            string result = "swi #"+swivalue.ToString();
            return result;
        }
    }
}
