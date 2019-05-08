using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{
    class CPU
    {
        //models a RAM array
        Memory RAM;
        //models a set of hardware registers
        Memory Registers;
        //contains a set of breakpoints for the CPU
        Dictionary<uint,bool> Breakpoints;
        //determines if breakpoints are on or off
        bool togbreak;
        //signals if input is present in the console.
        public uint irq;
        uint lbreak;
        //constructor for CPU class
        public CPU(ref Memory nram, ref Memory reg)
        {
            RAM = nram;
            Registers = reg;
            togbreak = true;
            Breakpoints = new Dictionary<uint,bool>();
            
        }

        //returns the next instruction and increments program counter
        public uint fetch ()
        {

            uint addr = Registers.getPC();
            if (togbreak)
            {
                if (Breakpoints.Keys.Contains(addr))
                {
                    Breakpoints.TryGetValue(addr, out bool x);


                    if (x)
                    {
                        lbreak = addr;
                        Breakpoints[addr] = false;
                        return uint.MaxValue;
                    }
                    else
                    {
                        Breakpoints[addr] = true;
                    }

                }
            }
           
            return RAM.ReadWord(addr);
            
        }

        //toggles breakpoints on or off
        public bool  toggleBreak()
        {
            if (togbreak) { togbreak = false; }
            else { togbreak = true; }
            return togbreak;
        }
        //does nothing right now
        public Instruction decode(uint instructval, ref Computer comp) {
            Instruction ins = Instruction.Factory(instructval, ref comp);
            return ins;
        }
        //halts execution for 1/4 of a second.
        public void execute(Instruction instruct)//Instruction instruct
        {
            //each instruction can potentially be executed differently, so i give each instruction
            //an execute method to handle its own execution
            //instruct.execute();
            
            Loader.Log("CPU: Executing", false, false);
            instruct.Execute();
        }
        //returns the list of breakpoints
        public Dictionary<uint,bool> getBreak()
        {
            return Breakpoints;
        }
        //adds a breakpoint for <addr>
        public void addBreak(uint addr)
        {
            Breakpoints.Add(addr, true);
        }
        //reset breakpoint on program reset
        public void resetBreak()
        {
            if(Breakpoints.ContainsKey(lbreak))
            Breakpoints[lbreak] = true;
        }
        //clear all breakpoints
        public void clearBreak()
        {
            Breakpoints.Clear();
        }
       
       
    }
   
}
