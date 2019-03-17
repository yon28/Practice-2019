using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{
    class ViewWater : ViewObj
    {
        public ViewWater(Panel map) : base(map)
        {
            pictBox.Image = Resources.Water; 
        }
    }
}
