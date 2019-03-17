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
            this.components = new System.ComponentModel.Container();
            this.p_Map = new System.Windows.Forms.Panel();
            this.lbGameOver = new System.Windows.Forms.Label();
            this.lbScore = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.l_points = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.p_Map.SuspendLayout();
            this.SuspendLayout();
            // 
            // p_Map
            // 
            this.p_Map.BackColor = System.Drawing.Color.Black;
            this.p_Map.Controls.Add(this.lbGameOver);
            this.p_Map.Controls.Add(this.lbScore);
            this.p_Map.Controls.Add(this.btnStart);
            this.p_Map.Controls.Add(this.l_points);
            this.p_Map.Location = new System.Drawing.Point(12, 11);
            this.p_Map.Name = "p_Map";
            this.p_Map.Size = new System.Drawing.Size(520, 520);
            this.p_Map.TabIndex = 1;
            this.p_Map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p_Map_MouseClick);
            // 
            // lbGameOver
            // 
            this.lbGameOver.AutoSize = true;
            this.lbGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbGameOver.ForeColor = System.Drawing.SystemColors.Control;
            this.lbGameOver.Location = new System.Drawing.Point(186, 214);
            this.lbGameOver.Name = "lbGameOver";
            this.lbGameOver.Size = new System.Drawing.Size(146, 26);
            this.lbGameOver.TabIndex = 4;
            this.lbGameOver.Text = "GAME OVER";
            this.lbGameOver.Visible = false;
            // 
            // lbScore
            // 
            this.lbScore.AutoSize = true;
            this.lbScore.BackColor = System.Drawing.Color.Transparent;
            this.lbScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lbScore.ForeColor = System.Drawing.SystemColors.Control;
            this.lbScore.Location = new System.Drawing.Point(482, 0);
            this.lbScore.Name = "lbScore";
            this.lbScore.Size = new System.Drawing.Size(20, 24);
            this.lbScore.TabIndex = 3;
            this.lbScore.Text = "0";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnStart.Location = new System.Drawing.Point(194, 189);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(127, 51);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "New Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // l_points
            // 
            this.l_points.AutoSize = true;
            this.l_points.BackColor = System.Drawing.Color.Black;
            this.l_points.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.l_points.Location = new System.Drawing.Point(458, 0);
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
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.Move += new System.EventHandler(this.GameForm_Move);
            this.p_Map.ResumeLayout(false);
            this.p_Map.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel p_Map;
        private System.Windows.Forms.Label l_points;
        private System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.Label lbScore;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbGameOver;
    }
}

