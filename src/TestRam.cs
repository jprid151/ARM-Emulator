//---------------------------------------------
//TestRam.cs Class holds unit tests that will be run
//when the --test option is discovered.
//---------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace armsim
{
    //static class to hold tests
    class TestRam
    {
        //runs the unit tests on the RAM and Loader classes.
        static public void RunTests()
        {
            
            Computer newComp = new Computer(32768);
            newComp.resetComputer(0);

            Log("Starting Add instruction test", false);
            //test add instruction
            newComp.Registers.setRegister(5, 25);
            Instruction addins = newComp.cpu.decode(0xE2854019, ref newComp);
            newComp.cpu.execute(addins);
            Debug.Assert(newComp.getReg(4) == 50);
            Log("Starting Sub instruction test", false);
            //test sub instruction with immediate shifted register
            newComp.Registers.setRegister(5, 17);
            newComp.Registers.setRegister(6, 1);
            Instruction subins = newComp.cpu.decode(0xE0454206, ref newComp);
            newComp.cpu.execute(subins);
            Debug.Assert(newComp.getReg(4) == 1);
            Log("Starting add instruction w/ register shifted register test", false);
            //test add instruction with register shifted register  
            newComp.Registers.setRegister(5, 2);
            newComp.Registers.setRegister(6, 1);
            newComp.Registers.setRegister(7, 4);
            Instruction addins2 = newComp.cpu.decode(0xE0854716, ref newComp);
            newComp.cpu.execute(addins2);
            Debug.Assert(newComp.getReg(4) == 18);
            Log("Starting Mul instruction test", false);
            //test mul instruction
            newComp.Registers.setRegister(5, 10);
            newComp.Registers.setRegister(6, 10);
            Instruction mulins = newComp.cpu.decode(0xE0040596, ref newComp);
            newComp.cpu.execute(mulins);
            Debug.Assert(newComp.getReg(4) == 100);
            Log("Starting Mov instruction test", false);
            //test mov instruction with register
            newComp.Registers.setRegister(8, 80);
            Instruction movins = newComp.cpu.decode(0xE1A04008, ref newComp);
            newComp.cpu.execute(movins);
            Debug.Assert(newComp.getReg(4) == 80);
            Log("Starting Mov instruction test with immediate shifted register", false);
            //test mov instruction with immediate shifted register
            newComp.Registers.setRegister(8, 1);
            Instruction movins2 = newComp.cpu.decode(0xE1A04208, ref newComp);
            newComp.cpu.execute(movins2);
            Debug.Assert(newComp.getReg(4) == 16);
            Log("Starting And instruction test", false);
            //test and instruction
            newComp.Registers.setRegister(1, 0xFE);
            Instruction andins = newComp.cpu.decode(0xe20120fe, ref newComp);
            newComp.cpu.execute(andins);
            Debug.Assert(newComp.getReg(2) == 0xFE);
            Log("Starting Eor instruction test", false);
            //test eor instruction
            newComp.Registers.setRegister(1, 0xFFFFFFFE);
            Instruction eorins = newComp.cpu.decode(0xe22134ff, ref newComp);
            newComp.cpu.execute(eorins);
            Debug.Assert(newComp.getReg(3) == 0x00FFFFFE);
            Log("Starting Or instruction test", false);
            //test orr instruction
            newComp.Registers.setRegister(1, 0x0);
            Instruction orins = newComp.cpu.decode(0xe381400f, ref newComp);
            newComp.cpu.execute(orins);
            Debug.Assert(newComp.getReg(4) == 0xF);
            Log("Starting Load instruction test", false);
            //test load instruction
            newComp.RAM.WriteWord(100, 5);
            newComp.Registers.setRegister(5, 75);
            Instruction loadins = newComp.cpu.decode(0xE5954019, ref newComp);
            newComp.cpu.execute(loadins);
            Debug.Assert(newComp.getReg(4) == 5);
            Log("Starting Writeback instruction test", false);
            //test writeback
            newComp.RAM.WriteWord(100, 5);
            newComp.Registers.setRegister(5, 75);
            Instruction loadins2 = newComp.cpu.decode(0xE5B54019, ref newComp);
            newComp.cpu.execute(loadins2);
            Debug.Assert(newComp.getReg(4) == 5);
            Debug.Assert(newComp.getReg(5) == 100);
            Log("Starting Store instruction test", false);
            //test store instruction          
            newComp.Registers.setRegister(4, 5);
            newComp.Registers.setRegister(5, 0);
            newComp.Registers.setRegister(6, 4);
            Instruction strins = newComp.cpu.decode(0xE7854106, ref newComp);
            newComp.cpu.execute(strins);
            Debug.Assert(newComp.RAM.ReadWord(16) == 5);
            Log("Starting Stm instruction test", false);
            //test store multiple
            newComp.Registers.setRegister(4, 1);
            newComp.Registers.setRegister(5, 2);
            newComp.Registers.setRegister(6, 3);
            Instruction strmins = newComp.cpu.decode(0xe92d0070, ref newComp);
            newComp.cpu.execute(strmins);
            Debug.Assert(newComp.RAM.ReadWord(0x6ff4) == 1);
            Log("Starting ldm instruction test", false);
            //test load multiple
            Instruction loadmins = newComp.cpu.decode(0xe89d0016, ref newComp);
            newComp.cpu.execute(loadmins);
            Debug.Assert(newComp.Registers.getRegister(1) == 1);
            Log("Starting bic instruction test", false);
            //test bit clear
            newComp.Registers.setRegister(0, 0xffffff00);
            Instruction bicins = newComp.cpu.decode(0xe3c020ff, ref newComp);
            newComp.cpu.execute(bicins);
            Debug.Assert(newComp.Registers.getRegister(2) == 0xFFFFFF00);
            Log("All tests passed!", false);
        }

        //writes <message> to the text box on the parent form, <log>
        //and to the logfile.
        static void Log(string message, bool doLog)
        {
            if (doLog)
            {
                Trace.WriteLine(message);
                
            }
            StreamWriter file = new StreamWriter("testlog.txt", true);
            file.WriteLine(message);
            file.Close();

        }
    }
}
