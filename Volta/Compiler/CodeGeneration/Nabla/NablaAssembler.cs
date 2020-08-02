using Antlr4.Runtime;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
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
            Console.WriteLine("Cargando código en memoria...");

            an = new AssemblyName(name ?? Guid.NewGuid().ToString());
#if NET48
            ab = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndSave);
            mb = ab.DefineDynamicModule(an.Name, $"{an.Name}.exe");
#endif
            nv = new NablaVisitor(ref mb);

            var lexer = new VoltaScanner(CharStreams.fromStream(code));
            var tokens = new CommonTokenStream(lexer);
            var parser = new VoltaParser(tokens);
            var tree = parser.program();
            var contextualAnalysis = new ContextualAnalysis();
            contextualAnalysis.Visit(tree);

            Console.WriteLine("Ensamblando código...");

            nv.Visit(tree);

            nv.rootType.CreateType();
#if NET48
            ab.SetEntryPoint(nv.mainMethod);
#endif

            Console.WriteLine("Código ensamblado correctamente.");
        }

#if NET48
        public void BuildProgram(string output) {
            if (!output.EndsWith(".exe"))
                output += ".exe";

            var outputFile = new FileInfo(output);
            if (!Directory.Exists(outputFile.DirectoryName))
                Directory.CreateDirectory(outputFile.DirectoryName);

            Console.WriteLine("Generando ejecutable...");
            ab.Save(outputFile.Name);

            File.Move(outputFile.Name, outputFile.FullName);

            Console.WriteLine("Ejecutable generado correctamente.");
        }
#endif

    }
}