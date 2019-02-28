using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{
    class ViewWall : ViewObj
    {
        public ViewWall(Panel map) : base(map)
        {
            picBox.Image = Resources.wall; ;
        }
    }
}
