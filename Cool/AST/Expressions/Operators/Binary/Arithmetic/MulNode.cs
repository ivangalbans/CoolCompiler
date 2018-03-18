﻿using Antlr4.Runtime;
using Cool.Semantics;
using System.Collections.Generic;

namespace Cool.AST
{
    class MulNode : ArithmeticOperation
    {
        public MulNode(ParserRuleContext context) : base(context)
        {
        }

        public override string Symbol => "+";

        public override void Accept(IVisitor visitor, IScope scope, ICollection<SemanticError> errors)
        {
            visitor.Visit(this, scope, errors);
        }
    }
}
