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
                }
                if (sender is Kolobok)
                {
                    (sender as Kolobok).Stop();
                }
                if (sender is Tank)
                {
                    ((Dynamic)sender).Deviate();
                    this.Deviate();
                    
                }
            }
        }
    }
}
