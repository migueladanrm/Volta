﻿using System;
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
    /// Interaction logic for OutputConsole.xaml
    /// </summary>
    public partial class OutputConsole : UserControl, IDEWindow
    {
        public OutputConsole() {
            InitializeComponent();
        }

        public event Action<object> RequestHide;

        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            RequestHide?.Invoke(this);
        }
    }
}