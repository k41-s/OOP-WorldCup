namespace WorldCupManager.UserControls
{
    partial class MatchStatsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMatchStats = new Label();
            SuspendLayout();
            // 
            // lblMatchStats
            // 
            lblMatchStats.AutoSize = true;
            lblMatchStats.Font = new Font("Segoe UI", 9F);
            lblMatchStats.Location = new Point(7, 13);
            lblMatchStats.Name = "lblMatchStats";
            lblMatchStats.Size = new Size(24, 32);
            lblMatchStats.TabIndex = 2;
            lblMatchStats.Text = "-";
            lblMatchStats.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MatchStatsControl
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblMatchStats);
            Name = "MatchStatsControl";
            Size = new Size(590, 59);
            Load += MatchStatsControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMatchStats;
    }
}
