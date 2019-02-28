using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{
    class ViewApple : ViewObj
    {
        public ViewApple(Panel map) : base(map)
        {
            picBox.Image = Resources.Apple;
            map.Controls.Add(picBox);
        }
    }
}
