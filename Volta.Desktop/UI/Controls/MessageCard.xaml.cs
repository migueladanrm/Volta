using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
