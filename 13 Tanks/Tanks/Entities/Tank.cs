using System;
using System.Drawing;

namespace Tanks
{
    public class Tank : Dynamic
    {
        public Tank()
        {
        }

        public Tank(Point position) : base(position)
        {
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
                if (sender is BulletK)
                {
                    Stop();
                    Move();
                    ((Dynamic)sender).Stop();
                    ((Dynamic)sender).Move();
                }
                if (sender is Kolobok)
                {
                    ((Dynamic)sender).Stop();
                }
                if (sender is Tank)
                {
                    ((Dynamic)sender).Deviate();
                    Deviate();
                }
            }
        }
    }
}
