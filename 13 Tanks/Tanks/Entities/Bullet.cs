using System;
using System.Drawing;
using System.Threading;

namespace Tanks
{
    public class Bullet : Dynamic
    {
        public string Name="";

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
            if (position.X == 0 || position.Y == 0 || position.X == MapSize.X- this.Width|| position.Y == MapSize.Y- this.Height)
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
