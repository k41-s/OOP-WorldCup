namespace WorldCupManager
{
    partial class EntryForm
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
            label2 = new Label();
            label3 = new Label();
            btnConfirm = new Button();
            btnCancel = new Button();
            cbCategory = new ComboBox();
            cbLanguage = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(80, 9);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 1;
            label1.Text = "Application Settings";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 68);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 2;
            label2.Text = "Category:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 170);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 4;
            label3.Text = "Language:";
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(12, 233);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(113, 59);
            btnConfirm.TabIndex = 5;
            btnConfirm.Text = "Confirm Settings";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(168, 233);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(113, 59);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnExit_Click;
            // 
            // cbCategory
            // 
            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategory.FormattingEnabled = true;
            cbCategory.Items.AddRange(new object[] { "Men", "Women" });
            cbCategory.Location = new Point(80, 65);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(201, 23);
            cbCategory.TabIndex = 7;
            // 
            // cbLanguage
            // 
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { "English", "Croatian" });
            cbLanguage.Location = new Point(80, 167);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(201, 23);
            cbLanguage.TabIndex = 8;
            // 
            // EntryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 304);
            Controls.Add(cbLanguage);
            Controls.Add(cbCategory);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "EntryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EntryForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnConfirm;
        private Button btnCancel;
        private ComboBox cbCategory;
        private ComboBox cbLanguage;
    }
}