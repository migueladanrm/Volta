using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Volta.Editor
{
    /// <summary>
    /// Archivo de código.
    /// </summary>
    public class CodeFile
    {
        private FileInfo file;

        public CodeFile() {
            Content = new MemoryStream();
        }

        public CodeFile(string path) {
            file = new FileInfo(path);
            Content = new MemoryStream(File.ReadAllBytes(path));
        }

        public string FileName {
            get => file?.Name;
            set => FileName = value;
        }

        public string FilePath => file.FullName ?? null;

        public MemoryStream Content { get; set; }

        public bool HasUnsavedChanges { get; set; }
    }
}