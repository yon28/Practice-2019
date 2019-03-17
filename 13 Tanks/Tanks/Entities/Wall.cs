﻿using System;
using System.Drawing;

namespace Tanks
{
    public class Wall : Obj
    { 
        public Wall(Point position) : base(position)
        {
        }

        public Wall()
        {
        }

        public void Replace()
        {
            OnReplaceNeeded();
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
                if (!(sender is Bullet))
                {
                    ((Dynamic)sender).Deviate();
                }
                else 
                {
                    Replace();
                    Stop();
                    ((Bullet)sender).Stop();
                    ((Bullet)sender).Move();
                }
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

       

    }
}

