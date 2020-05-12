using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.AddIn;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.SharpDevelop.Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using Volta.Compiler;
using Volta.Editor;
using System.Text;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for EditorTabsControl.xaml
    /// </summary>
    public partial class EditorTabsControl : UserControl
    {
        private int _newDocumentCounter = 1;

        private ITextMarkerService textMarkerService;

        private Editor.ToolTipManager.ToolTipService toolTipService;

        public event Action<int, Stream> OnDocumentChanged;
        public event Action<Caret> OnEditorCaretChanged;
        public event Action<int> OnRequestSaveDocument;

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

            InitializeTextMarkerService(te);

            InitializeToolTipService(te);

            te.TextArea.Caret.PositionChanged += TextEditorCaret_PositionChanged;

            te.TextChanged += Te_TextChanged;

            return te;
        }

        void InitializeTextMarkerService(TextEditor textEditor) {
            var textMarkerService = new TextMarkerService(textEditor.Document);
            textEditor.TextArea.TextView.BackgroundRenderers.Add(textMarkerService);
            textEditor.TextArea.TextView.LineTransformers.Add(textMarkerService);
            IServiceContainer services = (IServiceContainer)textEditor.Document.ServiceProvider.GetService(typeof(IServiceContainer));
            if (services != null)
                services.AddService(typeof(ITextMarkerService), textMarkerService);
            this.textMarkerService = textMarkerService;
        }

        void InitializeToolTipService(TextEditor textEditor) {
            toolTipService = new Editor.ToolTipManager.ToolTipService(textEditor);
            textEditor.TextArea.TextView.BackgroundRenderers.Add(toolTipService);
        }

        private void Te_TextChanged(object sender, EventArgs e) {
            toolTipService.RemoveAll();
            textMarkerService.RemoveAll(delegate (ITextMarker marker) { return true; });

            TextEditor textEditor = (sender as TextEditor);

            string text = textEditor.Text;
            List<VoltaParserError> errors = Controller.check(text);
            Debug.WriteLine("\n");
            errors.ForEach(delegate (VoltaParserError error) {
                Debug.WriteLine("El error es: ");
                Debug.WriteLine(error.msg);
                Debug.WriteLine("En la línea {0} y columna {1}", error.line, error.charPositionInLine);

                int offset = textEditor.Document.GetOffset(error.line, error.charPositionInLine);
                ITextMarker marker = textMarkerService.Create(offset, 0);
                marker.MarkerTypes = TextMarkerTypes.SquigglyUnderline;
                marker.MarkerColor = Colors.Red;

                toolTipService.CreateErrorToolTip(error, marker as TextMarker);
            });
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

        public void NewTab(ref CodeFile codeFile) {
            var te = GenerateTextEditor();
            te.Load(codeFile.Content);
            te.DocumentChanged += TE_DocumentChanged;
        }

        private void TE_DocumentChanged(object sender, EventArgs e) {
            var currentTE = sender as TextEditor;
            var payload = new MemoryStream(Encoding.UTF8.GetBytes(currentTE.Text));

            OnDocumentChanged?.Invoke(TC.SelectedIndex, payload);
        }

        private void TC_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                var tab = TC.Items[TC.SelectedIndex] as TabItem;
                var te = tab.Content as TextEditor;

                OnEditorCaretChanged?.Invoke(te.TextArea.Caret);

                InitializeTextMarkerService(te);
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private void TextEditorCaret_PositionChanged(object sender, EventArgs e) {
            try {
                OnEditorCaretChanged?.Invoke(sender as Caret);
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Save() {

        }
    }
}