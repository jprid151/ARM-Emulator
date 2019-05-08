//---------------------------------------
//Options.cs 
//Contains logic to parse command line arguments.
//--------------------------------------

using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace armsim
{
    //parses command line arguments and grabs values from hem.
    class Options
    {
        //stores the size of the RAM array
        string memsize;
        //stors the size as an integer value
        public int mem;
        //determines if testing will be run.
        public bool test;
        //file that will be loaded into memory.
        public string filename;
        //array that holds command line args.
        List<String> args;
        //records the presence of the log argument
        public bool log;
        //tells the program to send log messages to the GUI
        public bool output;
        public bool exec;
        public bool traceall;
        //constructor for Options class
        public Options(string[] args1)
        {
            filename = null;
            log = false;
            traceall = false;
            args = args1.ToList<String>();
        }

        //parses Options and sets values for later use. Returns nothing.
        public void ParseOptions()
        {
            var op = new OptionSet()
         {
             {"l|load=",v=> filename=v },
             {"m|mem=",v=> memsize=v },
         };
            if (args.Contains("-load") || args.Contains("-mem") || args.Contains("-log") || args.Contains("-olog") || args.Contains("-m") || args.Contains("-l")|| args.Contains("--l")||args.Contains("--m"))
            {
                MessageBox.Show("Invalid command line argument. Aborting execution.");
                Environment.Exit(0);
            }
            if (args.Contains("--test")) { this.test = true;  args.Remove("--test"); }
            if (args.Contains("--log")) { this.log = true; args.Remove("--log"); }
            if (args.Contains("--olog")) { this.output = true; args.Remove("--olog"); }
            if (args.Contains("--exec")) { this.exec = true; args.Remove("--exec"); }
            if (args.Contains("--traceall")) { this.traceall = true; args.Remove("--traceall"); }
            List<String> invalid = op.Parse(args);
            //invalid arguments go here.
            if(invalid.Count > 0)
            {
                MessageBox.Show("Invalid command line argument. Aborting execution.");
                Environment.Exit(0);
            }
            try { mem = System.Convert.ToInt32(memsize); } catch { MessageBox.Show("Invalid memory parameter. Must be numeric. Quitting execution."); Environment.Exit(0); }
            if (mem==0) { mem = 32768; }
            if (mem>1000000) { MessageBox.Show("Allowed memory exceeded. Maximum amount of RAM permitted is 1MB (1,000,000). Quitting execution."); Environment.Exit(0); }
            //if (!args.Contains("--load")) { MessageBox.Show("No load option specified. Quitting execution."); Environment.Exit(0); }
            if (filename == "") { MessageBox.Show("No file found. Quitting execution."); Environment.Exit(0); }
            if (filename!=null&&!File.Exists(filename)) { MessageBox.Show("Error: " + filename + "not found. Quitting execution."); Environment.Exit(0); }
        }
    }
}
