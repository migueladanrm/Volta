using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Volta.Compiler
{
    public class Controller
    {
        public static List<VoltaParserError> check(string text)
        {
            ICharStream charStream = CharStreams.fromstring(text);
            VoltaScanner scanner = new VoltaScanner(charStream);
            ITokenStream tokens = new CommonTokenStream(scanner);

            VoltaParser parser = new VoltaParser(tokens);

            VoltaParserErrorListener errorListener = new VoltaParserErrorListener();

            parser.AddErrorListener(errorListener);

            IParseTree tree = parser.program();

            return errorListener.GetErrors();
        }
    }
}
