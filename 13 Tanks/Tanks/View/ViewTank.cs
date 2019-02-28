using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{

    class ViewTank : ViewDynamic
    {
        public ViewTank(Panel map) : base(map)
        {
            picBox.Image = Resources.U2;
        }

        // Смена картинки при смене направления движения

        protected override void ChangePicture()
        {
            switch (Model.DirectionNow)
            {
                case (int)Direction.Up:
                    {
                        picBox.Image = Resources.U2;
                        break;
                    }
                case ((int)Direction.Down):
                    {
                        picBox.Image = Resources.D2;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        picBox.Image = Resources.R2;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        picBox.Image = Resources.L2;
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
