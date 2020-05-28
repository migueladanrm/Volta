using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MessageCard.xaml
    /// </summary>
    public partial class MessageCard : UserControl
    {
        private const string CATEGORY_TAG = "Custom properties";

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MessageCard), new PropertyMetadata("Hello, world!"));

        public MessageCard() {
            InitializeComponent();
            DataContext = this;
        }

        [Category(CATEGORY_TAG),Description("The text on the card")]
        public string Text {
            get => GetValue(TextProperty) as string;
            set => SetValue(TextProperty, value);
        }
    }
}
