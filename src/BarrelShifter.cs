using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//---------------------------------------------
//BarrelShifter.cs
//Contains logic for shift operations
//---------------------------------------------
namespace armsim
{   //handles shift operations.
    static class BarrelShifter
    {
        //perform a logical left shifton <val>
        static public uint LogicLeftShift(uint val, int bits)
        {
            return val << bits;
        }
        //perform a logical right shifton <val>
        static public uint LogicalRightShift(uint val,int bits)
        {
            
            return val >> bits;
        }
        //perform an arithmetic right shift on <val>
        static public uint ArithRightShift(uint val, int bits)
        {
            int x = (int)val;
            val = (uint)(x>> bits);
            return val;
        }
        //perform a rotatation on <val>
        static public uint ROR(uint val, int bits)
        {
            
            return (val >> bits) | (val << (32 - bits));
        }
    }
}
