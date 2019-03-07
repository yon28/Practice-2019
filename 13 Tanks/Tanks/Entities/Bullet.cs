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

        public override void Move()
        {
            if (position.X + dx >= 0 && position.X + this.Width + dx < MapSize.X)
            {
                position.X += dx;
            }
            if (position.Y + dy >= 0 && position.Y + this.Height + dy < MapSize.Y)
            {
                position.Y += dy;
            }
            if (position.X == 0 || position.Y == 0 || Math.Abs(MapSize.X - position.X) <= this.Width*2||  Math.Abs(MapSize.Y - position.Y)<= this.Height*2)
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
                    this.Stop();
                    this.Move();
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
