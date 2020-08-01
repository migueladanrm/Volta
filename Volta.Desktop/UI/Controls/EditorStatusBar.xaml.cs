using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Volta.Compiler;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for EditorStatusBar.xaml
    /// </summary>
    public partial class EditorStatusBar : UserControl
    {
        internal const string TAB_ERRORLIST = "tab.errorlist";
        internal const string TAB_OUTPUT = "tab.output";
        internal const string TAB_CONSOLE = "tab.console";

        public event Action<string> RequestTab;

        public EditorStatusBar() {
            InitializeComponent();
        }

        public void UpdateEditorCaretPositions(Caret caret) {
            TxtEditorCaret.Text = $"Ln {caret.Line} Col {caret.Column}";
        }

        private void Btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            try {
                var tag = (sender as FrameworkElement).Tag.ToString();
                RequestTab?.Invoke(tag);
            } catch { }
        }

        public void UpdateErrorsCount(List<VoltaCompilerError> errorList) {
            TxtErrorCount.Text = errorList.Count == 1 ? "1 error" : $"{errorList.Count} errores";
        }

    }
}