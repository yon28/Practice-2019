using System;
using System.Drawing;
using System.Threading;

namespace Tanks
{
    public class Bullet : Dynamic
    {
        public string Name = "";

        public Bullet(Point position) : base(position)
        {
        }

        public Bullet()
        {
        }

        public override void Move()
        {
            if (position.X + dx >= 0 && position.X + Width + dx < MapSize.X)
            {
                position.X += dx;
            }
            if (position.Y + dy >= 0 && position.Y + Height + dy < MapSize.Y)
            {
                position.Y += dy;
            }
            if (position.X <= 2
                || position.Y <= 2
                || Math.Abs(MapSize.X - position.X) <= Width + 10 && DirectionNow == 0
                || Math.Abs(MapSize.Y - position.Y) <= Height + 10 && DirectionNow == 3)
            {
                Stop();
            }
            flag = true;
            OnPositionChanged();
            Thread.Sleep(40 / GameForm.Speed);
        }
    }
}
