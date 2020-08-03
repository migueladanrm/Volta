using System;
using System.Collections.Generic;
using System.Text;

namespace Volta.UI.Controls
{
    internal interface IDEWindow
    {
        event Action<object> RequestHide;
        //event Action Show;
    }
}
