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
            label1.Location = new Point(20, 124);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(246, 45);
            label1.TabIndex = 2;
            label1.Text = "Match Rankings";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(743, 124);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(242, 45);
            label2.TabIndex = 3;
            label2.Text = "Player Rankings";
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(20, 1161);
            btnPrint.Margin = new Padding(4, 2, 4, 2);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(464, 100);
            btnPrint.TabIndex = 4;
            btnPrint.Text = "Print rankings";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(867, 1161);
            btnExit.Margin = new Padding(4, 2, 4, 2);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(464, 100);
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
            flpMatches.Location = new Point(20, 256);
            flpMatches.Margin = new Padding(4, 2, 4, 2);
            flpMatches.Name = "flpMatches";
            flpMatches.Size = new Size(602, 877);
            flpMatches.TabIndex = 6;
            flpMatches.WrapContents = false;
            // 
            // flpPlayers
            // 
            flpPlayers.AutoScroll = true;
            flpPlayers.BackColor = Color.LightGray;
            flpPlayers.FlowDirection = FlowDirection.TopDown;
            flpPlayers.Location = new Point(743, 256);
            flpPlayers.Margin = new Padding(4, 2, 4, 2);
            flpPlayers.Name = "flpPlayers";
            flpPlayers.Size = new Size(602, 877);
            flpPlayers.TabIndex = 7;
            flpPlayers.WrapContents = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(743, 207);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(441, 32);
            label3.TabIndex = 0;
            label3.Text = "Name, Goals, Yellow Cards, Appearances";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 207);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(460, 32);
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
            btnSettings.Location = new Point(537, 1161);
            btnSettings.Margin = new Padding(4, 2, 4, 2);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(279, 100);
            btnSettings.TabIndex = 9;
            btnSettings.Text = "Change settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // lblTeamName
            // 
            lblTeamName.AutoSize = true;
            lblTeamName.Font = new Font("Segoe UI", 14F);
            lblTeamName.Location = new Point(254, 19);
            lblTeamName.Margin = new Padding(6, 0, 6, 0);
            lblTeamName.Name = "lblTeamName";
            lblTeamName.Size = new Size(239, 51);
            lblTeamName.TabIndex = 10;
            lblTeamName.Text = "[team_name]";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label5.Location = new Point(20, 19);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(243, 51);
            label5.TabIndex = 11;
            label5.Text = "Team Name:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label6.Location = new Point(743, 19);
            label6.Margin = new Padding(6, 0, 6, 0);
            label6.Name = "label6";
            label6.Size = new Size(196, 51);
            label6.TabIndex = 12;
            label6.Text = "Category:";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 14F);
            lblCategory.Location = new Point(938, 19);
            lblCategory.Margin = new Padding(6, 0, 6, 0);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(189, 51);
            lblCategory.TabIndex = 13;
            lblCategory.Text = "[category]";
            // 
            // RankingListForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1361, 1286);
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
            Margin = new Padding(4, 2, 4, 2);
            Name = "RankingListForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rankings";
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
