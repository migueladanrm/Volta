using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Volta.Compiler
{
    public abstract class VoltaCompilerError
    {

        public IToken Token { get; protected set; }
        public int Line { get; protected set; }
        public int CharPositionInLine { get; protected set; }
        public string Message { get; protected set; }
    }
}
