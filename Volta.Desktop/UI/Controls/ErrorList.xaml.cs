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
using Volta.Compiler;

namespace Volta.UI.Controls
{
    /// <summary>
    /// Interaction logic for ErrorList.xaml
    /// </summary>
    public partial class ErrorList : UserControl
    {
        public event Action OnRequestHide;

        public ErrorList() {
            InitializeComponent();
        }

        public void UpdateErrorList(List<VoltaParserError> errors) {
            ErrorListContainer.Children.Clear();
            errors.ForEach(e => ErrorListContainer.Children.Add(new ErrorListItem(e)));
        }

        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            OnRequestHide?.Invoke();
        }
    }
}
