using Antlr4.Runtime;

namespace Volta.Compiler
{
    /// <summary>
    /// Definición abstracta para errores del compilador Volta.
    /// </summary>
    public abstract class VoltaCompilerError
    {
        /// <summary>
        /// Inicializa una instancia de <see cref="VoltaCompilerError"/>.
        /// </summary>
        public VoltaCompilerError() {

        }

        /// <summary>
        /// Inicializa una instancia de <see cref="VoltaCompilerError"/>.
        /// </summary>
        /// <param name="token">Token.</param>
        /// <param name="line">Número de línea.</param>
        /// <param name="col">Número de columna.</param>
        /// <param name="message">Mensaje de error.</param>
        public VoltaCompilerError(IToken token, int line, int col, string message) {
            Token = token;
            Line = line;
            Column = col;
            Message = message;
        }

        /// <summary>
        /// Token.
        /// </summary>
        public IToken Token { get; protected set; }

        /// <summary>
        /// Número de línea.
        /// </summary>
        public int Line { get; protected set; }

        /// <summary>
        /// Número de columna.
        /// </summary>
        public int Column { get; protected set; }

        /// <summary>
        /// Mensaje de error.
        /// </summary>
        public string Message { get; protected set; }
    }
}