using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Volta.Compiler.CodeAnalysis;
using Volta.Compiler.CodeGeneration.Delta;

namespace Volta.Compiler
{
    public class Controller
    {
        public static Tuple<IParseTree, List<VoltaCompilerError>> Check(string text)
        {
            ICharStream charStream = CharStreams.fromstring(text);
            VoltaScanner scanner = new VoltaScanner(charStream);
            ITokenStream tokens = new CommonTokenStream(scanner);

            VoltaParser parser = new VoltaParser(tokens);

            VoltaParserErrorListener errorListener = new VoltaParserErrorListener();

            parser.RemoveErrorListeners();

            parser.AddErrorListener(errorListener);

            IParseTree tree = parser.program();

            //Debug.WriteLine("Probando el debugger");
            //Debug.WriteLine((tree as ParserRuleContext).ToStringTree(parser));

            ContextualAnalysis contextualAnalysis = new ContextualAnalysis();

            List<VoltaCompilerError> errors = new List<VoltaCompilerError>();
            errors.AddRange(errorListener.Errors);
            
            //if(errors.Count == 0)
            {
                contextualAnalysis.Visit(tree);
                errors.AddRange(contextualAnalysis.Errors);
            }

            return new Tuple<IParseTree, List<VoltaCompilerError>>(tree, errors) ;
        }
    }
}
