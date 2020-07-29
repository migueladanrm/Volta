using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volta.Compiler;
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

        //[Option(CommandOptionType.NoValue, LongName = "check", ShortName = "c", Description = "Sets if a error check will run before build.")]
        //public bool OptionCheckErrors { get; set; }

        public int OnExecute() {
            try {
                var file = new FileInfo(OptionInput);
                //var text = File.ReadAllText(OptionInput);
                //if (OptionCheckErrors) {
                //    var errors = Controller.Check(text);
                //    foreach (var vpe in errors) {
                //        Console.Error.WriteLine($"Error at Ln {vpe.Line} Col {vpe.Column}:\n\t{vpe.Message}");
                //    }
                //}

                var nabla = new NablaAssembler(
                    new MemoryStream(File.ReadAllBytes(file.FullName)),
                    file.Name.Substring(0, file.Name.LastIndexOf('.'))
                    );

                nabla.BuildProgram();

                Console.WriteLine("Build successful.");

                return 0;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
    }
}