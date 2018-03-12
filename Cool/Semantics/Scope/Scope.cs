﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cool.Semantics
{
    class Scope : IScope
    {
        /// <summary>
        /// Information relative to variables.
        /// </summary>
        Dictionary<string, TypeInfo> _variables = new Dictionary<string, TypeInfo>();

        /// <summary>
        /// Information relative to variables.
        /// </summary>
        Dictionary<string, (TypeInfo[] Args, TypeInfo ReturnType)> _functions = new Dictionary<string, (TypeInfo[], TypeInfo)>();

        /// <summary>
        /// Information relative to types in the current scope.
        /// </summary>
        public static Dictionary<string, TypeInfo> DeclaredTypes;

        public IScope Parent { get; set; }
        public TypeInfo Type { get; set; }

        static Scope()
        {
            DeclaredTypes = new Dictionary<string, TypeInfo>();
            DeclaredTypes.Add("Object", new TypeInfo { Name = "Object", Parent = null });
            DeclaredTypes.Add("Int", new TypeInfo { Name = "Int", Parent = DeclaredTypes["Object"] });
            DeclaredTypes.Add("Bool", new TypeInfo { Name = "Bool", Parent = DeclaredTypes["Object"] });
            DeclaredTypes.Add("String", new TypeInfo { Name = "String", Parent = DeclaredTypes["Object"] });
            DeclaredTypes.Add("IO", new TypeInfo { Name = "IO", Parent = DeclaredTypes["Object"] });
        }

        public bool IsDefined(string name, out TypeInfo type)
        {
            return _variables.TryGetValue(name, out type) ||
                    Parent.IsDefined(name, out type);
        }

        public bool IsDefined(string name, TypeInfo[] args, out TypeInfo type)
        {
            type = TypeInfo.NULL;
            if(_functions.ContainsKey(name) && _functions[name].Args.Length == args.Length)
            {
                bool ok = true;
                for (int i = 0; i < args.Length; ++i)
                    if (!(args[i] <= _functions[name].Args[i]))
                        ok = false;
                if(ok)
                {
                    type = _functions[name].ReturnType;
                    return true;
                }
            }

            return Parent.IsDefined(name, args, out type) ||
                   Type.Parent.ClassReference.Scope.IsDefined(name, args, out type);
        }

        public bool IsDefinedType(string name, out TypeInfo type)
        {
            throw new NotImplementedException();
        }

        public bool Define(string name, TypeInfo type)
        {
            throw new NotImplementedException();
        }

        public bool Define(string name, TypeInfo[] args, TypeInfo type)
        {
            throw new NotImplementedException();
        }

        public bool Change(string name, TypeInfo type)
        {
            throw new NotImplementedException();
        }

        public IScope CreateChild()
        {
            throw new NotImplementedException();
        }

        #region
        private static NullScope nullScope = new NullScope();

        public static NullScope NULL => nullScope;

        internal class NullScope : IScope
        {
            public IScope Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public TypeInfo Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public bool Change(string name, TypeInfo type)
            {
                return false;
            }

            public IScope CreateChild()
            {
                throw new NotImplementedException();
            }

            public bool Define(string name, TypeInfo type)
            {
                return false;
            }

            public bool Define(string name, TypeInfo[] args, TypeInfo type)
            {
                return false;
            }

            public bool IsDefined(string name, out TypeInfo type)
            {
                type = TypeInfo.NULL;
                return false;
            }

            public bool IsDefined(string name, TypeInfo[] args, out TypeInfo type)
            {
                type = TypeInfo.NULL;
                return false;
            }

            public bool IsDefinedType(string name, out TypeInfo type)
            {
                type = TypeInfo.NULL;
                return false;
            }
        }
        #endregion

    }
}