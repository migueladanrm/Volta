using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Volta.Common;
using Microsoft.Win32;

namespace Volta.UI
{
    /// <summary>
    /// Interaction logic for IDE.xaml
    /// </summary>
    public partial class IDE : Window
    {
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
            if (LblEditorHint.Visibility.Equals(Visibility.Visible))
                LblEditorHint.Visibility = Visibility.Collapsed;

            if (!EditorTabs.Visibility.Equals(Visibility.Visible))
                EditorTabs.Visibility = Visibility.Visible;

            EditorTabs.NewTab();
        });

        public ICommand OpenFileCommand => new DelegateCommand((x) => {
            var dlg = new OpenFileDialog {
                Filter = "Archivos de código C# (*.cs) | *.cs;",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = false
            };
            if (dlg.ShowDialog().Value) {
                string containerPath = dlg.FileName;

            }
        });

        public ICommand TestCommand => new DelegateCommand((x) => {
            SLDlgShow(nameof(DlgAbout));
        });

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

        private void SLDlgRequestHide() => SLHide();

        #endregion

        private void Defaults() {
            LayoutSecondary.Visibility = Visibility.Collapsed;

            LblEditorHint.Visibility = Visibility.Visible;
            EditorTabs.Visibility = Visibility.Collapsed;
        }
    }
}
