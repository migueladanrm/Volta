using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.AddIn;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.SharpDevelop.Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Volta.Compiler;
using Volta.Editor;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for CodeTab.xaml
    /// </summary>
    public partial class CodeTab : UserControl
    {
        public CodeFile CodeFile { get; private set; }

        private ITextMarkerService textMarkerService;
        private Editor.ToolTipManager.ToolTipService toolTipService;
        private List<VoltaCompilerError> errors = new List<VoltaCompilerError>();

        public event Action<Caret> OnEditorCaretChanged;
        public event Func<CodeFile, CodeFile> OnRequestSaveNewFile;
        public event Action<CodeFile> OnRequestCloseUnsavedFile;
        public event Action OnRequestTabClose;

        public event Action<List<VoltaCompilerError>> OnErrorListUpdated;

        public CodeTab() {
            InitializeComponent();
        }

        public CodeTab(CodeFile codeFile) {
            InitializeComponent();
            DataContext = this;

            CodeFile = codeFile;

            Defaults();
        }

        private void Defaults() {
            InitializeTextMarkerService(TE);
            InitializeToolTipService(TE);

            TE.Load(CodeFile.Content);

            TE.TextArea.Caret.PositionChanged += TextEditorCaret_PositionChanged;
            TE.TextChanged += TE_TextChanged;

            TE_TextChanged(TE, null);
        }

        private void InitializeTextMarkerService(TextEditor textEditor) {
            var textMarkerService = new TextMarkerService(textEditor.Document);
            textEditor.TextArea.TextView.BackgroundRenderers.Add(textMarkerService);
            textEditor.TextArea.TextView.LineTransformers.Add(textMarkerService);
            IServiceContainer services = (IServiceContainer)textEditor.Document.ServiceProvider.GetService(typeof(IServiceContainer));
            if (services != null)
                services.AddService(typeof(ITextMarkerService), textMarkerService);
            this.textMarkerService = textMarkerService;
        }

        private void InitializeToolTipService(TextEditor textEditor) {
            toolTipService = new Editor.ToolTipManager.ToolTipService(textEditor);
            textEditor.TextArea.TextView.BackgroundRenderers.Add(toolTipService);
        }

        private void TE_TextChanged(object sender, EventArgs e) {
            toolTipService.RemoveAll();
            textMarkerService.RemoveAll(delegate (ITextMarker marker) { return true; });

            var textEditor = sender as TextEditor;
            var text = textEditor.Text;
            errors = Controller.Check(text);
            Debug.WriteLine("\n");
            errors.ForEach((VoltaCompilerError error) => {
                int offset = textEditor.Document.GetOffset(error.Line, error.CharPositionInLine);
                ITextMarker marker = textMarkerService.Create(offset, 0);
                marker.MarkerTypes = TextMarkerTypes.SquigglyUnderline;
                marker.MarkerColor = Colors.Red;

                toolTipService.CreateErrorToolTip(error, marker as TextMarker);
            });

            OnErrorListUpdated?.Invoke(errors);

            if (e != null)
                CodeFile.HasUnsavedChanges = true;
        }

        private void TextEditorCaret_PositionChanged(object sender, EventArgs e) {
            try {
                OnEditorCaretChanged?.Invoke(sender as Caret);
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Save() {
            CodeFile.Content = new MemoryStream(Encoding.UTF8.GetBytes(TE.Text));

            if (CodeFile.FilePath != null)
                CodeFile.Save();
            else {
                var savedCF = OnRequestSaveNewFile?.Invoke(CodeFile);
                if (savedCF != null)
                    CodeFile = savedCF;
            }
        }

        public void Close() {
            if (!CodeFile.HasUnsavedChanges) {
                OnRequestTabClose?.Invoke();
                return;
            } else {
                var result = MessageBox.Show($"¿Desea guardar los cambios pendientes en '{CodeFile.FileName}'?", "Cambios sin guardar", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.Yes) {
                    Save();
                    Close();
                } else if (result == MessageBoxResult.No) {
                    CodeFile.HasUnsavedChanges = false;
                    Close();
                }
            }
        }

        public void Cut() {
            if (0 < TE.SelectionLength)
                TE.Cut();
        }

        public void Copy() {
            TE.Copy();
        }

        public void Paste() {
            if (Clipboard.ContainsText())
                TE.Paste();
        }

        public new void Focus() {
            OnEditorCaretChanged?.Invoke(TE.TextArea.Caret);
            OnErrorListUpdated?.Invoke(errors);
        }
    }
}