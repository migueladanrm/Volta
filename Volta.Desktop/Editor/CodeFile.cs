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
            HasUnsavedChanges = false;
        }

        public CodeFile(string path) {
            file = new FileInfo(path);
            Content = new MemoryStream(File.ReadAllBytes(path));
            HasUnsavedChanges = false;

            FileName = file.Name;
        }

        public string FileName { get; set; }

        public string FilePath => file?.FullName ?? null;

        public MemoryStream Content { get; set; }

        public bool HasUnsavedChanges { get; set; }

        public void Save() {
            if (FilePath != null) {
                File.WriteAllBytes(FilePath, Content.ToArray());
                HasUnsavedChanges = false;
            }
        }
    }
}