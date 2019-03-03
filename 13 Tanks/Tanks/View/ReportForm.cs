using System;
using System.Windows.Forms;

namespace Tanks
{
    public partial class ReportForm : Form
    {
        Game game;
        private void Report_Load(object sender, EventArgs e)
        {
            game = new Game();
            game.UpdateReport();
            dgvReport.DataSource = game.report;
        }

        public ReportForm()
        {
            InitializeComponent();
        }
    }
}
