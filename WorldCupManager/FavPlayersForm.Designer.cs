namespace WorldCupManager
{
    partial class FavPlayersForm
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
            components = new System.ComponentModel.Container();
            contextMenuStrip = new ContextMenuStrip(components);
            markFavouriteToolStripMenuItem = new ToolStripMenuItem();
            unmarkFavouriteToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            flpAllPlayers = new FlowLayoutPanel();
            flpFavPlayers = new FlowLayoutPanel();
            btnSave = new Button();
            btnExit = new Button();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(32, 32);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { markFavouriteToolStripMenuItem, unmarkFavouriteToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(216, 48);
            contextMenuStrip.Opening += contextMenuStrip_Opening;
            // 
            // markFavouriteToolStripMenuItem
            // 
            markFavouriteToolStripMenuItem.Name = "markFavouriteToolStripMenuItem";
            markFavouriteToolStripMenuItem.Size = new Size(215, 22);
            markFavouriteToolStripMenuItem.Text = "Mark player as favourite";
            markFavouriteToolStripMenuItem.Click += markFavouriteToolStripMenuItem_Click;
            // 
            // unmarkFavouriteToolStripMenuItem
            // 
            unmarkFavouriteToolStripMenuItem.Name = "unmarkFavouriteToolStripMenuItem";
            unmarkFavouriteToolStripMenuItem.Size = new Size(215, 22);
            unmarkFavouriteToolStripMenuItem.Text = "Unmark player as favourite";
            unmarkFavouriteToolStripMenuItem.Click += unmarkFavouriteToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(59, 9);
            label1.Name = "label1";
            label1.Size = new Size(533, 28);
            label1.TabIndex = 2;
            label1.Text = "Drap and drop or right click to select three favourite players";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 48);
            label2.Name = "label2";
            label2.Size = new Size(131, 21);
            label2.TabIndex = 3;
            label2.Text = "Favourite Players:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(352, 48);
            label3.Name = "label3";
            label3.Size = new Size(85, 21);
            label3.TabIndex = 4;
            label3.Text = "All Players:";
            // 
            // flpAllPlayers
            // 
            flpAllPlayers.AllowDrop = true;
            flpAllPlayers.AutoScroll = true;
            flpAllPlayers.BackColor = Color.LightGray;
            flpAllPlayers.FlowDirection = FlowDirection.TopDown;
            flpAllPlayers.Location = new Point(352, 72);
            flpAllPlayers.Name = "flpAllPlayers";
            flpAllPlayers.Size = new Size(286, 410);
            flpAllPlayers.TabIndex = 5;
            flpAllPlayers.WrapContents = false;
            flpAllPlayers.DragDrop += FlowLayoutPanel_DragDrop;
            flpAllPlayers.DragEnter += FlowLayoutPanel_DragEnter;
            // 
            // flpFavPlayers
            // 
            flpFavPlayers.AllowDrop = true;
            flpFavPlayers.BackColor = Color.LightGray;
            flpFavPlayers.FlowDirection = FlowDirection.TopDown;
            flpFavPlayers.Location = new Point(12, 72);
            flpFavPlayers.Name = "flpFavPlayers";
            flpFavPlayers.Size = new Size(286, 411);
            flpFavPlayers.TabIndex = 6;
            flpFavPlayers.WrapContents = false;
            flpFavPlayers.DragDrop += FlowLayoutPanel_DragDrop;
            flpFavPlayers.DragEnter += FlowLayoutPanel_DragEnter;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 505);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(303, 41);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save Favourite Players";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(335, 505);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(303, 41);
            btnExit.TabIndex = 8;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // FavPlayersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(648, 558);
            Controls.Add(btnExit);
            Controls.Add(btnSave);
            Controls.Add(flpFavPlayers);
            Controls.Add(flpAllPlayers);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FavPlayersForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FavPlayersForm";
            Shown += FavPlayersForm_Shown;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem markFavouriteToolStripMenuItem;
        private ToolStripMenuItem unmarkFavouriteToolStripMenuItem;
        private Label label2;
        private Label label3;
        private FlowLayoutPanel flpAllPlayers;
        private FlowLayoutPanel flpFavPlayers;
        private Button btnSave;
        private Button btnExit;
    }
}