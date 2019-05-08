//Program.cs
//Serves as the entry point for the program.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace armsim
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
           
            Options opts = new Options(args);
            opts.ParseOptions();
            if (opts.exec && opts.filename != null)
            {
                Computer exec = new Computer(opts.mem);
                exec.setFileName(opts.filename);
                exec.setTrace();
                exec.resetComputer();
              
                exec.Run();
                Environment.Exit(0);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(args));
        }
    }
}
