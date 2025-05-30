namespace WorldCupManager
{
    partial class LoadingForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbLoading = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbLoading).BeginInit();
            SuspendLayout();
            // 
            // pbLoading
            // 
            pbLoading.Dock = DockStyle.Fill;
            pbLoading.Location = new Point(0, 0);
            pbLoading.Name = "pbLoading";
            pbLoading.Size = new Size(279, 203);
            pbLoading.SizeMode = PictureBoxSizeMode.CenterImage;
            pbLoading.TabIndex = 0;
            pbLoading.TabStop = false;
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(279, 203);
            Controls.Add(pbLoading);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoadingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoadingForm";
            TopMost = true;
            FormClosed += LoadingForm_FormClosed;
            Load += LoadingForm_Load;
            ((System.ComponentModel.ISupportInitialize)pbLoading).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbLoading;
    }
}