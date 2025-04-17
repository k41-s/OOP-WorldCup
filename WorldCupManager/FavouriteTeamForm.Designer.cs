namespace WorldCupManager
{
    partial class FavouriteTeamForm
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
            label1 = new Label();
            cbTeams = new ComboBox();
            btnConfirm = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.Location = new Point(85, 47);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(521, 59);
            label1.TabIndex = 0;
            label1.Text = "Select your favourite team";
            // 
            // cbTeams
            // 
            cbTeams.FormattingEnabled = true;
            cbTeams.Location = new Point(22, 211);
            cbTeams.Margin = new Padding(6);
            cbTeams.Name = "cbTeams";
            cbTeams.Size = new Size(606, 40);
            cbTeams.TabIndex = 1;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(22, 378);
            btnConfirm.Margin = new Padding(6);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(258, 124);
            btnConfirm.TabIndex = 2;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(373, 378);
            btnExit.Margin = new Padding(6);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(258, 124);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // FavouriteTeamForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(654, 527);
            Controls.Add(btnExit);
            Controls.Add(btnConfirm);
            Controls.Add(cbTeams);
            Controls.Add(label1);
            Margin = new Padding(4, 2, 4, 2);
            Name = "FavouriteTeamForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FavouriteTeamForm";
            Load += FavouriteTeamForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cbTeams;
        private Button btnConfirm;
        private Button btnExit;
    }
}