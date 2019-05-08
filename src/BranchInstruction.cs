using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    class BranchInstruction: Instruction
    {
        public bool link;
        public uint offset;
        public bool bx;
        public uint strset;
        public BranchInstruction(uint nval,ref Computer comp): base(nval, ref comp)
        {

        }
        public override void Execute()
        {
            bool x = ShouldExecute();
            if (!x) { return; }
            if (bx) { computer.Registers.setRegister(15, computer.Registers.getRegister(14)); return; }
            if(link)
            computer.Registers.setRegister(14,computer.Registers.getPC());
            computer.Registers.setRegister(15, (uint) offset);
        }
        
        public override string ToAssembly()
        {
            if (link) { return "bl"+GetOpcode()+"  " + offset.ToString("X4"); }
            if (bx) { return "br"+GetOpcode()+" lx"; }
            return "br"+GetOpcode()+ " "+ offset.ToString("X4");
        }
        
    }
}
