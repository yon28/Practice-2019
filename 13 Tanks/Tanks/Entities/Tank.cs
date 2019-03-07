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
                    (sender as Dynamic).Stop();//
                    this.Stop();
                    this.Move();
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

        public override void Stop()
        {
            dy = 0;
            dx = 0;
            position.X = -30;
            position.Y = -30;
            run = false;
        }
    }
}
