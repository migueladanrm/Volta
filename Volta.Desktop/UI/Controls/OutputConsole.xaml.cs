using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for OutputConsole.xaml
    /// </summary>
    public partial class OutputConsole : UserControl, IDEWindow
    {
        public OutputConsole() {
            InitializeComponent();

            Clear();
        }

        public event Action<object> RequestHide;

        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            RequestHide?.Invoke(this);
        }

        public void AddLine(string line) {
            Dispatcher.Invoke(() => {
                TbxOutput.Text = $"{TbxOutput.Text}\n{line}";
            });
        }

        public void Clear() {
            TbxOutput.Clear();
        }
    }
}