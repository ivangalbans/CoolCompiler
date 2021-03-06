﻿using Antlr4.Runtime;
using Cool.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cool.AST
{
    public class DispatchImplicitNode : DispatchNode
    {
        public DispatchImplicitNode(ParserRuleContext context) : base(context)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
