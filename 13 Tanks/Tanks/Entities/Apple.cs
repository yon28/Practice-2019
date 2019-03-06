using System;
using System.Drawing;

namespace Tanks
{
    public class Apple : Obj
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
        public Apple(Point position) : base(position)
        {
        }

        public Apple()
        {
        }

        //Перемещает при съедании
        protected void Replace()
        {
            OnReplaceNeeded();
        }

        // Обработчик события проверки координат
        public override void OnCheckPosition(object sender, EventArgs e)
        {
            PositionChangedEventArgs positionArgs = e as PositionChangedEventArgs;
            if (positionArgs == null)
                return;
            if (CollidesWith(positionArgs.NewRectangle))
            {
                if (sender is Kolobok)
                {
                    Replace();
                    GameForm.game.Score++;
                }
            }
        }

    }
}
