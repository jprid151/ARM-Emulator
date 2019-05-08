using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    class MSRInstruction : Instruction
    { 
        //tracks if the instruction has a immediate or register operand.
        bool reg;

        
        public MSRInstruction(uint nval, ref Computer comp) : base(nval, ref comp)
        {

        }
        //decode the instrucoin
        public void Decode()
        {
            reg = Memory.TestBit(22, value);
            if (Memory.ExtractBits(value, 23, 27) == 6)
            {
                source = new Operand(Memory.ExtractBits(value, 0, 7), 0, 0, Memory.ExtractBits(value, 8, 11), false);

            }
            else
            {
                dest = new Operand(Memory.ExtractBits(value, 0, 3), 0, 0, 0, true);
            }

        }
        //executes the instruction and swaps processsor modes if needed.
        public override void Execute()
        {
            if (dest != null)
            {
                if (!reg)
                {
                    uint val = computer.getReg((int)dest.RegNum);
                   if (Memory.ExtractBits(val, 0, 4) == 0b10010)
                    {
                        computer.Registers.swapStacks(2);
                    }
                    if (Memory.ExtractBits(val, 0, 4) == 0b10011)
                    {
                        computer.Registers.swapStacks(1);
                    }
                    if (Memory.ExtractBits(val, 0, 4) == 0b11111)
                    {
                        computer.Registers.swapStacks(0);
                    }
                   
                    computer.Registers.cpsr = computer.getReg((int)dest.RegNum);
                }
                else
                {
                    computer.Registers.writeSPSR(computer.getReg((int)dest.RegNum));
                }

            }
            else
            {
                if (!reg)
                {
                    uint val = BarrelShifter.ROR(source.immNum, (int)source.rotateAmt);
                    computer.Registers.writeSPSR(val);
                }
            
                else
                {
                    uint val = BarrelShifter.ROR(source.immNum, (int)source.rotateAmt);
                    computer.Registers.writeSPSR(val);
                }
            }
        }
        //returns the dissassembled representation
        public override string ToAssembly()
        {
            string result= "msr ";
            if (dest != null)
            {
                if (!reg)
                {
                    result += "cpsr, r";
                }
                else
                {
                    result += "spsr, r";
                }
                result += dest.RegNum;
                return result;
            }
            else
            {
                if (!reg)
                {
                    result += "cpsr, 0x";
                }
                else
                {
                    result += "spsr, 0x";
                }
                result += source.immNum;
                return result;
            }
        }
    }
}
