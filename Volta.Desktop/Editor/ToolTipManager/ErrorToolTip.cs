using ICSharpCode.AvalonEdit.AddIn;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Volta.Compiler;

namespace Volta.Editor.ToolTipManager
{
    public class ErrorToolTip : ToolTip
    {
        
        public ErrorToolTip(VoltaParserError error, TextMarker marker)
        {
            this.Error = error;
            this.Marker = marker;
        }

        public VoltaParserError Error { get; set; }

        public TextMarker Marker { get; set; }


        public void Delete()
        {
            this.IsOpen = false;
            this.IsEnabled = false;
        }
    }
}
