using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//------------------------------------------------------
//AddSubInstruction.cs
//Contains logic for execution of add and sub instructions
//-----------------------------------------------------
namespace armsim
{   //models add,sub, and rsb instructions in code
    class AddSubInstruction: DataProcessingInstruction
    {
        //determines the condition code to follow for execution
#pragma warning disable IDE0044 // Add readonly modifier
        public uint flags;
#pragma warning restore IDE0044 // Add readonly modifier
                               //determines type of instruction
        public int type;
        public AddSubInstruction(uint nval, Computer comp): base(nval, ref comp)
        {
            
        }

        //perform an addition/subtraction operation
        public override void Execute()
        {
            if (!ShouldExecute()) { return; }
            uint val1 = computer.getReg((int)source.RegNum);
                if (operand2.immNum != uint.MaxValue)
                {
                    uint val2 = operand2.immNum;
                    int rotate = (int)operand2.rotateAmt * 2;
                    val2 = (uint)(val2 >> rotate) | (val2 << (32 - rotate));
                //perform addition operation
                if (type == 0)
                {
                    uint result = val2 + val1;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                }
                //perform subtraction operation
                else if (type == 1)
                {
                    uint result = val1 - val2;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                }
                //rsb operation
                else
                {
                    uint result = val2 - val1;
                    computer.Registers.setRegister((int)dest.RegNum, result);
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
                    if (operand2.shiftNum == 1)
                    {
                        value = BarrelShifter.LogicalRightShift(value, (int)(shiftamt));
                    }
                    if (operand2.shiftNum == 2)
                    {
                        value = BarrelShifter.ArithRightShift(value, (int)(shiftamt));
                    }
                    else if(operand2.shiftNum==3)
                    {
                       value = BarrelShifter.ROR(value, (int)shiftamt);
                    }
                    //add with shifted register
                if (type == 0)
                {
                    uint result = value + val1;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                }
                //subtract with shifted register
                else if(type ==1)
                {
                    uint result = val1 - value;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                }
                //rsb with shifted register
                else
                {
                    uint result = value - val1;
                    computer.Registers.setRegister((int)dest.RegNum, result);
                }
                return;
                }
            
        }
        //determine if the instruction should execute based on condition flags (phase 4)
        
        //return a disassembled represntation of the instruction
        public override string ToAssembly()
        {

           
            string result = "";
            if (type == 0)
            {
                result += "add"+GetOpcode();
            }
            else if (type == 1)
            {
                result += "sub"+GetOpcode();
            }
            else
            {
                result += "rsb"+GetOpcode();
            }
            result += " r" + dest.RegNum;
            result += ", r" + source.RegNum;
            result += ", ";//immediate value
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
            }//add shift operand
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
            return result;
        }
        //set the carry etc. flags if needed (phase 4)
        
    }
}
