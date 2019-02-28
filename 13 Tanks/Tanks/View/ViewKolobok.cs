using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{
    class ViewKolobok : ViewDynamic
    {
        public ViewKolobok(Panel map) : base(map)
        {
            picBox.Image = Resources.D;
        }

        // Смена картинки при смене направления движения
        protected override void ChangePicture()
        {
            switch (Model.DirectionNow)
            {
                case (int)Direction.Stay:
                    {
                        picBox.Image = Resources.U;
                        break;
                    }
                case (int)Direction.Up:
                    {
                        picBox.Image = Resources.U;
                        break;
                    }
                case ((int)Direction.Down):
                    {
                        picBox.Image = Resources.D;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        picBox.Image = Resources.R;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        picBox.Image = Resources.L;
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
