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
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(246, 45);
            label1.TabIndex = 2;
            label1.Text = "Match Rankings";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(734, 9);
            label2.Name = "label2";
            label2.Size = new Size(242, 45);
            label2.TabIndex = 3;
            label2.Text = "Player Rankings";
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(12, 1045);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(633, 101);
            btnPrint.TabIndex = 4;
            btnPrint.Text = "Print rankings";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(703, 1045);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(633, 101);
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
            flpMatches.Location = new Point(12, 140);
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
            flpPlayers.Location = new Point(734, 140);
            flpPlayers.Name = "flpPlayers";
            flpPlayers.Size = new Size(602, 877);
            flpPlayers.TabIndex = 7;
            flpPlayers.WrapContents = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(734, 91);
            label3.Name = "label3";
            label3.Size = new Size(441, 32);
            label3.TabIndex = 0;
            label3.Text = "Name, Goals, Yellow Cards, Appearances";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 91);
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
            // RankingListForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1348, 1158);
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
    }
}
