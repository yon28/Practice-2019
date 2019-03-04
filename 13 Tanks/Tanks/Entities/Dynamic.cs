using System;
using System.Drawing;
using System.Threading;

namespace Tanks
{
    public class Dynamic : Obj
    {
        public event EventHandler CheckPosition;
        protected virtual void OnCheck()
        {
            if (CheckPosition != null)
                CheckPosition(this, new PositionChangedEventArgs(new Rectangle(this.position.X + dx, this.position.Y + dy, this.Width, this.Height)));
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
                    if (!(this is Bullet))
                    {
                        this.Deviate();
                    }
                    if (!(sender is Bullet))
                    {
                        ((Dynamic)sender).Deviate();
                    }
                }
                if (sender is Kolobok && this is Tank)
                {
                    (sender as Kolobok).Die();
                }
                if (sender is Tank && this is Bullet)
                {
                    ((Bullet)this).Stop();
                    ((Tank)sender).Stop();
                }
            }
        }

        public virtual void Die()/**/
        {
            OnReplaceNeeded();
        }


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

        protected int dy;
        protected int dx;
        protected bool flag = false;
        protected int delta = 1;
        private int directionNow;

        public int DirectionNow
        {
            get { return directionNow; }
            set { directionNow = value; }
        }

        public Dynamic() : base()
        {
        }

        public Dynamic(Point position) : base(position)
        {
            dy = 0;
            dx = delta;
            Turn();
        }

        virtual public void Move()
        {
            if (position.X + dx >= 0 && position.X + this.Width + dx < MapSize.X)
                position.X += dx;
            else
                if (!(this is Bullet)) Turn();
            if (position.Y + dy >= 0 && position.Y + this.Height + dy < MapSize.Y)
                position.Y += dy;
            else
            {
                if (!(this is Bullet)) Turn();
            }
            flag = true;
            OnPositionChanged();
            Thread.Sleep(100 / GameForm.speed);
        }

        public override void Stop()
        {
            dx = 0;
            dy = 0;
            position.X = -30;
            position.Y = -30;
            /**/
        }

        public void Deviate()
        {
            if (flag == true)
            {
                dx = -dx;
                dy = -dy;
                switch (directionNow)
                {
                    case (int)Direction.Left:
                        {
                            directionNow = (int)Direction.Right;
                            break;
                        }
                    case (int)Direction.Right:
                        {
                            directionNow = (int)Direction.Left;
                            break;
                        }
                    case (int)Direction.Up:
                        {
                            directionNow = (int)Direction.Down;
                            break;
                        }
                    case (int)Direction.Down:
                        {
                            directionNow = (int)Direction.Up;
                            break;
                        }
                }
                flag = false;
            }
        }

        public void Turn()
        {
            IdentifyDirection(GameForm.rand.Next(0, 4));
        }

        public void IdentifyDirection(int direction)
        {
            switch (direction)
            {
                case (int)Direction.Down:
                    {
                        dy = delta;
                        dx = 0;
                        directionNow = (int)Direction.Down;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        dy = 0;
                        dx = -delta;
                        directionNow = (int)Direction.Left;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        dy = 0;
                        dx = delta;
                        directionNow = (int)Direction.Right;
                        break;
                    }
                case (int)Direction.Up:
                    {
                        dy = -delta;
                        dx = 0;
                        directionNow = (int)Direction.Up;
                        break;
                    }

                default:
                    break;
            }
        }

        public virtual void Run()
        {
            while (this != null)
            {
                if (!(this is Bullet) && GameForm.rand.Next(0, 50) == 1)
                {
                    Turn();
                }
                OnCheck();
                Move();
            }
        }


    }
}
