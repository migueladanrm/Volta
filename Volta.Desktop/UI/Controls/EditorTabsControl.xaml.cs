using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
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
using Volta.Editor;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for EditorTabsControl.xaml
    /// </summary>
    public partial class EditorTabsControl : UserControl
    {
        private int _newDocumentCounter = 1;

        public EditorTabsControl() {
            InitializeComponent();

            DataContext = this;

            Defaults();
        }

        public int OpenedDocuments => TC.Items.Count;

        private void Defaults() {
            TC.Items.Remove(TC.Items[0]);
        }

        private TextEditor GenerateTextEditor() {
            var te = new TextEditor {
                FontFamily = new FontFamily("Consolas"),
                FontSize = 16,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                ShowLineNumbers = true,
                SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#")
            };
            te.TextArea.Caret.PositionChanged += TextEditorCaret_PositionChanged;

            return te;
        }

        public void NewTab(bool focus = true) {
            var te = GenerateTextEditor();

            TC.Items.Add(new TabItem {
                Header = $"Nuevo elemento {_newDocumentCounter}",
                Content = te
            });

            if (focus) {
                TC.SelectedIndex = TC.Items.Count - 1;
                te.TextArea.Focus();
            }

            _newDocumentCounter++;
        }

        public void NewTab(string path) {
            var codeFile = new CodeFile(path);

            var te = GenerateTextEditor();
            te.Load(codeFile.FilePath);

            TC.Items.Add(new TabItem {
                Header = codeFile.FileName,
                Content = te
            });

            TC.SelectedIndex = TC.Items.Count - 1;
            te.TextArea.Focus();
        }

        private void TC_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                var tab = TC.Items[TC.SelectedIndex] as TabItem;
                var te = tab.Content as TextEditor;
                UpdateTextEditorCaretPositions(te.TextArea.Caret);
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void TextEditorCaret_PositionChanged(object sender, EventArgs e) {
            try {
                var caret = sender as Caret;
                UpdateTextEditorCaretPositions(caret);
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void UpdateTextEditorCaretPositions(Caret caret) {
            TxtEditorCaret.Text = $"Ln {caret.Line} Col {caret.Column}";
        }
    }
}