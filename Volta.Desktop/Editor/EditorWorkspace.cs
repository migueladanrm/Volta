using System;
using System.Collections.Generic;
using System.Text;

namespace Volta.Editor
{
    internal class EditorWorkspace
    {
        private List<CodeFile> documents;

        private EditorWorkspace() {

        }

        internal static EditorWorkspace NewInstance()
            => new EditorWorkspace();
    }
}