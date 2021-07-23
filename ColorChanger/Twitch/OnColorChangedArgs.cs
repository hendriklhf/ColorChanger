using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorChanger.Twitch
{
    public class OnColorChangedArgs : EventArgs
    {
        public string Color { get; }

        public OnColorChangedArgs(string color)
        {
            Color = color;
        }
    }
}
