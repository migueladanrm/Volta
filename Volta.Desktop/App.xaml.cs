using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Volta
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) {
            var sb = new StringBuilder();
            foreach (var s in e.Args)
                sb.Append(s);

            new UI.IDE(sb.ToString()).Show();
        }
    }
}
