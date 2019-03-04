using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{

    public class ViewTank : ViewDynamic
    {
        public ViewTank(Panel map) : base(map)
        {
            pictBox.Image = Resources.U2;
        }

        // Смена картинки при смене направления движения

        protected override void ChangePicture()
        {
            switch (Model.DirectionNow)
            {
                case (int)Direction.Up:
                    {
                        pictBox.Image = Resources.U2;
                        break;
                    }
                case ((int)Direction.Down):
                    {
                        pictBox.Image = Resources.D2;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        pictBox.Image = Resources.R2;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        pictBox.Image = Resources.L2;
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
