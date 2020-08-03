using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace Volta
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ARG_CRITICAL_EXCEPTION = "--critical-exception";

        protected override void OnStartup(StartupEventArgs e) {
#if RELEASE
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif

            if (0 < e.Args.Length) {
                switch (e.Args[0]) {
                    default:
                        MessageBox.Show("Argumento de ejecución inválido.", "Volta", MessageBoxButton.OK, MessageBoxImage.Warning);
                        new UI.IDE().Show();
                        break;
                    case ARG_CRITICAL_EXCEPTION:
                        new UI.CrashReport(e.Args[1]).Show();
                        break;
                }
            } else new UI.IDE().Show();
        }

        #region Soporte para excepciones críticas

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            var exceptionInstancePath = SaveExceptionInstance((Exception)e.ExceptionObject);
            Process.Start("VoltaDesktop.exe", $"{ARG_CRITICAL_EXCEPTION} {exceptionInstancePath}");
        }

        private string SaveExceptionInstance(Exception ex) {
            string filename = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Temp\VoltaDesktop_crash_{DateTime.Now:yyyyMMdd-HHmmss}";
            using (var fs = new FileStream(filename, FileMode.Create)) {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fs, ex);
            }

            return filename;
        }

        #endregion
    }
}