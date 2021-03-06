﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.CodeGeneration.MIPSCode;

namespace Cool.CodeGeneration.IntermediateCode.ThreeAddressCode
{
    public class CallLabelLine : CodeLine
    {
        public LabelLine Method { get; }
        public int Result { get; }
        public CallLabelLine(LabelLine method, int result_variable = -1)
        {
            Method = method;
            Result = result_variable;
        }

        public override string ToString()
        {
            if(Result == -1)
                return $"Call {Method.Label};";
            else
                return $"t{Result} = Call {Method.Label};";
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class CallAddressLine : CodeLine
    {
        public int Address { get; }
        public int Result { get; }
        public CallAddressLine(int address, int result_variable = -1)
        {
            Address = address;
            Result = result_variable;
        }

        public override string ToString()
        {
            if(Result == -1)
                return $"Call t{Address};";
            else
                return $"t{Result} = Call t{Address};";
        }

        public override void Accept(ICodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
