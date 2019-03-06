using System;
using System.Drawing;

namespace Tanks
{
    public class Obj
    {
        protected int dy =0;
        protected int dx =0;
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
        private const int width = 40;
        private const int height = 40;
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
        }

        public Obj()
        {
            this.position = new Point(GameForm.rand.Next(0, GameForm.X - Width - 2), GameForm.rand.Next(0, GameForm.Y - Height - 2));
        }

        public Obj(Point position)
        {
            this.position = position;
        }

        protected bool CheckCrossing(Point p)
        {
            if (this.Position.X + this.Width >= p.X && this.Position.X <= p.X)
            {
                if (this.Position.Y + this.Height >= p.Y && this.Position.Y <= p.Y)
                {
                    return true;
                }
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
            var positionArgs = e as PositionChangedEventArgs;
            if (positionArgs == null)
                return;
            if (CollidesWith(positionArgs.NewRectangle))
            {
                if (!(sender is Bullet))
                {
                    ((Dynamic)sender).Deviate();
                }
                else  if (this is Wall)
                {
                    ((Bullet)sender).Stop();
                }
            }
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
