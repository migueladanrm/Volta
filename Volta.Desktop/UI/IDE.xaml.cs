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
using System.Windows.Shapes;
using Volta.Common;

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

        public ICommand NewFileCommand => new DelegateCommand((_) => {
            EditorTabs.NewTab();
        });

        #endregion

        private void Defaults() {
            LblEditorHint.Visibility = Visibility.Collapsed;
        }
    }
}
