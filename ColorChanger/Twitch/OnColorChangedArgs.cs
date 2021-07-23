using System;

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
