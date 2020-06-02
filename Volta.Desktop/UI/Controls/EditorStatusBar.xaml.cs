using ICSharpCode.AvalonEdit.Editing;
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
    /// Interaction logic for EditorStatusBar.xaml
    /// </summary>
    public partial class EditorStatusBar : UserControl
    {

        public event Action RequestErrorList;

        public EditorStatusBar() {
            InitializeComponent();
        }

        public void UpdateEditorCaretPositions(Caret caret) {
            TxtEditorCaret.Text = $"Ln {caret.Line} Col {caret.Column}";
        }

        private void Btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            try {
                var tag = (sender as FrameworkElement).Tag.ToString();
                switch (tag) {
                    case "errorlist":
                        RequestErrorList?.Invoke();
                        break;
                }
            } catch {

            }
        }

        public void UpdateErrorsCount(List<VoltaCompilerError> errorList) {
            TxtErrorCount.Text = errorList.Count == 1 ? "1 error" : $"{errorList.Count} errores";
        }

    }
}