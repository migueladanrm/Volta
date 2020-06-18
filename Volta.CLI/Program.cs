using McMaster.Extensions.CommandLineUtils;
using System.Reflection;
using Volta.CLI.Commands;
using static System.Console;

namespace Volta.CLI
{
    class Program
    {
        static int Main(string[] args) {
            WriteLine($"Volta CLI | Version {Assembly.GetExecutingAssembly().GetName().Version}\n==============================\n");
            return CommandLineApplication.Execute<VoltaCommand>(args);
        }
    }
}