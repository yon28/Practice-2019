using System.Windows.Forms;
using Tanks.Properties;
namespace Tanks
{
    class ViewBullet : ViewDynamic
    {
        public ViewBullet (Panel map) : base(map)
        {
            int DirectionNow = GameForm.game.Kolobok.DirectionNow;
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
            base.Show();
        }


    }
}
