using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

        public IDE(string args) {
            InitializeComponent();
            DataContext = this;
            Defaults();

            if (args.Contains("--test")) {
                NewFileCommand.Execute(null);
                AlternateErrorListCommand.Execute(null);
            }
        }

        #region Comandos

        public ICommand AlternateErrorListCommand => new DelegateCommand((x) => AlternateErrorList());

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
                FileName = $"Nuevo{_counter}.cs"
            });
            _counter++;
        });

        public ICommand OpenFileCommand => new DelegateCommand((x) => {
            var dlg = new OpenFileDialog {
                Filter = "Archivos de código C# (*.cs) | *.cs|Todos los archivos (*.*) | *.*",
                InitialDirectory = VoltaSettings.LastDirectory ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = false
            };
            if (dlg.ShowDialog().Value) {
                ChangeViewMode(showEnvironment: true);

                string path = dlg.FileName;
                OpenCodeFile(new CodeFile(path));

                VoltaSettings.LastDirectory = path.Substring(0, path.LastIndexOf('\\'));

                // actualización de archivos recientes.
                var recentFiles = VoltaSettings.RecentFiles ?? new List<string>();
                if (recentFiles.Contains(path))
                    recentFiles.Remove(path);

                recentFiles.Insert(0, path);

                if (recentFiles.Count > 10)
                    recentFiles.RemoveRange(VoltaSettings.MaxRecentFiles, recentFiles.Count - VoltaSettings.MaxRecentFiles);

                VoltaSettings.RecentFiles = recentFiles;
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
            //LblEditorHint.Visibility = Visibility.Visible;
            TC.Visibility = Visibility.Collapsed;
            ErrorList.Visibility = Visibility.Collapsed;
            ErrorList.OnRequestHide += AlternateErrorList;
            EditorStatusBar.Visibility = Visibility.Collapsed;
            EditorStatusBar.RequestErrorList += AlternateErrorList;
            Toolbar.Visibility = Visibility.Collapsed;
            MC.Visibility = Visibility.Collapsed;

            WC.Visibility = Visibility.Visible;
            WCRecentsLoad();
        }

        private void BtnNewFile_Click(object sender, RoutedEventArgs e)
            => NewFileCommand.Execute(null);


        private void ChangeViewMode(bool showEnvironment) {
            //LblEditorHint.Visibility = showEnvironment ? Visibility.Collapsed : Visibility.Visible;
            WC.Visibility = showEnvironment ? Visibility.Collapsed : Visibility.Visible; ;
            TC.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;
            Toolbar.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;
            EditorStatusBar.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;

            WCRecentsLoad();
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
            ct.OnErrorListUpdated += ErrorList.UpdateErrorList;
            ct.OnErrorListUpdated += EditorStatusBar.UpdateErrorsCount;

            var ti = new TabItem {
                Header = cf.FileName,
                Content = ct
            };
            ti.GotFocus += (sender, e) => (ti.Content as CodeTab).Focus();

            TC.Items.Add(ti);

            TC.SelectedIndex = TC.Items.Count - 1;
        }

        private void OpenCodeFile(string path)
            => OpenCodeFile(new CodeFile(path));

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

        private void ToolbarButton_Click(object sender, RoutedEventArgs e) {
            try {
                switch ((sender as FrameworkElement).Tag.ToString().Replace("toolbar.", "")) {
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
                        {
                            var currentCodeTab = GetCurrentCodeTab();
                            if (currentCodeTab.errors.Count == 0)
                            {
                                var selected = CompilerSelect.SelectedIndex;

                                MCShow($"Compilando y ejecutando el programa con {(selected == 0 ? "Delta" : "Nabla")}");

                                if(selected == 0)
                                {
                                    var tree = currentCodeTab.tree;

                                    var deltaCode = new Compiler.CodeGeneration.Delta.DeltaVisitor(tree);

                                    var textFile = deltaCode.CreateTempFile();

                                    var exeFile = @".\Minics.exe";

                                    var info = new ProcessStartInfo();

                                    info.FileName = exeFile;
                                    info.Arguments = $"{textFile}";
                                    info.RedirectStandardError = true;
                                    info.RedirectStandardInput = true;
                                    info.RedirectStandardOutput = true;

                                    info.UseShellExecute = false;
                                    

                                    
                                
                                    try
                                    {
                                        using (Process exeProcess = Process.Start(info))
                                        {

                                            exeProcess.BeginErrorReadLine();
                                            exeProcess.BeginOutputReadLine();

                                            exeProcess.OutputDataReceived += (a, b) =>
                                            {
                                                Debug.WriteLine(b.Data);
                                            };

                                            exeProcess.ErrorDataReceived += (a, b) =>
                                            {
                                                Debug.WriteLine(b.Data);
                                            };

                                            


                                            using (StreamWriter myStreamWriter = exeProcess.StandardInput)
                                            {
                                                String inputText;
                                                Debug.WriteLine("Enter a line of text (or press the Enter key to stop):");

                                                inputText = "3";
                                                myStreamWriter.WriteLine(inputText);

                                                myStreamWriter.Close();

                                                exeProcess.WaitForExit();
                                            }
                                            
                                        }
                                    
                                    }
                                    catch (Exception error)
                                    {
                                        Debug.WriteLine("ERORROROROROOR");
                                        Debug.WriteLine(error.Message);// Log error.
                                    }
                                }
                            }
                            else
                            {
                                MCShow("Aún existen errores en el código, debe elminarlos primero");
                            }

                            break;
                        }
                    case "close":
                        CloseTabCommand.Execute(null);
                        break;
                }
            } catch (Exception ex) {
                Debug.Fail(ex.Message);
            }
        }

        private void AlternateErrorList() {
            ErrorList.Visibility = ErrorList.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MCShow(string message) {
            MC.Opacity = 1;
            MC.Text = message;
            MC.IsVisibleChanged += (sender, e) => {
                var sb = FindResource("Storyboard.MC.Hide") as Storyboard;
                sb.Completed += (_sender, _e) => {
                    MC.Visibility = Visibility.Collapsed;
                };
                sb.Begin();
            };
            MC.Visibility = Visibility.Visible;
        }

        private void WCRecentsLoad() {
            WCRecentContainer.Children.RemoveRange(2, WCRecentContainer.Children.Count - 1);

            var recentFiles = VoltaSettings.RecentFiles;
            var existsRecentFiles = recentFiles != null && 0 < recentFiles.Count;
            (WCRecentContainer.Children[1] as FrameworkElement).Visibility = !existsRecentFiles ? Visibility.Visible : Visibility.Collapsed;

            if (existsRecentFiles) {
                foreach (var rf in recentFiles) {
                    var item = new TextBlock {
                        Style = WC.FindResource("Style.ActionLabel") as Style,
                        Tag = $"recent {rf}",
                        Text = rf
                    };
                    item.MouseLeftButtonDown += WCDoAction;
                    WCRecentContainer.Children.Add(item);
                }
            }
        }

        private void WCDoAction(object sender, RoutedEventArgs e) {
            var action = (sender as TextBlock).Tag as string;
            if (action == null)
                return;

            switch (action) {
                default:
                    if (action.StartsWith("recent ")) {
                        action = action.Replace("recent ", "");
                        OpenCodeFile(action);
                        ChangeViewMode(showEnvironment: true);

                        //actualización de archivos recientes.
                        var recentFiles = VoltaSettings.RecentFiles;
                        recentFiles.Remove(action);
                        recentFiles.Insert(0, action);
                        VoltaSettings.RecentFiles = recentFiles;
                    }
                    break;
                case "openFile":
                    OpenFileCommand.Execute(null);
                    break;
                case "newFile":
                    NewFileCommand.Execute(null);
                    break;
                case "about":
                    SLDlgShow(nameof(DlgAbout));
                    break;
            }
        }
    }
}