using System.Windows.Controls;
using Volta.Compiler;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for ErrorListItem.xaml
    /// </summary>
    public partial class ErrorListItem : UserControl
    {
        public VoltaCompilerError ParserError { get; private set; }

        public ErrorListItem() {
            InitializeComponent();
        }

        public ErrorListItem(VoltaCompilerError error) {
            InitializeComponent();
            DataContext = this;

            ParserError = error;
            Defaults();
        }

        private void Defaults() {
            TxtErrorMessage.Text = ParserError.Message;
            TxtErrorLineNumber.Text = ParserError.Line.ToString();
        }
    }
}
