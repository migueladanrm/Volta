using System;

namespace Volta.Compiler.CodeGeneration.Nabla
{
    /// <summary>
    /// Herramientas auxiliares.
    /// </summary>
    internal static class NablaHelper
    {
        internal static Type ParseType(string text) {
            switch (text) {
                default:
                    return null;
                case "int":
                    return typeof(int);
                case "int[]":
                    return typeof(int[]);
                case "char":
                    return typeof(char);
                case "char[]":
                    return typeof(char[]);
                case "float":
                    return typeof(float);
                case "float[]":
                    return typeof(float[]);
                case "bool":
                    return typeof(bool);
                case "bool[]":
                    return typeof(bool[]);
                case "string":
                    return typeof(string);
                case "string[]":
                    return typeof(string[]);
            }
        }
    }
}