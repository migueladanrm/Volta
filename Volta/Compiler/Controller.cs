using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Volta.Compiler.CodeAnalysis;

namespace Volta.Compiler
{
    public class Controller
    {
        public static List<VoltaCompilerError> Check(string text)
        {
            ICharStream charStream = CharStreams.fromstring(text);
            VoltaScanner scanner = new VoltaScanner(charStream);
            ITokenStream tokens = new CommonTokenStream(scanner);

            VoltaParser parser = new VoltaParser(tokens);

            VoltaParserErrorListener errorListener = new VoltaParserErrorListener();

            parser.RemoveErrorListeners();

            parser.AddErrorListener(errorListener);

            IParseTree tree = parser.program();

            //Debug.WriteLine(":D");
            //Debug.WriteLine((tree as ParserRuleContext).ToStringTree(parser));

            ContextualAnalysis contextualAnalysis = new ContextualAnalysis();

            List<VoltaCompilerError> errors = new List<VoltaCompilerError>();
            errors.AddRange(errorListener.Errors);
            
            //if(errors.Count == 0)
            {
                contextualAnalysis.Visit(tree);
                errors.AddRange(contextualAnalysis.Errors);
            }

           
            
            

            return errors;
        }
    }
}
