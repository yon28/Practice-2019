namespace Tanks
{
    partial class GameForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.p_Map = new System.Windows.Forms.Panel();
            this.l_points = new System.Windows.Forms.Label();
            this.p_Map.SuspendLayout();
            this.SuspendLayout();
            // 
            // p_Map
            // 
            this.p_Map.BackColor = System.Drawing.Color.Black;
            this.p_Map.Controls.Add(this.l_points);
            this.p_Map.Location = new System.Drawing.Point(12, 11);
            this.p_Map.Name = "p_Map";
            this.p_Map.Size = new System.Drawing.Size(520, 520);
            this.p_Map.TabIndex = 1;
            // 
            // l_points
            // 
            this.l_points.AutoSize = true;
            this.l_points.BackColor = System.Drawing.Color.Black;
            this.l_points.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.l_points.Location = new System.Drawing.Point(471, 0);
            this.l_points.Name = "l_points";
            this.l_points.Size = new System.Drawing.Size(0, 13);
            this.l_points.TabIndex = 1;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 542);
            this.Controls.Add(this.p_Map);
            this.Name = "GameForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.p_Map.ResumeLayout(false);
            this.p_Map.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel p_Map;
        private System.Windows.Forms.Label l_points;
    }
}

