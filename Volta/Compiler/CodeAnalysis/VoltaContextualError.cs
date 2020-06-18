using Antlr4.Runtime;

namespace Volta.Compiler.CodeAnalysis
{
    /// <summary>
    /// Error de análisis contextual de compilador Volta.
    /// </summary>
    public class VoltaContextualError : VoltaCompilerError
    {
        /// <summary>
        /// Inicializa una instancia de <see cref="VoltaContextualError"/>.
        /// </summary>
        /// <param name="token">Token.</param>
        /// <param name="line">Número de línea.</param>
        /// <param name="row">Número de columna.</param>
        /// <param name="message">Mensaje de error.</param>
        public VoltaContextualError(IToken token, int line, int row, string message)
            : base(token, line, row, message) {

        }
    }
}