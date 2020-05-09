﻿using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Volta.Compiler;

namespace Volta.Editor.ToolTipManager
{
    public class ToolTipService : IBackgroundRenderer
    {
        TextEditor textEditor;

        List<ErrorToolTip> errorsToolTips; 
        public ToolTipService (TextEditor textEditor)
        {
            this.textEditor = textEditor;
            this.errorsToolTips = new List<ErrorToolTip>();

            textEditor.TextArea.TextView.MouseHover += delegate (object sender, MouseEventArgs e)
            {
                this.errorsToolTips.ForEach(delegate (ErrorToolTip error)
                {

                    if (error.PlacementRectangle.Contains(e.GetPosition(sender as TextView)))
                    {
                        error.IsOpen = true;
                    }
                    else
                    {
                        error.IsOpen = false;
                        textEditor.TextArea.TextView.ToolTip = null;
                    }
                });
            };
        }

        public KnownLayer Layer
        {
            get
            {
                // draw behind selection
                return KnownLayer.Selection;
            }
        }

        public void CreateErrorToolTip(VoltaParserError error)
        {
            ErrorToolTip errorToolTip = new ErrorToolTip(error);
            this.errorsToolTips.Add(errorToolTip);
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            errorsToolTips.ForEach(delegate (ErrorToolTip error)
            {
                VisualLine line = textView.GetVisualLine(error.Error.line);

                //Debug.WriteLine("X: {0}    Y: {1}", line.GetTextLineVisualXPosition(line.GetTextLine(error.Error.charPositionInLine), error.Error.charPositionInLine), line.VisualTop);

                List<Rect> rects = BackgroundGeometryBuilder.GetRectsFromVisualSegment(textView, line, error.Error.charPositionInLine, error.Error.charPositionInLine).ToList();
                error.Content = error.Error.msg;

                rects.ForEach(delegate (Rect r)
                {
                    Debug.WriteLine("X:{0} , Y:{1}, W:{2}, H:{3}", r.X, r.Y, r.Width, r.Height);

                    r.Width = 18.00;
                    r.X -= 5.0;


                    error.PlacementTarget = textEditor.TextArea.TextView;
                    error.Placement = PlacementMode.Bottom;
                    error.PlacementRectangle = r;
                    textEditor.TextArea.TextView.ToolTip = error;

                    
                });
            }
            );
            
        }

        public void RemoveAll()
        {
            errorsToolTips.ForEach(delegate (ErrorToolTip error)
            {
                error.Delete();
            });
            this.errorsToolTips = new List<ErrorToolTip>();
        }
    }
}
