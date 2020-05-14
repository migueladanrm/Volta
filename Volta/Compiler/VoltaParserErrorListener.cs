using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Sharpen;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Volta.Compiler
{
    public class VoltaParserErrorListener : IParserErrorListener
    {
        List<VoltaParserError> errors;
        public VoltaParserErrorListener()
        {
            errors = new List<VoltaParserError>();
        }
        
        public void ReportAmbiguity(Parser recognizer, DFA dfa, int startIndex, int stopIndex, bool exact, BitSet ambigAlts, ATNConfigSet configs)
        {
        }

        public void ReportAttemptingFullContext(Parser recognizer, DFA dfa, int startIndex, int stopIndex, BitSet conflictingAlts, SimulatorState conflictState)
        {
        }

        public void ReportContextSensitivity(Parser recognizer, DFA dfa, int startIndex, int stopIndex, int prediction, SimulatorState acceptState)
        {
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            if(recognizer is VoltaParser)
            {
                msg = "Parser error: " + msg;
            }
            else if (recognizer is VoltaScanner)
            {
                msg = "Scanner error: " + msg;
            }
            this.errors.Add(new VoltaParserError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e));
        }

        public List<VoltaParserError> GetErrors() { return this.errors; }
    }
}
