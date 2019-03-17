using System;
using System.Drawing;
using System.Linq;

namespace Tanks
{
    public class Obj
    {
        protected int dy = 0;
        protected int dx = 0;
        private Point mapSize;
        public Point MapSize
        {
            get { return mapSize; }
            set { mapSize = value; }
        }
        public bool run = true;
        protected Point position;
        public Point Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPositionChanged();
            }
        }
        private const int width = 20;
        private const int height = 20;
        public int Width
        {
            get { return width; }
            set { }
        }
        public int Height
        {
            get { return height; }
            set { }
        }

        public virtual void Stop()
        {
            dy = 0;
            dx = 0;
            position.X = -30;
            position.Y = -30;
            run = false;
            GameForm.game.UnSubscribePos(this);
            if (this is Wall) { GameForm.game.ArrWall.Remove((Wall)this); }
            if (this is Apple) { GameForm.game.ArrApple.Remove((Apple)this); }
            if (this is Tank) { GameForm.game.ArrTank.Remove((Tank)this); }
            //if (this is Bullet) { GameForm.game.ArrBullet.Remove((Bullet)this); }
        }

        public Obj()
        {
            position = new Point(GameForm.rand.Next(0, GameForm.X - Width - 1), GameForm.rand.Next(0, GameForm.Y - Height - 1));
        }

        public Obj(Point position)
        {
            this.position = position;
        }

        protected bool CheckCrossing(Point p)
        {
            if (p.X - Position.X >= 0 && p.X - Position.X <= Width && p.Y - Position.Y >= 0 && p.Y - Position.Y <= Height)
            {
                return true;
            }
            return false;
        }

        public bool CollidesWith(Rectangle rect)
        {
            if (CheckCrossing(new Point(rect.Left, rect.Top)) ||
                CheckCrossing(new Point(rect.Right, rect.Top)) ||
                CheckCrossing(new Point(rect.Right, rect.Bottom)) ||
                CheckCrossing(new Point(rect.Left, rect.Bottom)))
            {
                return true;
            }
            return false;
        }

        public virtual void OnCheckPosition(object sender, EventArgs e)
        {
        }

        public event EventHandler PositionChanged;
        protected virtual void OnPositionChanged()
        {
            if (PositionChanged != null)
                PositionChanged(this, new EventArgs());
        }

        public event EventHandler ReplaceNeeded;
        public void OnReplaceNeeded()
        {
            if (ReplaceNeeded != null)
                ReplaceNeeded(this, new EventArgs());
        }

    }
}
