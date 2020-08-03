using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Volta.Compiler.CodeGeneration.Nabla;

namespace Volta.CLI.Commands
{
    [Command(Name = "volta")]
    class VoltaCommand : ICommand
    {
        [Required]
        [Option(CommandOptionType.SingleValue, LongName = "input", ShortName = "i", Description = "The input code file.")]
        public string OptionInput { get; set; }

        [Required]
        [Option(CommandOptionType.SingleValue, LongName ="output", ShortName ="o", Description ="The output executable file.")]
        public string OptionOutput { get; set; }

        public int OnExecute() {
            try {
                Console.WriteLine($"Inicializando proceso de compilación para '{OptionInput}'...");

                if (!OptionOutput.EndsWith(".exe"))
                    OptionOutput += ".exe";

                var file = new FileInfo(OptionInput);
                var nabla = new NablaAssembler(
                    new MemoryStream(File.ReadAllBytes(file.FullName)),
                    file.Name.Substring(0, file.Name.LastIndexOf('.'))
                    );

                nabla.BuildProgram(OptionOutput);

                Console.WriteLine($"{OptionInput} -> {OptionOutput}");
                Console.WriteLine("=========================\nCompilación exitosa.");
                return 0;
            } catch (Exception e) {
                Console.Error.WriteLine($"Ha ocurrido un error en la compilación: {e.Message}");
                return -1;
            }
        }
    }
}