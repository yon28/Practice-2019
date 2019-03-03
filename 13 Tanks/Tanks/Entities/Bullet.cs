using System;
using System.Drawing;
using System.Threading;

namespace Tanks
{
    public class Bullet : Dynamic
    {
        private const int width = 30;
        private const int height = 30;
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
        public Bullet (Point position) : base(position)
        {
        }

        public Bullet()
        {
        }

       override public void Run()
        {
            while (true)/**/
            {
                OnCheck();
                Move();
            }
        }


        public override void OnCheckPosition(object sender, EventArgs e)
        {
            PositionChangedEventArgs positionArgs = e as PositionChangedEventArgs;
            if (positionArgs == null)
                return;
            if (CollidesWith(positionArgs.NewRectangle))
            {
                if (sender is Tank)
                {
                    ((Dynamic)sender).Die();
                }
                if (sender is Kolobok)
                {
                    /**/
                    (sender as Kolobok).Die();

                }
            }
        }


        override public void Move()
        {
            if (position.X + dx >= 0 && position.X + this.Width + dx < MapSize.X)
                position.X += dx;
            else
                /**/
            if (position.Y + dy >= 0 && position.Y + this.Height + dy < MapSize.Y)
                position.Y += dy;
            else
            {
               /**/
            }
            flag = true;
            OnPositionChanged();
            Thread.Sleep(50 / GameForm.speed);
        }
    }
}
