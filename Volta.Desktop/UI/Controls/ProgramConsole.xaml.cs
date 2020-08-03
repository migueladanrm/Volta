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

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for ProgramConsole.xaml
    /// </summary>
    public partial class ProgramConsole : UserControl, IDEWindow
    {
        public ProgramConsole() {
            InitializeComponent();

            ConsoleControl.StartProcess("cmd", string.Empty);
        }

        public event Action<object> RequestHide;

        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            RequestHide?.Invoke(this);
        }


        public void ExecuteProgram(string path, string args = null) {
            ConsoleControl.WriteInput($"{path} {args}", Color.FromRgb(0, 0, 0), true);
        }
    }
}
