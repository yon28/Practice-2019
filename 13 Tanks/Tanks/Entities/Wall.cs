using System;
using System.Drawing;

namespace Tanks
{
    public class Wall : Obj
    {
        protected bool flag = false;
        private const int width = 20;
        private const int height = 20;
        public new int Width
        {
            get { return width; }
            set { }
        }
        public new int Height
        {
            get { return height; }
            set { }
        }
        public Wall(Point position) : base(position)
        {
        }

        public Wall()
        {
        }

        public void Replace()
        {
            OnReplaceNeeded();
        }

        public override void OnCheckPosition(object sender, EventArgs e)
        {
            var positionArgs = e as PositionChangedEventArgs;
            if (positionArgs == null)
            {
                return;
            }
            if (CollidesWith(positionArgs.NewRectangle))
            {
                if (sender is Bullet)
                {
                    Replace();
                    Stop();
                }
                if (!(sender is Bullet))
                {
                    ((Dynamic)sender).Deviate();
                }
                else if (this is Wall)
                {
                    ((Bullet)sender).Stop();
                }
            }
        }


        public event EventHandler CheckPosition;

        protected virtual void OnCheck()
        {
            if (CheckPosition != null)
            {
                CheckPosition(this, new PositionChangedEventArgs(new Rectangle(this.position.X + dx, this.position.Y + dy, this.Width, this.Height)));
            }
        }

       

    }
}

