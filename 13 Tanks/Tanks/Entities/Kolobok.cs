using System;
using System.Drawing;

namespace Tanks
{
    public class Kolobok : Dynamic
    {
        public override void Run()
        {
            while (run)
            {
                OnCheck();
                Move();
            }
        }
        public void Shoot()
        {
             GameForm.game.Shoot();
        }

        public Kolobok() : base()
        {
        }

        public Kolobok(Point position) : base(position)
        {
        }

        // Обработчик события смена координат
        public override void OnCheckPosition(object sender, EventArgs e)
        {
            PositionChangedEventArgs positionArgs = e as PositionChangedEventArgs;
            if (positionArgs == null)
            {
                return;
            }
            if (CollidesWith(positionArgs.NewRectangle))
            {
                if (sender is Tank)
                {
                    // Die();
                    Stop();
                }
            }
        }

    }
}
