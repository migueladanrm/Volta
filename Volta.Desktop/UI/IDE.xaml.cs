using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Volta.Common;
using Volta.Editor;

namespace Volta.UI
{
    /// <summary>
    /// Interaction logic for IDE.xaml
    /// </summary>
    public partial class IDE : Window
    {
        private EditorWorkspace __workspace;

        public IDE() {
            InitializeComponent();

            DataContext = this;

            Defaults();
        }

        #region Comandos

        public ICommand CloseTabCommand => new DelegateCommand((x) => {

        });

        public ICommand HideSecondaryLayoutCommand => new DelegateCommand((x) => {
            if (LayoutSecondary.Visibility.Equals(Visibility.Visible))
                SLHide();
        });

        public ICommand NewFileCommand => new DelegateCommand((_) => {
            ChangeViewMode(showEnvironment: true);

            EditorTabs.NewTab();
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
                EditorTabs.NewTab(containerPath);

                VoltaSettings.LastDirectory = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf('\\'));
            }
        });

        public ICommand TestCommand => new DelegateCommand((x) => {
            SLDlgShow(nameof(DlgAbout));
        });

        public ICommand AboutCommand => new DelegateCommand((_) => SLDlgShow(nameof(DlgAbout)));

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
            EditorTabs.Visibility = Visibility.Collapsed;
            EditorStatusBar.Visibility = Visibility.Collapsed;
            Toolbar.Visibility = Visibility.Collapsed;

            EditorTabs.OnEditorCaretChanged += EditorStatusBar.UpdateEditorCaretPositions;

            __workspace = EditorWorkspace.NewInstance();
        }

        private void BtnNewFile_Click(object sender, RoutedEventArgs e) {

        }

        private void ChangeViewMode(bool showEnvironment) {
            LblEditorHint.Visibility = showEnvironment ? Visibility.Collapsed : Visibility.Visible;
            EditorTabs.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;
            Toolbar.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;
            EditorStatusBar.Visibility = showEnvironment ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}