namespace WorldCupManager
{
    partial class RankingListForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            btnPrint = new Button();
            btnExit = new Button();
            flpMatches = new FlowLayoutPanel();
            flpPlayers = new FlowLayoutPanel();
            label3 = new Label();
            label4 = new Label();
            printDocument = new System.Drawing.Printing.PrintDocument();
            printDialog = new PrintDialog();
            btnSettings = new Button();
            lblTeamName = new Label();
            label5 = new Label();
            label6 = new Label();
            lblCategory = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(11, 58);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(121, 21);
            label1.TabIndex = 2;
            label1.Text = "Match Rankings";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(400, 58);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(121, 21);
            label2.TabIndex = 3;
            label2.Text = "Player Rankings";
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(11, 544);
            btnPrint.Margin = new Padding(2, 1, 2, 1);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(250, 47);
            btnPrint.TabIndex = 4;
            btnPrint.Text = "Print rankings";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(467, 544);
            btnExit.Margin = new Padding(2, 1, 2, 1);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(250, 47);
            btnExit.TabIndex = 5;
            btnExit.Text = "Exit Application";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // flpMatches
            // 
            flpMatches.AutoScroll = true;
            flpMatches.BackColor = Color.LightGray;
            flpMatches.FlowDirection = FlowDirection.TopDown;
            flpMatches.Location = new Point(11, 120);
            flpMatches.Margin = new Padding(2, 1, 2, 1);
            flpMatches.Name = "flpMatches";
            flpMatches.Size = new Size(324, 411);
            flpMatches.TabIndex = 6;
            flpMatches.WrapContents = false;
            // 
            // flpPlayers
            // 
            flpPlayers.AutoScroll = true;
            flpPlayers.BackColor = Color.LightGray;
            flpPlayers.FlowDirection = FlowDirection.TopDown;
            flpPlayers.Location = new Point(400, 120);
            flpPlayers.Margin = new Padding(2, 1, 2, 1);
            flpPlayers.Name = "flpPlayers";
            flpPlayers.Size = new Size(324, 411);
            flpPlayers.TabIndex = 7;
            flpPlayers.WrapContents = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(400, 97);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(221, 15);
            label3.TabIndex = 0;
            label3.Text = "Name, Goals, Yellow Cards, Appearances";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 97);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(231, 15);
            label4.TabIndex = 8;
            label4.Text = "Location, Visitors, Home team, Away team";
            // 
            // printDocument
            // 
            printDocument.EndPrint += printDocument_EndPrint;
            printDocument.PrintPage += printDocument_PrintPage;
            // 
            // printDialog
            // 
            printDialog.AllowSelection = true;
            printDialog.Document = printDocument;
            printDialog.PrintToFile = true;
            printDialog.UseEXDialog = true;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(289, 544);
            btnSettings.Margin = new Padding(2, 1, 2, 1);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(150, 47);
            btnSettings.TabIndex = 9;
            btnSettings.Text = "Change settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // lblTeamName
            // 
            lblTeamName.AutoSize = true;
            lblTeamName.Font = new Font("Segoe UI", 14F);
            lblTeamName.Location = new Point(137, 9);
            lblTeamName.Name = "lblTeamName";
            lblTeamName.Size = new Size(121, 25);
            lblTeamName.TabIndex = 10;
            lblTeamName.Text = "[team_name]";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label5.Location = new Point(11, 9);
            label5.Name = "label5";
            label5.Size = new Size(120, 25);
            label5.TabIndex = 11;
            label5.Text = "Team Name:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label6.Location = new Point(400, 9);
            label6.Name = "label6";
            label6.Size = new Size(99, 25);
            label6.TabIndex = 12;
            label6.Text = "Category:";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 14F);
            lblCategory.Location = new Point(505, 9);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(97, 25);
            lblCategory.TabIndex = 13;
            lblCategory.Text = "[category]";
            // 
            // RankingListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(733, 603);
            Controls.Add(lblCategory);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(lblTeamName);
            Controls.Add(btnSettings);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(flpPlayers);
            Controls.Add(flpMatches);
            Controls.Add(btnExit);
            Controls.Add(btnPrint);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "RankingListForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            Load += RankingListForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Button btnPrint;
        private Button btnExit;
        private FlowLayoutPanel flpMatches;
        private FlowLayoutPanel flpPlayers;
        private Label label3;
        private Label label4;
        private System.Drawing.Printing.PrintDocument printDocument;
        private PrintDialog printDialog;
        private Button btnSettings;
        private Label lblTeamName;
        private Label label5;
        private Label label6;
        private Label lblCategory;
    }
}
