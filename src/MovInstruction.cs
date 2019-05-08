using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//---------------------------------------------
//MovInstruction.cs
//handles decode/execute/disassembly of mov instructions
//---------------------------------------------
namespace armsim
{
    //Code representation of mov instruction
    class MovInstruction: DataProcessingInstruction
    {
        //determines if instruction is an mvn instruction or not
        public bool mvn;
        public bool sinstruct;
        public MovInstruction(uint nval, Computer comp): base(nval,ref comp)
        {
            value = nval; computer = comp;
        }

        //moves the value from one op to another and stores it
        public override void Execute()
        {
            if (!ShouldExecute()) { return; }
            //execute a move for an immediate value
            if(value== 0xe1a00000)
            {
                return;
            }
            if (operand2.immNum != uint.MaxValue)
            {
                
                uint value = operand2.immNum;
                int rotate = (int)operand2.rotateAmt*2;
                operand2.immNum = (uint)(value >> rotate) | (value << (32 - rotate));
                if (mvn) { operand2.immNum = ~operand2.immNum; }
              
                computer.Registers.setRegister((int)dest.RegNum, operand2.immNum);
                if (sinstruct)
                {
                    MovFlags(0, value);
                }
                return;
            }
            //execute a move for a register/shifted register
            else
            {
                uint value = computer.getReg((int)operand2.RegNum);
                uint shifttype = operand2.shiftNum;
                uint shiftamt = operand2.shiftAmt;
                //shift amount according to type
                if (operand2.shiftNum == 0)
                {
                    value = BarrelShifter.LogicLeftShift(value, (int)(shiftamt));
                }
                if (operand2.shiftNum == 1)
                {
                    value = BarrelShifter.LogicalRightShift(value, (int)(shiftamt));
                }
                if (operand2.shiftNum == 2)
                {
                    value = BarrelShifter.ArithRightShift(value, (int)(shiftamt));
                }
                if (operand2.shiftNum == 3)
                {
                    value = BarrelShifter.ROR(value, (int)shiftamt);
                }
                if (mvn) { value=~value; }
                computer.Registers.setRegister((int)dest.RegNum, value);

                if (sinstruct)
                {
                    MovFlags(0, value);
                }
                return;
            }
            throw new NotImplementedException();
        }
        //used for movs to switch back processor modes.
       
        public void MovFlags(uint v2, uint value)
        {
            if (dest.RegNum != 15)
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
            }
            else
            {
                computer.Registers.swapStacks(0);
            }
        }
        //return a disassembled representation of the instruction
        public override string ToAssembly()
        {
            string result = "mov"+GetOpcode()+" ";

            result += "r" + dest.RegNum;
            if (operand2.RegNum != uint.MaxValue)
            {
                result += ", r" + operand2.RegNum;
                if (operand2.shiftAmt > 0) { 
                if (operand2.shiftNum == 0)
                {
                    result += ", lsl";
                }
                else if (operand2.shiftNum == 1)
                {
                    result += ", lsr";
                }
                else if (operand2.shiftNum == 2)
                {
                    result += ", asr";
                }
                else
                {
                    result += ", ror";
                }

                if (regshift > -1) { result += " r" + regshift; }
                else
                { 
                        result += " #" + operand2.shiftAmt;
                }
                }
                result = result.ToLower();
            }
            else
            {
                if (operand2.rotateAmt > 0)
                {
                    operand2.immNum = BarrelShifter.ROR(operand2.immNum, (int)operand2.rotateAmt*2);
                }
                result += ", #" + operand2.immNum.ToString();
                result = result.ToLower();
            }
            return result;
            
        }
        //change a flag if needed (phase 4)
     
    }
}
