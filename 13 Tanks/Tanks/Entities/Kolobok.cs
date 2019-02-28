using System;
using System.Drawing;
//using Kolobok.MVC;

namespace Tanks
{
    public class Kolobok : Dynamic
    {
        public override void Run()
        {
            while (true)
            {
                OnCheck();
                Move();
            }
        }

        public Kolobok() : base()
        {
            delta = 2;
        }

        public Kolobok(Point position) : base(position)
        {
            delta = 2;
        }

        public void Die()
        {
            OnReplaceNeeded();
        }

        // Обработчик события смена координат
        public override void OnCheckPosition(object sender, EventArgs e)
        {
            PositionChangedEventArgs positionArgs = e as PositionChangedEventArgs;
            if (positionArgs == null)
                return;
            if (CollidesWith(positionArgs.NewRectangle))
            {
                if (sender is Tank)
                    Die();
            }
        }


    }
}
