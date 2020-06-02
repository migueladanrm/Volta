using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;

namespace Volta.Compiler
{
    public abstract class Symbol
    {
        public Symbol() {

        }

        public Symbol(string name, int level) {
            Level = level;
            Name = name;
        }

        public int Level { get; set; }
        public string Name { get; set; }
        public IToken Token { get; private set; }

    }
}