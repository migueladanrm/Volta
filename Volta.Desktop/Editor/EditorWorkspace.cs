using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Volta.Editor
{
    internal class EditorWorkspace
    {
        private List<CodeFile> documents;

        internal event Action<int> OnUnsavedFileCloseAttempt;
        internal event Action<int> OnSaveNewFileAttempt;

        private EditorWorkspace() {
            documents = new List<CodeFile>();
        }

        internal static EditorWorkspace NewInstance()
            => new EditorWorkspace();

        internal void CloseDocument(int index) {
            try {
                var item = documents[index];

                if (item.HasUnsavedChanges)
                    OnUnsavedFileCloseAttempt?.Invoke(index);
                else {
                    documents.RemoveAt(index);
                }
            }catch(Exception e) {

            }
        }

        internal void SaveDocument(int index) {
            var item = documents[index];

            if (item.FilePath == null) 
                OnSaveNewFileAttempt?.Invoke(index);
            else {
                File.WriteAllBytes(item.FilePath, item.Content.ToArray());
            }
        }
    }
}