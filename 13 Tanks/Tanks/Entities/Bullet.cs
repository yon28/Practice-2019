using System;
using System.Drawing;
using System.Threading;

namespace Tanks
{
    public class Bullet : Dynamic
    {
        public string Name="";
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

        protected override  void Move()
        {
            if (position.X + dx >= 0 && position.X + this.Width + dx < MapSize.X)
            {
                position.X += dx;
            }
            if (position.Y + dy >= 0 && position.Y + this.Height + dy < MapSize.Y)
            {
                position.Y += dy;
            }
            if (position.X == 0 || position.Y == 0 || position.X == MapSize.X-30 || position.Y == MapSize.Y-30)
            {
                Stop();
            }
            flag = true;
            OnPositionChanged();
            Thread.Sleep(20 / GameForm.Speed);
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
                if (sender is Tank && this is BulletK)
                {
                    ((Tank)sender).Stop();
                    ((Bullet)this).Stop();
                }
                if (sender is Kolobok && this is BulletT)
                {
                    (sender as Kolobok).Stop();
                    ((Bullet)this).Stop();
                }
                if (sender is Tank && !(this is Bullet))
                {
                    ((Tank)sender).Deviate();
                    this.Deviate();
                }
            }
        }
    }
}
