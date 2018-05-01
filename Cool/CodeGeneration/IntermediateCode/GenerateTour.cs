﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.AST;
using Cool.Semantics;
using Cool.CodeGeneration.IntermediateCode.ThreeAddressCode;

namespace Cool.CodeGeneration.IntermediateCode
{
    class GenerateTour : IVisitor
    {
        IIntermediateCode IntermediateCode;
        IScope Scope;

        Dictionary<string,int> variable_link;
        ClassNode current_class;
        int variable_counter;
        int jump_labal_counter;

        public IIntermediateCode GetIntermediateCode(ProgramNode node, IScope scope)
        {
            variable_counter = 0;
            jump_labal_counter = 0;
            Scope = scope;
            IntermediateCode = new IntermediateCode(scope);
            node.Accept(this);
            return IntermediateCode;
        }

        public void Visit(ProgramNode node)
        {
            List<ClassNode> sorted = new List<ClassNode>();
            sorted.AddRange(node.Classes);
            sorted.Sort((x,y) => (Scope.GetType(x.TypeClass.Text) <= Scope.GetType(y.TypeClass.Text) ? 1 : -1));
            
            foreach (var c in sorted)
            {
                c.Accept(this);
            }

        }

        public void Visit(ClassNode node)
        {
            current_class = node;

            foreach (var f in node.FeatureNodes)
            {
                f.Accept(this);
            }
        }

        public void Visit(MethodNode node)
        {
            IntermediateCode.DefineMethod(current_class.TypeClass.Text, node.Id.Text);
            

        }

        public void Visit(AttributeNode node)
        {
            IntermediateCode.DefineAttribute(current_class.TypeClass.Text, node.Formal.Id.Text);
            LabelLine l = IntermediateCode.AddConstructorCallAttribute(current_class.TypeClass.Text, node.Formal.Id.Text);
            IntermediateCode.AddCodeLine(l);

            IntermediateCode.AddCodeLine(new ParamLine(variable_counter));
            int this_var = variable_counter;
            variable_counter++;

            int t1 = variable_counter;
            node.AssignExp.Accept(this);

            var a = new AssignmentVariableToMemoryLine(this_var, t1, IntermediateCode.GetAttributeOffset(current_class.TypeClass.Text, node.Formal.Id.Text));
            IntermediateCode.AddCodeLine(a);

            IntermediateCode.AddCodeLine(new ReturnLine(-1));
        }

        public void Visit(IntNode node)
        {
            IntermediateCode.AddCodeLine(new AssignmentConstantToVariableLine(variable_counter, node.Value));
        }

        public void Visit(ArithmeticOperation node)
        {
            int t = variable_counter;
            ++variable_counter;
            int t1 = variable_counter;
            node.LeftOperand.Accept(this);

            ++variable_counter;
            int t2 = variable_counter;
            node.RightOperand.Accept(this);

            IntermediateCode.AddCodeLine(new ArithmeticLine(t, t1, t2, node.Symbol));
        }

        public void Visit(AssignmentNode node)
        {
            throw new NotImplementedException();
        }
        
        public void Visit(BoolNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(CaseNode node)
        {
            throw new NotImplementedException();
        }
        

        public void Visit(ComparisonOperation node)
        {
            throw new NotImplementedException();
        }

        public void Visit(DispatchExplicitNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(DispatchImplicitNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(EqualNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(FormalNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(IdentifierNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(IfNode node)
        {
            throw new NotImplementedException();
        }
        
        public void Visit(IsVoidNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(LetNode node)
        {
            throw new NotImplementedException();
        }

        

        public void Visit(NegNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NewNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NotNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(SelfNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(SequenceNode node)
        {
            foreach (var s in node.Sequence)
            {
                s.Accept(this);
            }
        }

        public void Visit(StringNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(VoidNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(WhileNode node)
        {
            throw new NotImplementedException();
        }
    }
}
