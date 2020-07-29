using Antlr4.Runtime;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

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
            ab = currentDom.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndSave);
            Console.WriteLine("Assembly Builder");
            Console.WriteLine(ab.FullName);
#endif
            mb = ab.DefineDynamicModule(an.Name);
            Console.WriteLine("Module Builder");
            nv = new NablaVisitor(ref mb);

            var lexer = new VoltaScanner(CharStreams.fromStream(code));
            var tokens = new CommonTokenStream(lexer);
            var parser = new VoltaParser(tokens);
            var tree = parser.program();

            nv.Visit(tree);
        }

#if NET48
        public void BuildProgram() {
            ab.Save($"{an.Name}.exe");
        }
#endif
    }
}