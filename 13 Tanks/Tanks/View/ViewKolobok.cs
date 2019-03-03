using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{
    class ViewKolobok : ViewDynamic
    {
        public ViewKolobok(Panel map) : base(map)
        {
            pictBox.Image = Resources.D;
        }

        // Смена картинки при смене направления движения
        protected override void ChangePicture()
        {
            switch (Model.DirectionNow)
            {
                case (int)Direction.Up:
                    {
                        pictBox.Image = Resources.U;
                        break;
                    }
                case ((int)Direction.Down):
                    {
                        pictBox.Image = Resources.D;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        pictBox.Image = Resources.R;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        pictBox.Image = Resources.L;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
