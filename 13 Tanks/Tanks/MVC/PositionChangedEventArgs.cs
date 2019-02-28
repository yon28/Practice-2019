using System;
using System.Drawing;

namespace Tanks
{
    public class PositionChangedEventArgs : EventArgs
    {
        private Rectangle newRectangle;

        public Rectangle NewRectangle
        {
            get { return newRectangle; }
            set { newRectangle = value; }
        }
        public PositionChangedEventArgs() : base()
        {
        }
        public PositionChangedEventArgs(Rectangle newRectangle)
            : base()
        {
            this.newRectangle = newRectangle;
        }
    }
}

