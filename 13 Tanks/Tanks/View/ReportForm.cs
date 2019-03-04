using System;
using System.Threading;
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
            UpdateStatus();
        }

        public ReportForm()
        {
            InitializeComponent();

        }

        private void UpdateStatus()
        {
            dgvReport.DataSource = null;
            dgvReport.DataSource = game?.report;
        }
    }
}
