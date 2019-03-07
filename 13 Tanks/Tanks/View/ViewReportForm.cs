using System.Windows.Forms;

namespace Tanks
{
    class ViewReportForm : View<Game>
    {
        public DataGridView dgvReport;
        public ViewReportForm(DataGridView dgvReport)
        {
            this.dgvReport = dgvReport;
        }

        protected override void Update()
        {
            GameForm.game.UpdateReport();
            dgvReport.DataSource = null;
            dgvReport.DataSource = GameForm.game?.report;
        }
    }
}
