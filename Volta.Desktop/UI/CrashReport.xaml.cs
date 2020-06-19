using System;
using System.IO;
using System.Windows;
using Volta.Common;

namespace Volta.UI
{
    /// <summary>
    /// Interaction logic for CrashReport.xaml
    /// </summary>
    public partial class CrashReport : Window
    {
        /// <summary>
        /// Inicializa una instancia de <see cref="CrashReport"/>.
        /// </summary>
        public CrashReport() {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializa una instancia de <see cref="CrashReport"/>.
        /// </summary>
        /// <param name="crashReportFile">Archivo de datos del error crítico.</param>
        public CrashReport(string crashReportFile) {
            InitializeComponent();

            LoadCrashReport(crashReportFile);
        }

        /// <summary>
        /// Carga la información del error crítico.
        /// </summary>
        /// <param name="crashReportFile">Archivo de datos del error crítico.</param>
        private void LoadCrashReport(string crashReportFile) {
            if (crashReportFile == null || !File.Exists(crashReportFile))
                return;

            try {
                var file = new FileInfo(crashReportFile);
                var ex = SerializationUtils.Deserialize<Exception>(File.ReadAllBytes(file.FullName));
                TbxCrashDetails.Text = $"Volta Crash Report\n{file.CreationTime:O}\n\nMessage:\n{ex.Message}";
            } catch { }
        }

        private void BtnDone_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}