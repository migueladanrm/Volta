using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volta.Compiler;
using System.IO;

namespace Volta.CLI.Commands
{
    [Command(Name = "volta")]
    class VoltaCommand : ICommand
    {
        [Required]
        [Option(CommandOptionType.SingleValue, LongName = "file", ShortName = "f", Description = "The repository unique name.")]
        public string OptionFile { get; set; }

        public int OnExecute() {
            try {
                var text = File.ReadAllText(OptionFile);
                var errors = Controller.Check(text);
                foreach (var vpe in errors) {
                    Console.Error.WriteLine($"Error at Ln {vpe.Line} Col {vpe.CharPositionInLine}:\n\t{vpe.Message}");
                }
                return 0;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
    }
}