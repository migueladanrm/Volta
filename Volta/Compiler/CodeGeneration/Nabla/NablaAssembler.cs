using Antlr4.Runtime;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Volta.Compiler.CodeAnalysis;

namespace Volta.Compiler.CodeGeneration.Nabla
{
    public class NablaAssembler
    {
        private AssemblyName an;
        private AssemblyBuilder ab;
        private ModuleBuilder mb;
        private NablaVisitor nv;

        public NablaAssembler(Stream code, string name = null) {

            AppDomain currentDom = Thread.GetDomain();

            an = new AssemblyName(name ?? Guid.NewGuid().ToString());
#if NET48
            ab = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndSave);
            mb = ab.DefineDynamicModule(an.Name,$"{an.Name}.exe");
#endif
            nv = new NablaVisitor(ref mb);

            var lexer = new VoltaScanner(CharStreams.fromStream(code));
            var tokens = new CommonTokenStream(lexer);
            var parser = new VoltaParser(tokens);
            var tree = parser.program();
            var contextualAnalysis = new ContextualAnalysis();
            contextualAnalysis.Visit(tree);

            nv.Visit(tree);
#if NET48
            ab.SetEntryPoint(nv.mainMethod);
#endif
        }

#if NET48
        public void BuildProgram() {
            ab.Save($"{an.Name}.exe");
        }
#endif
    }
}