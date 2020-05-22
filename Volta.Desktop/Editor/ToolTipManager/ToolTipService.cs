using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.AddIn;
using ICSharpCode.AvalonEdit.Rendering;
using System.Collections.Generic;
using System.Linq;
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
        public ToolTipService(TextEditor textEditor) {
            this.textEditor = textEditor;
            this.errorsToolTips = new List<ErrorToolTip>();

            textEditor.TextArea.TextView.MouseHover += delegate (object sender, MouseEventArgs e) {
                this.errorsToolTips.ForEach(delegate (ErrorToolTip error) {

                    if (error.PlacementRectangle.Contains(e.GetPosition(sender as TextView))) {
                        error.IsOpen = true;
                    } else {
                        error.IsOpen = false;
                        textEditor.TextArea.TextView.ToolTip = null;
                    }
                });
            };
        }

        public KnownLayer Layer {
            get {
                return KnownLayer.Selection;
            }
        }

        public void CreateErrorToolTip(VoltaParserError error, TextMarker marker) {
            ErrorToolTip errorToolTip = new ErrorToolTip(error, marker);
            this.errorsToolTips.Add(errorToolTip);
        }

        public void Draw(TextView textView, DrawingContext drawingContext) {
            errorsToolTips.ForEach(delegate (ErrorToolTip error) {
                //VisualLine line = textView.GetVisualLine(error.Error.line);

                List<Rect> rects = BackgroundGeometryBuilder.GetRectsForSegment(textView, error.Marker).ToList();
                error.Content = error.Error.Message;

                rects.ForEach(delegate (Rect r) {
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

        public void RemoveAll() {
            errorsToolTips.ForEach(delegate (ErrorToolTip error) {
                error.Delete();
            });
            this.errorsToolTips = new List<ErrorToolTip>();
        }
    }
}