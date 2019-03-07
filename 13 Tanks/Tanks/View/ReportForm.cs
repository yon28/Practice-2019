using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Tanks
{
    public partial class ReportForm : Form
    {
        public List<ReportLine> report = new List<ReportLine>();
        public Thread reportThread;
        public ReportForm()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            GameForm.game.UpdateReport();
            reportThread = new Thread(new ThreadStart(GameForm.game.UpdateReport));
            reportThread.Start();
        }

        delegate void SetCallback();
        public void UpdateReport()
        {
            if (this.dgvReport.InvokeRequired)
            {
                SetCallback d = new SetCallback(UpdateReport);
                dgvReport.Invoke(d, new object[] { });
            }
            else
            {
                dgvReport.DataSource = null;
                dgvReport.DataSource = GameForm.game?.report;
            }
        }

    }
}
