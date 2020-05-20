using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            parser.RemoveErrorListeners();

            parser.AddErrorListener(errorListener);

            IParseTree tree = parser.program();

            Debug.WriteLine(":D");
            Debug.WriteLine((tree as ParserRuleContext).ToStringTree());
           
            
            

            return errorListener.GetErrors();
        }
    }
}
