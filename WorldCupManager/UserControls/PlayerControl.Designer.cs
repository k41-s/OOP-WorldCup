namespace WorldCupManager
{
    partial class PlayerControl
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
            lblName = new Label();
            pbPlayerImg = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPlayerImg).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 8F);
            lblName.Location = new Point(31, 3);
            lblName.Name = "lblName";
            lblName.Size = new Size(11, 13);
            lblName.TabIndex = 0;
            lblName.Text = "-";
            // 
            // pbPlayerImg
            // 
            pbPlayerImg.Location = new Point(0, 3);
            pbPlayerImg.Name = "pbPlayerImg";
            pbPlayerImg.Size = new Size(25, 19);
            pbPlayerImg.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlayerImg.TabIndex = 1;
            pbPlayerImg.TabStop = false;
            pbPlayerImg.Click += pbPlayerImg_Click;
            // 
            // PlayerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbPlayerImg);
            Controls.Add(lblName);
            Margin = new Padding(3, 1, 3, 1);
            Name = "PlayerControl";
            Size = new Size(237, 25);
            Load += PlayerControl_Load;
            MouseDown += PlayerControl_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pbPlayerImg).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private PictureBox pbPlayerImg;
    }
}
