using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//------------------------------------------------------------
//CompInstruction.cs
//Contains decode/execute logic for comp instructions
//------------------------------------------------------------
namespace armsim
{

    //models the and, eor, and orr instructions in code
    class CompInstruction: DataProcessingInstruction
    {
        //tells the type of comparison
        public int type;
        
        public CompInstruction(uint nval, Computer comp): base( nval, ref comp)
        {
            //if opcode = x then type=and
            //else if opcode = x then type = or
            //else type = eor
        }
        //perform a comparison operation
        public override void Execute()
        {
            if (!ShouldExecute()) { return; }
            uint val1 =0;
            if (type != 3)
            {
                val1 = computer.getReg((int)source.RegNum);
            }
            else
            {
                val1 = computer.getReg((int)dest.RegNum);
            }
            if (operand2.immNum != uint.MaxValue)
            {
                uint val2 = operand2.immNum;
                int rotate = (int)operand2.rotateAmt * 2;
                val2 = (uint)(val2 >> rotate) | (val2 << (32 - rotate));
                //perform and cmp
                if (type == 0)
                {
                    uint result = val1 & val2;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                    //ChangeFlags(val1, val2, result);
                }
                //perform or cmp
                else if (type == 1)
                {
                    uint result = val1 | val2;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                   // ChangeFlags(val1, val2, result);
                }
                //perform eor cmp
                else if (type==2)
                {
                    uint result = val1 ^ val2;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                    //ChangeFlags(val1, val2, result);
                }
                else
                {
                    uint result = computer.getReg((int)dest.RegNum) - val2;
                    ChangeFlags(val1, val2, result);
                }
                
            }
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
                else if (operand2.shiftNum == 1)
                {
                    value = BarrelShifter.LogicalRightShift(value, (int)(shiftamt));
                }
                else if (operand2.shiftNum == 2)
                {
                    value = BarrelShifter.ArithRightShift(value, (int)(shiftamt));
                }
                else
                {
                    value = BarrelShifter.ROR(value, (int)shiftamt);
                }
                //perform and cmp
                if (type == 0)
                {
                    uint result = val1 & value;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                    //ChangeFlags(val1, value, result);
                }
                //perform or cmp
                else if (type == 1)
                {
                    uint result = val1 | value;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                    //ChangeFlags(val1, value, result);
                }
                //perform eor cmp
                else if (type ==2)
                {
                    uint result = val1 ^ value;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                    //ChangeFlags(val1, value, result);
                }
                else
                {
                    uint result = computer.getReg((int)dest.RegNum) - value;
                    ChangeFlags(val1, value, result);
                }
                
                return;
            }


        }

        //determine if the code should execute  (phase 4)
       
        //change flags if needed (phase 4)
       
        //return a string representation of the instruction
        public override string ToAssembly()
        {
            
            string result = "";
            if (type == 0)
            {
                result += "and"+GetOpcode();
            }
            else if (type == 1) { result += "orr"; }
            else if (type == 2)
            {
                result += "eor"+GetOpcode();
            }
            else
            {
                result += "cmp"+GetOpcode();
            }
           
            result += " r" + dest.RegNum;
            if(type!=3)
            result += ", r" + source.RegNum;
            result += ", ";
            //add immediate op
            if (operand2.immNum != uint.MaxValue)

            {
                if (operand2.rotateAmt > 0)
                {
                    operand2.immNum = BarrelShifter.ROR(operand2.immNum, (int)operand2.rotateAmt*2);
                }
                result += "#" + operand2.immNum;

            }
            else
            {
                result += "r" + operand2.RegNum;
            }
            //add shifted op
            if (operand2.shiftAmt > 0)
            {

                if (operand2.shiftNum == 0)
                {
                    result += ", lsl";
                }
                else if (operand2.shiftNum == 1)
                {
                    result += ", lsr";
                }
                else if(operand2.shiftNum==2)
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
           
            return result;
        }
    }
}
