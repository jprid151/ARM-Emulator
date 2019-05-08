#Project Header

Jonathon Pridgeon, CPS 310

Submission Date: 11/7/18 Time Spent: 26 Hours

#Features
A Level Features completed (CMP,branch instructions implemented along with using MRS/MSR and OS to drive input/output)

#Prerequisites:
This program runs on Windows 10. It requires Visual Studio 2017 to run. 

#Build/Test:
To build the project, open the "armsim.csproj" file in Visual Studio. Then, click the "Build" menu and select "Build armsim." The project will then build. To run the unit tests, run the project with the "--test" option from the command line. Test output will be sent to "testlog.txt" 

#Configuration:

To enable/disable logging, run the app with the command line option "--log." Logging messages are sent to "mylogfile.txt"

To start the application with a file pre-loaded, run the exe as "armsim.exe --load filename". The app will then start with "filename" pre-loaded into simulated memory. 

To run the application from the command line, run the exe with the "--exec" and "--load filename" options, which will run the app and produce a trace in trace.log.

Use the "-traceall" command line option to view instructions run when the simulator is in SVC/IRQ modes. Otherwise, these instructions will not appear in the trace file.

Resources contains icons for the application's Run, Step etc. buttons. 

Sample.txt contains assembly for the disassembly panel

#User Guide:

Run the loader by typing "armsim.exe." Use the command line option "--mem memsize" to specify the amount of ram you want to use, up to 1,000,000 bytes, or 1 MB. 

The application will display. Select a file with the "load" menu option under "File." Then, the app will display the file's disassembly, memory, and register status. You can see the stack and flags by clicking on the other tabs above "Registers." 

Select "Run" to start running. The app will run through the file's memory and stop when it reaches the end or when you click "Stop." 

Select "Step" to do one step through memory.

Select "Breakpoint" to add a breakpoint to the app's memory. When you hit "Run", the app will stop on a breakpoint that you supply.

Hit "Reset" to reset the application as if it had just loaded the file. Breakpoints will be kept through a reset.

Tracing will go to a file named "Trace.log". You can toggle tracing with the menu options under "File" or with the keyboard short cut.

The app supports the following keyboard shortcuts:

Load File: Ctrl-O
F5: Run
Single-step: F10
Stop: Ctrl-Q
Toggle Trace: Ctrl-T
Reset: Ctrl-R
Toggle Breakpoints: Ctrl-B

#Instruction Implementation

##C level
Add/Sub/Rsb instructions
Mov/MVN instructions
Bic instructions
And/Eor/Orr instructions
CMP Instructions
Branch Instructions

##B Level
LDR/STR (preindex, with/without writeback)
LDM/STM (with/without writeback)


##A Level
MRS/MSR Instructions
Movs Instruction

#Bug Report:

Entering something when the program is not asking for it with a call to getline may result in a crash. 

Instructions with the .word directive, i.e. .word foobar may have a strange disassembly, due to there not being a reliable way to differentiate them from normal instructions that I could tell.

#Project Journal:

https://docs.google.com/document/d/1iNnhyYrKAkhJflg0kzPGD9T9Bhy-PcvM_0gXs5UkT0w/edit?usp=sharing

#Academic Integrity Statement:

"By affixing my signature below, I certify that the accompanying work represents my own intellectual effort. Furthermore, I have received no outside help other than what is documented below."

Jonathon Pridgeon