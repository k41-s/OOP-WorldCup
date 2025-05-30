namespace WorldCupManager
{
    partial class PlayerStatsControl
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
            pbPlayerImg = new PictureBox();
            lblPlayerStats = new Label();
            ((System.ComponentModel.ISupportInitialize)pbPlayerImg).BeginInit();
            SuspendLayout();
            // 
            // pbPlayerImg
            // 
            pbPlayerImg.Location = new Point(0, 0);
            pbPlayerImg.Name = "pbPlayerImg";
            pbPlayerImg.Size = new Size(55, 55);
            pbPlayerImg.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPlayerImg.TabIndex = 0;
            pbPlayerImg.TabStop = false;
            pbPlayerImg.Click += pbPlayerImg_Click;
            // 
            // lblPlayerStats
            // 
            lblPlayerStats.AutoSize = true;
            lblPlayerStats.Font = new Font("Segoe UI", 9F);
            lblPlayerStats.Location = new Point(88, 11);
            lblPlayerStats.Name = "lblPlayerStats";
            lblPlayerStats.Size = new Size(24, 32);
            lblPlayerStats.TabIndex = 1;
            lblPlayerStats.Text = "-";
            // 
            // PlayerStatsControl
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblPlayerStats);
            Controls.Add(pbPlayerImg);
            Name = "PlayerStatsControl";
            Size = new Size(500, 59);
            Load += PlayerStatsControl_Load;
            ((System.ComponentModel.ISupportInitialize)pbPlayerImg).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbPlayerImg;
        private Label lblPlayerStats;
    }
}
