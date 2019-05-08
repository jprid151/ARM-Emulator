using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace armsim
{ //abstract class that models DataProcessingInstructions and contains logic to return all inheriting instructions when created by Decode
    abstract class DataProcessingInstruction: Instruction
    {
        public int regshift;
        public DataProcessingInstruction(uint nval, ref Computer comp):base(nval,ref comp) { value = nval;computer = comp; }
        //decode the instruction from <value1> and <comp> and return the appropriate type
        static public DataProcessingInstruction Decode(uint value1, ref Computer comp)
        {


            uint val = Memory.ExtractBits(value1, 21, 24);
            switch (val)
            {
                case 4:
                    //add instruction
                    AddSubInstruction addins = new AddSubInstruction(value1, comp);
                    addins.regshift = -1;
                    addins.type = 0;
                    uint amreg = Memory.ExtractBits(value1, 12, 15);
                    addins.dest = new Operand(amreg, 0, 0, 0, true);
                    amreg = Memory.ExtractBits(value1, 16, 19);
                    addins.source = new Operand(amreg, 0, 0, 0, true);
                    bool aisImm = Memory.TestBit(25, value1);
                    //immediate value
                    if (aisImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        addins.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            addins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            addins.regshift = (int) shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            addins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return addins;
       
                case 2:
                    //sub instruction
                    AddSubInstruction subins = new AddSubInstruction(value1, comp);
                    subins.regshift = -1;
                    subins.type = 1;
                    uint smreg = Memory.ExtractBits(value1, 12, 15);
                    subins.dest = new Operand(smreg, 0, 0, 0, true);
                    smreg = Memory.ExtractBits(value1, 16, 19);
                    subins.source = new Operand(smreg, 0, 0, 0, true);
                    bool sisImm = Memory.TestBit(25, value1);
                    //immediate value
                    if (sisImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        subins.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            subins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            subins.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            subins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return subins;
                case 0:
                    CompInstruction ains = new CompInstruction(value1, comp);
                    ains.regshift = -1;
                    ains.type = 0;
                    uint areg = Memory.ExtractBits(value1, 12, 15);
                    ains.dest = new Operand(areg, 0, 0, 0, true);
                    areg = Memory.ExtractBits(value1, 16, 19);
                    ains.source = new Operand(areg, 0, 0, 0, true);
                    bool aImm = Memory.TestBit(25, value1);
                    if (aImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        ains.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            ains.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            ains.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            ains.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return ains;
                case 14:
                    BitClearInstruction bins = new BitClearInstruction(value1, comp);
                    bins.regshift = -1;
                    uint breg = Memory.ExtractBits(value1, 12, 15);
                    bins.dest = new Operand(breg, 0, 0, 0, true);
                    breg = Memory.ExtractBits(value1, 16, 19);
                    bins.source = new Operand(breg, 0, 0, 0, true);
                    bool bImm = Memory.TestBit(25, value1);
                    if (bImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        bins.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            bins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            bins.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            bins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return bins;
                //and instruction
                case 10:
                    CompInstruction cmpins = new CompInstruction(value1, comp);
                    cmpins.regshift = -1;
                    cmpins.type = 3;
                    uint cmpreg = Memory.ExtractBits(value1, 16, 19);
                    cmpins.dest = new Operand(cmpreg, 0, 0, 0, true);
                   
                    bool cmpImm = Memory.TestBit(25, value1);
                    if (cmpImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        cmpins.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            cmpins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            cmpins.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            cmpins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return cmpins;
                //or instruction
                case 12:
                    CompInstruction cins = new CompInstruction(value1, comp);
                    cins.regshift = -1;
                    cins.type = 1;
                    uint creg = Memory.ExtractBits(value1, 12, 15);
                    cins.dest = new Operand(creg, 0, 0, 0, true);
                    amreg = Memory.ExtractBits(value1, 16, 19);
                    cins.source = new Operand(amreg, 0, 0, 0, true);
                    bool cImm = Memory.TestBit(25, value1);
                    if (cImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        cins.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            cins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            cins.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            cins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return cins;
                //rsb instruction
                case 3:
                    AddSubInstruction rsubins = new AddSubInstruction(value1, comp);
                    rsubins.regshift = -1;
                    rsubins.type = 2;
                    uint srmreg = Memory.ExtractBits(value1, 12, 15);
                    rsubins.dest = new Operand(srmreg, 0, 0, 0, true);
                    amreg = Memory.ExtractBits(value1, 16, 19);
                    rsubins.source = new Operand(amreg, 0, 0, 0, true);
                    bool risImm = Memory.TestBit(25, value1);
                    //immediate value
                    if (risImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        rsubins.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            rsubins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            rsubins.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            rsubins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return rsubins;
                case 15:
                    //mvn
                    MovInstruction mvninstruction = new MovInstruction(value1, comp);
                    mvninstruction.regshift = -1;
                    mvninstruction.mvn = true;

                    uint mreg = Memory.ExtractBits(value1, 12, 15);
                    mvninstruction.dest = new Operand(mreg, 0, 0, 0, true);
                    bool misImm = Memory.TestBit(25, value1);
                 
                    //immediate value
                    if (misImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        mvninstruction.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(val, 0, 3);
                            mvninstruction.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            mvninstruction.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            mvninstruction.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return mvninstruction;
                //eor instruction
                case 1:
                    CompInstruction eins = new CompInstruction(value1, comp);
                    eins.regshift = -1;
                    eins.type = 2;
                    uint ereg = Memory.ExtractBits(value1, 12, 15);
                    eins.dest = new Operand(ereg, 0, 0, 0, true);
                    areg = Memory.ExtractBits(value1, 16, 19);
                    eins.source = new Operand(areg, 0, 0, 0, true);
                    bool eImm = Memory.TestBit(25, value1);
                    if (eImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        eins.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            eins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            eins.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            eins.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return eins;
                //mov instruciton
                case 13:
                    MovInstruction instruction = new MovInstruction(value1, comp);
                    instruction.sinstruct = Memory.TestBit(20, value1);
                    instruction.regshift = -1;
                    uint reg = Memory.ExtractBits(value1, 12, 15);
                    instruction.dest = new Operand(reg, 0, 0,0, true);
                    bool isImm = Memory.TestBit(25, value1);
                    //immediate value
                    if (isImm)
                    {
                        uint rotate = Memory.ExtractBits(value1, 8, 11);
                        uint immediate = Memory.ExtractBits(value1, 0, 7);
                        instruction.operand2 = new Operand(immediate, 0, 0, rotate, false);

                    }
                    //register/shifted register
                    else
                    {
                        // register/immediate shifted register
                        bool isReg = Memory.TestBit(4, value1);
                        if (!isReg)
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 7, 11);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            instruction.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                        //register shifted register
                        else
                        {
                            uint shiftAmt = Memory.ExtractBits(value1, 8, 11);
                            instruction.regshift = (int)shiftAmt;
                            shiftAmt = comp.getReg((int)shiftAmt);
                            uint shiftType = Memory.ExtractBits(value1, 5, 6);
                            uint RM = Memory.ExtractBits(value1, 0, 3);
                            instruction.operand2 = new Operand(RM, shiftAmt, shiftType, 0, true);
                        }
                    }
                    return instruction;
                default: break;

            }
            
            return null;
        }

    }
}
