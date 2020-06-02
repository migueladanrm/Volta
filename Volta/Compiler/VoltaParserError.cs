using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Volta.Compiler
{
    public class VoltaParserError : VoltaCompilerError
    {
        public VoltaParserError(TextWriter output, IRecognizer recognizer, IToken token, int line, int charPositionInLine, string msg, RecognitionException e) {
            Output = output;
            Recognizer = recognizer;
            Token = token;
            Line = line;
            CharPositionInLine = charPositionInLine;
            Message = msg;
            Exception = e;
        }

        public TextWriter Output { get; private set; }
        public IRecognizer Recognizer { get; private set; }
        public RecognitionException Exception { get; private set; }
    }
}