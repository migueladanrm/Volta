using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Volta.Common;
using Volta.Editor;
using Volta.UI.Controls;

namespace Volta.UI
{
    /// <summary>
    /// Interaction logic for IDE.xaml
    /// </summary>
    public partial class IDE : Window
    {
        private int _counter = 1;

        public IDE() {
            InitializeComponent();

            DataContext = this;

            Defaults();
        }

        #region Comandos

        public ICommand CloseTabCommand => new DelegateCommand((x) => {
            GetCurrentCodeTab()?.Close();

            if (TC.Items.Count < 1)
                ChangeViewMode(false);
        });

        public ICommand HideSecondaryLayoutCommand => new DelegateCommand((x) => {
            if (LayoutSecondary.Visibility.Equals(Visibility.Visible))
                SLHide();
        });

        public ICommand NewFileCommand => new DelegateCommand((_) => {
            ChangeViewMode(showEnvironment: true);

            OpenCodeFile(new CodeFile {
                FileName = $"Nuevo{_counter}.mcs"
            });
            _counter++;
        });

        public ICommand OpenFileCommand => new DelegateCommand((x) => {
            var dlg = new OpenFileDialog {
                Filter = "Archivos de código C# (*.cs) | *.cs;",
                InitialDirectory = VoltaSettings.LastDirectory ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = false
            };
            if (dlg.ShowDialog().Value) {
                ChangeViewMode(showEnvironment: true);

                string containerPath = dlg.FileName;
                OpenCodeFile(new CodeFile(containerPath));

                VoltaSettings.LastDirectory = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf('\\'));
            }
        });

        public ICommand SaveFileCommand => new DelegateCommand((x) => {
            GetCurrentCodeTab()?.Save();
        });

        public ICommand TestCommand => new DelegateCommand((x) => {
            SLDlgShow(nameof(DlgAbout));
        });

        public ICommand AboutCommand => new DelegateCommand((x) => SLDlgShow(nameof(DlgAbout)));

        public ICommand CutCommand => new DelegateCommand((x) => GetCurrentCodeTab()?.Cut());
        public ICommand CopyCommand => new DelegateCommand((x) => GetCurrentCodeTab()?.Copy());
        public ICommand PasteCommand => new DelegateCommand((x) => GetCurrentCodeTab()?.Paste());

        #endregion

        #region Soporte de cuadros de dialogo

        private void SLHide(object sender, RoutedEventArgs e) => SLHide();

        private void SLHide(bool hideOnlyDialog = false) {
            var currentDialog = SLGetCurrentVisibleDialog();
            if (currentDialog != null) {
                var sbHideDialog = FindResource("Storyboard.Dialog.Hide") as Storyboard;

                foreach (var animation in sbHideDialog.Children)
                    Storyboard.SetTargetName(animation, currentDialog.Name);

                BeginStoryboard(sbHideDialog);
            }

            if (hideOnlyDialog)
                return;

            var sbHideSL = (Storyboard)FindResource("Storyboard.LayoutSecondary.Hide");
            sbHideSL.Completed += (_sender, _e) => LayoutSecondary.Visibility = Visibility.Collapsed;

            BeginStoryboard(sbHideSL);
        }

        private void SLDlgShow(string id) {
            if (LayoutSecondary.Visibility == Visibility.Visible && SLGetCurrentVisibleDialog() != null)
                SLHide(hideOnlyDialog: true);

            FrameworkElement targetDialog = null;
            foreach (FrameworkElement element in LayoutSecondary.Children) {
                if (element is Border)
                    element.Visibility = element.Name != null && element.Name.Equals(id) ? Visibility.Visible : Visibility.Collapsed;

                if (element.Name == id)
                    targetDialog = element;
            }

            if (LayoutSecondary.Visibility != Visibility.Visible) {
                LayoutSecondary.Visibility = Visibility.Visible;
                BeginStoryboard((Storyboard)FindResource("Storyboard.LayoutSecondary.Show"));
            }

            var sbShowDialog = (Storyboard)FindResource("Storyboard.Dialog.Show");
            foreach (var animation in sbShowDialog.Children)
                Storyboard.SetTargetName(animation, id);

            BeginStoryboard(sbShowDialog);
        }

        private FrameworkElement SLGetCurrentVisibleDialog() {
            foreach (FrameworkElement child in LayoutSecondary.Children) {
                if (child is Border && child.Visibility.Equals(Visibility.Visible))
                    return child;
            }
            return null;
        }

        #endregion

        private void Defaults() {
            LayoutSecondary.Visibility = Visibility.Collapsed;

            LblEditorHint.Visibility = Visibility.Visible;
            TC.Visibility = Visibility.Collapsed;
            ErrorList.Visibility = Visibility.Collapsed;
            EditorStatusBar.Visibility = Visibility.Collapsed;
            Toolbar.Visibility = Visibility.Collapsed;
        }

        private void BtnNewFile_Click(object sender, RoutedEventArgs e) {
            NewFileCommand.Execute(null);
        }

        private void ChangeViewMode(bool showEnvironment) {
            LblEditorHint.Visibility = showEnvironment ? Visibility.Collapsed : Visibility.Visible;
            TC.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;
            Toolbar.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;
            EditorStatusBar.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;
        }

        private CodeTab GetCurrentCodeTab() {
            if (0 < TC.Items.Count)
                return (TC.Items[TC.SelectedIndex] as TabItem).Content as CodeTab;

            return null;
        }

        private void OpenCodeFile(CodeFile cf) {
            if (cf.FilePath != null) {
                for (int i = 0; i < TC.Items.Count; i++) {
                    var tmpCF = (((TabItem)TC.Items[i]).Content as CodeTab).CodeFile;

                    if (tmpCF.FilePath != null && tmpCF.FilePath.Equals(cf.FilePath)) {
                        TC.SelectedIndex = i;
                        break;
                    }
                }
            }

            var ct = new CodeTab(cf);
            ct.OnEditorCaretChanged += EditorStatusBar.UpdateEditorCaretPositions;
            ct.OnRequestSaveNewFile += SaveNewFile;
            ct.OnRequestTabClose += TabCloseRequest;

            TC.Items.Add(new TabItem {
                Header = cf.FileName,
                Content = ct
            });

            TC.SelectedIndex = TC.Items.Count - 1;
        }

        private CodeFile SaveNewFile(CodeFile cf) {
            var dlg = new SaveFileDialog {
                Filter = "Archivos de código C# (*.cs) | *.cs;",
                InitialDirectory = VoltaSettings.LastDirectory ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                AddExtension = true,
                FileName = cf.FileName
            };

            if (dlg.ShowDialog() == true) {
                var filename = dlg.FileName;

                File.WriteAllBytes(filename, cf.Content.ToArray());
                (TC.Items[TC.SelectedIndex] as TabItem).Header = filename.Substring(filename.LastIndexOf('\\') + 1, filename.Length - filename.LastIndexOf('\\') - 1);
                return new CodeFile(dlg.FileName);
            }

            return null;
        }

        private void TabCloseRequest() {
            TC.Items.Remove(TC.Items[TC.SelectedIndex]);
        }

        private void DlgPendingChangesBtnSave_Click(object sender, RoutedEventArgs e) {

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void ToolbarButton_Click(object sender, RoutedEventArgs e) {
            try {
                switch((sender as FrameworkElement).Tag.ToString().Replace("toolbar.", "")) {
                    case "new":
                        NewFileCommand.Execute(null);
                        break;
                    case "open":
                        OpenFileCommand.Execute(null);
                        break;
                    case "save":
                        SaveFileCommand.Execute(null);
                        break;
                    case "cut":
                        CutCommand.Execute(null);
                        break;
                    case "copy":
                        CopyCommand.Execute(null);
                        break;
                    case "paste":
                        PasteCommand.Execute(null);
                        break;
                    case "buildrun":
                        break;
                    case "close":
                        CloseTabCommand.Execute(null);
                        break;
                }
            }catch(Exception ex) {
                Debug.Fail(ex.Message);
            }
        }
    }
}