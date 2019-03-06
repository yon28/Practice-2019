using System.Windows.Forms;

namespace Tanks
{
    class KolobokController
    {
        public void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (sender is Kolobok)
            {
                switch (e.KeyData)
                {
                    case Keys.Up:
                        {
                            ((Kolobok)sender).IdentifyDirection((int)Direction.Up);
                            break;
                        }
                    case Keys.Down:
                        {
                            ((Kolobok)sender).IdentifyDirection((int)Direction.Down);
                            break;
                        }
                    case Keys.Left:
                        {
                            ((Kolobok)sender).IdentifyDirection((int)Direction.Left);
                            break;
                        }
                    case Keys.Right:
                        {
                            ((Kolobok)sender).IdentifyDirection((int)Direction.Right);
                            break;
                        }
                    case Keys.Space:
                        {
                            ((Kolobok)sender).Shoot();
                            break;
                        }
                    default:
                        break;
                }
            }
        }



    }
}

