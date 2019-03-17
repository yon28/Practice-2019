using System;
using System.Drawing;
using System.Threading;

namespace Tanks
{
    public class Dynamic : Obj
    {
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
                if ((this is Tank) && GameForm.rand.Next(0, 100) == 2)
                {
                    Thread thread = new Thread(new ThreadStart(((Tank)this).Shoot));
                    GameForm.game.arrThread.Add(thread);
                    thread.Start();
                }
                OnCheck();
                Move();
            }
        }

        public void Shoot()
        {
            Rectangle rect = new Rectangle(Position.X, Position.Y, new Bullet().Width, (new Bullet()).Height);
            if (this is Kolobok)
            {
                var bullet = new BulletK(rect.Location);/**/
                AddBullet(bullet);
            }
            else if (this is Tank)
            {
                var bullet = new BulletT(rect.Location);/**/
                AddBullet(bullet);
            }
        }

        public void AddBullet(Bullet bullet)
        {
            bullet.IdentifyDirection(DirectionNow);
            bullet.Name = DateTime.Now.ToString();
            GameForm.game.arrBullet.Add(bullet);
            GameForm.game.SubscribeBulletPos(bullet);
            Thread thread = new Thread(new ThreadStart(bullet.Run));
            thread.Name = bullet.Name;
            GameForm.game.arrThread.Add(thread);
            thread.Start();
            ViewShoot();
        }
        
        delegate void SetCallback();
        public void ViewShoot()
        {
            if (GameForm.viewGame.panelMap.InvokeRequired)
            {
                SetCallback d = new SetCallback(ViewShoot);
                GameForm.viewGame.panelMap.Invoke(d, new object[] { });
            }
            else
            {
                GameForm.viewGame.ViewShoot(this);
            }
        }

        public virtual void Move()
        {
            if (position.X + dx >= 0 && position.X + Width + dx < MapSize.X)
            {
                position.X += dx;
            }
            else
            {
                Turn();
            }
            if (position.Y + dy >= 0 && position.Y + Height + dy < MapSize.Y)
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
                CheckPosition(this, new PositionChangedEventArgs(new Rectangle(position.X + dx, position.Y + dy, Width, Height)));
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
                if (this is BulletT && sender is Kolobok)
                {
                    Stop();
                    Move();
                    ((Dynamic)sender).Stop();
                }
                if ( !(this is Bullet)&& sender is Tank)
                {
                    Deviate();
                    ((Dynamic)sender).Deviate();
                }
            }
        }
    }
}
