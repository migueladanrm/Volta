using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Volta.Compiler.IdentificationTable
{
    public class VoltaContextualError : VoltaCompilerError
    {
        public VoltaContextualError(IToken token, int line, int charPositionInLine, string msg)
        {
            Token = token;
            Line = line;
            CharPositionInLine = charPositionInLine;
            Message = msg;
        }
    }
}
