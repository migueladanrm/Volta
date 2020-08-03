using System;

namespace Volta.UI.Controls
{
    internal interface IDEWindow
    {
        event Action<object> RequestHide;
    }
}
