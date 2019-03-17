using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{
    class ViewWall : ViewObj
    {
        public ViewWall(Panel map) : base(map)
        {
            pictBox.Image = Resources.wall; 
        }
    }
}
