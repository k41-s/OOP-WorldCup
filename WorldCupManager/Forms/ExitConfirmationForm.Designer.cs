namespace WorldCupManager
{
    partial class ExitConfirmationForm
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
            btnConfirm = new Button();
            btnClose = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new Font("Segoe UI", 10F);
            btnConfirm.Location = new Point(12, 134);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(316, 131);
            btnConfirm.TabIndex = 0;
            btnConfirm.Text = "Yes";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnClose
            // 
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.Location = new Point(412, 134);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(316, 131);
            btnClose.TabIndex = 1;
            btnClose.Text = "No, go back";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(133, 33);
            label1.Name = "label1";
            label1.Size = new Size(487, 50);
            label1.TabIndex = 2;
            label1.Text = "Do you really want to exit?";
            // 
            // ExitConfirmationForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(740, 278);
            Controls.Add(label1);
            Controls.Add(btnClose);
            Controls.Add(btnConfirm);
            Name = "ExitConfirmationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ExitConfirmationForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConfirm;
        private Button btnClose;
        private Label label1;
    }
}