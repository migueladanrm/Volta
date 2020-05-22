using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Volta.Compiler
{
    public class VoltaParserError
    {
        public VoltaParserError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
            Output = output;
            Recognizer = recognizer;
            OffendingSymbol = offendingSymbol;
            Line = line;
            CharPositionInLine = charPositionInLine;
            Message = msg;
            Exception = e;
        }

        public TextWriter Output { get; private set; }
        public IRecognizer Recognizer { get; private set; }
        public IToken OffendingSymbol { get; private set; }
        public int Line { get; private set; }
        public int CharPositionInLine { get; private set; }
        public string Message { get; private set; }
        public RecognitionException Exception { get; private set; }
    }
}