﻿using System.Windows.Forms;
using Tanks.Properties;
namespace Tanks
{
    public class ViewBullet : ViewDynamic
    {
        int DirectionNow; 
        public ViewBullet (Dynamic dynamic, Panel map):base( map)
        {
            DirectionNow = dynamic.DirectionNow;
            switch (DirectionNow)
            {
                case (int)Direction.Up:
                    {
                        pictBox.Image = Resources.PU;
                        break;
                    }
                case ((int)Direction.Down):
                    {
                        pictBox.Image = Resources.P;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        pictBox.Image = Resources.PR;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        pictBox.Image = Resources.PL;
                        break;
                    }
                default:
                    break;
            }
            map.Controls.Add(pictBox);
            pictBox.Height = Model.Height;
            pictBox.Width = Model.Width;
        }
    }
}
