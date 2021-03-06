﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.CodeGeneration.MIPSCode;

namespace Cool.CodeGeneration.IntermediateCode.ThreeAddressCode
{
    public class ReturnLine : CodeLine
    {
        public int Variable { get; }

        public ReturnLine(int variable = -1)
        {
            Variable = variable;
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "Return " + (Variable == -1 ? "" : "t" + Variable) + ";\n";
        }
    }
}
