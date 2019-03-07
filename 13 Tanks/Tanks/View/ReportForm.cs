using System;
using System.Threading;
using System.Windows.Forms;

namespace Tanks
{
    public partial class ReportForm : Form
    {
        private static ViewReportForm viewReportForm;
        internal static ViewReportForm ViewReportForm
        {
            get => viewReportForm;
            set => viewReportForm = value;
        }

        private void Report_Load(object sender, EventArgs e)
        {
            //GameForm.game.UpdateReport();
            //UpdateStatus();
        }

        public ReportForm()
        {
            InitializeComponent();
            //ViewReportForm = new ViewReportForm(dgvReport);
            //ViewReportForm.Model = GameForm.game;
        }

        private void UpdateStatus()
        {
            dgvReport.DataSource = null;
            dgvReport.DataSource = GameForm.game?.report;
        }
    }
}
