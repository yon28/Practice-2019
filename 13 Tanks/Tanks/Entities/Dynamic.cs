﻿using System;
using System.Drawing;
using System.Threading;

namespace Tanks
{
    public class Dynamic : Obj
    {
        private const int width = 30;
        private const int height = 30;
        protected bool flag = false;
        protected int delta = 1;
        private int directionNow;

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

        public virtual void Die()/**/
        {
            OnReplaceNeeded();
        }

        public virtual void Run()
        {
            while (run)
            {
                if ((this is Kolobok || this is Tank) && GameForm.rand.Next(0, 50) == GameForm.rand.Next(0, 50))
                {
                    Turn();
                }
                if ((this is Tank) && GameForm.rand.Next(0, 40) == 2)
                {
                    GameForm.game.ShootTank(this);
                }
                OnCheck();
                Move();
            }
        }

        protected virtual void Move()
        {
            if (position.X + dx >= 0 && position.X + this.Width + dx < MapSize.X)
            {
                position.X += dx;
            }
            else
            {
                Turn();
            }
            if (position.Y + dy >= 0 && position.Y + this.Height + dy < MapSize.Y)
            {
                position.Y += dy;
            }
            else
            {
                Turn();
            }
            flag = true;
            OnPositionChanged();
            Thread.Sleep(100 / GameForm.Speed);
        }

        public void Turn()
        {
            IdentifyDirection(GameForm.rand.Next(0, 4));
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

        public event EventHandler CheckPosition;

        protected virtual void OnCheck()
        {
            if (CheckPosition != null)
            {
                CheckPosition(this, new PositionChangedEventArgs(new Rectangle(this.position.X + dx, this.position.Y + dy, this.Width, this.Height)));
            }
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

                if (this is Tank && sender is BulletK)
                {
                    ((Dynamic)this).Stop();
                    ((Bullet)sender).Stop();
                }
                if (sender is Tank && this is BulletK)
                {
                    ((Dynamic)sender).Stop();
                    ((Bullet)this).Stop();
                }
                if (sender is Kolobok && this is BulletT)
                {
                    ((Bullet)this).Stop();
                    (sender as Kolobok).Stop();
                }
                if (sender is Kolobok && this is Tank)
                {
                    (sender as Kolobok).Stop();
                }
                if (sender is Tank && !(this is Bullet))
                {
                    this.Deviate();
                    ((Dynamic)sender).Deviate();
                }
            }
        }

    }
}
