using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Volta.Compiler;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for ErrorList.xaml
    /// </summary>
    public partial class ErrorList : UserControl, IDEWindow
    {
        public event Action<object> RequestHide;

        public ErrorList() {
            InitializeComponent();
        }

        public void UpdateErrorList(List<VoltaCompilerError> errors) {
            ErrorListContainer.Children.Clear();
            errors.ForEach(e => ErrorListContainer.Children.Add(new ErrorListItem(e)));
        }

        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            RequestHide?.Invoke(this);
        }

    }
}