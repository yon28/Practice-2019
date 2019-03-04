using System;
using System.Drawing;
using System.Threading;

namespace Tanks
{
    public class Bullet : Dynamic
    {
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
        public Bullet(Point position) : base(position)
        {
        }

        public Bullet()
        {
        }

        override public void Run()
        {
            while (true)
            {
                OnCheck();
                Move();
            }
        }

        override public void Move()
        {
            if (position.X + dx >= 0 && position.X + this.Width + dx < MapSize.X)
            {
                position.X += dx;
            }
            if (position.Y + dy >= 0 && position.Y + this.Height + dy < MapSize.Y)
            {
                position.Y += dy;
            }
            if (position.X == 0 || position.Y == 0 || position.X == MapSize.X || position.Y == MapSize.Y)
            {
                Stop();
            }
            flag = true;
            OnPositionChanged();
            Thread.Sleep(15 / GameForm.speed);
        }
    }
}
