using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Volta.Compiler;
using System.IO;
using System.Linq;

namespace Volta.Tests
{
    [TestClass]
    public class VoltaCompilerTests
    {
        private Dictionary<string, string> codeSamples;

        [TestInitialize]
        public void Initialize() {
            codeSamples = new Dictionary<string, string>();

            Directory.GetFiles("SampleFiles").ToList().ForEach(file => {
                var fileName = new FileInfo(file).Name;
                codeSamples.Add(fileName, File.ReadAllText(file));

                Console.WriteLine($"Loaded '{fileName}'.");
            });

        }

        [TestMethod]
        public void CheckCodeErrors() {
            Console.WriteLine("Checking samples...");

            //foreach (var kv in codeSamples) {
            //    Console.WriteLine($"File: {kv.Key}");
            //    var errors = Controller.Check(kv.Value);
            //    foreach (var vpe in errors) {
            //        Console.Error.WriteLine($"Error at Ln {vpe.Line} Col {vpe.Column}:\n\t{vpe.Message}");
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
