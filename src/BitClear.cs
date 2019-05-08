using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//--------------------------------------------------------------
//BitClear.cs
//Contains logic for decode/execute of Bic instructions
//-------------------------------------------------------------
namespace armsim
{   //models the BitClear instruction in code
    class BitClearInstruction: DataProcessingInstruction
    {
        //records type of bit clear (whether flag values must change)
        string type;
        
        public BitClearInstruction(uint nval, Computer comp):base( nval,ref comp)
        {

        }
        //execute a bit clear operation
        public override void Execute()
        {
            if (!ShouldExecute()) { return; }
            uint val1 = (computer.getReg((int)source.RegNum));
            if (operand2.immNum != uint.MaxValue)
            {
                uint val2 = operand2.immNum;
                int rotate = (int)operand2.rotateAmt * 2;
                val2 = (val2 >> rotate) | (val2 << (32 - rotate));
              
                uint result = (uint)(val1 & ~val2);
                uint d = (uint)(val1 & val2);
                computer.Registers.setRegister((int)dest.RegNum, result);
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
                if (operand2.shiftNum == 1)
                {
                    value = BarrelShifter.LogicalRightShift(value, (int)(shiftamt));
                }
                if (operand2.shiftNum == 2)
                {
                    value = BarrelShifter.ArithRightShift(value, (int)(shiftamt));
                }
                uint result = (uint)(val1 & ~value);
                computer.Registers.setRegister((int)dest.RegNum, result);
                return;
            }
        }
        //determine if the instruction should execute
       
        //change flags if needed
       
        //return a string representation of the instruction
        public override string ToAssembly()
        {
            string result = "bic"+GetOpcode();
            result += " r" + dest.RegNum;
            result += ", r" + source.RegNum;
            result += ", ";
            if (operand2.immNum != uint.MaxValue)
            {
                if (operand2.rotateAmt > 0)
                {
                    operand2.immNum = BarrelShifter.ROR(operand2.immNum, (int)operand2.rotateAmt);
                }
                result += "#" + operand2.immNum;

            }
            else
            {
                result += "r" + operand2.RegNum;
            }
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
                else
                {
                    result += ", asr";
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
