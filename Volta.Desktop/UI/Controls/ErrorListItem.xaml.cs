using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Volta.Compiler;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for ErrorListItem.xaml
    /// </summary>
    public partial class ErrorListItem : UserControl
    {
        public VoltaParserError ParserError { get; private set; }

        public ErrorListItem() {
            InitializeComponent();
        }

        public ErrorListItem(VoltaParserError error) {
            InitializeComponent();
            DataContext = this;

            ParserError = error;
            Defaults();
        }

        private void Defaults() {
            TxtErrorMessage.Text = ParserError.Message;
        }
    }
}
