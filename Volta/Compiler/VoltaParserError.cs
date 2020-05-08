using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Volta.Compiler
{
    public class VoltaParserError
    {
        public readonly TextWriter output;
        public readonly IRecognizer recognizer;
        public readonly IToken offendingSymbol;
        public readonly int line;
        public readonly int charPositionInLine;
        public readonly string msg;
        public readonly RecognitionException e;

        public VoltaParserError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            this.output = output;
            this.recognizer = recognizer;
            this.offendingSymbol = offendingSymbol;
            this.line = line;
            this.charPositionInLine = charPositionInLine;
            this.msg = msg;
            this.e = e;
        }
    }
}
