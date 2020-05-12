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

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for EditorStatusBar.xaml
    /// </summary>
    public partial class EditorStatusBar : UserControl
    {

        public EditorStatusBar() {
            InitializeComponent();
        }

        public void UpdateEditorCaretPositions(Caret caret) {
            TxtEditorCaret.Text = $"Ln {caret.Line} Col {caret.Column}";
        }
    }
}