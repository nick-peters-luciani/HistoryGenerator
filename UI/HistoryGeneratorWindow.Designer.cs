namespace HistoryGenerator
{
	partial class HistoryGeneratorWindow
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.RenderButton = new System.Windows.Forms.Button();
			this.GenerateButton = new System.Windows.Forms.Button();
			this.CanvasPanel = new System.Windows.Forms.Panel();
			this.RenderView = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.CanvasPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RenderView)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 27);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.AutoScroll = true;
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel1MinSize = 150;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.CanvasPanel);
			this.splitContainer1.Panel2MinSize = 150;
			this.splitContainer1.Size = new System.Drawing.Size(1168, 921);
			this.splitContainer1.SplitterDistance = 300;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.Text = "splitContainer1";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
			this.splitContainer2.Panel1MinSize = 200;
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.RenderButton);
			this.splitContainer2.Panel2.Controls.Add(this.GenerateButton);
			this.splitContainer2.Panel2MinSize = 50;
			this.splitContainer2.Size = new System.Drawing.Size(300, 921);
			this.splitContainer2.SplitterDistance = 600;
			this.splitContainer2.TabIndex = 0;
			this.splitContainer2.Text = "splitContainer2";
			// 
			// tabControl1
			// 
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(300, 600);
			this.tabControl1.TabIndex = 0;
			// 
			// RenderButton
			// 
			this.RenderButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.RenderButton.Location = new System.Drawing.Point(0, 40);
			this.RenderButton.Name = "RenderButton";
			this.RenderButton.Size = new System.Drawing.Size(300, 40);
			this.RenderButton.TabIndex = 1;
			this.RenderButton.Text = "Render";
			this.RenderButton.UseVisualStyleBackColor = true;
			this.RenderButton.Click += new System.EventHandler(this.RenderButton_Click);
			// 
			// GenerateButton
			// 
			this.GenerateButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.GenerateButton.Location = new System.Drawing.Point(0, 0);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(300, 40);
			this.GenerateButton.TabIndex = 0;
			this.GenerateButton.Text = "Generate";
			this.GenerateButton.UseVisualStyleBackColor = true;
			this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// CanvasPanel
			// 
			this.CanvasPanel.AutoScroll = true;
			this.CanvasPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.CanvasPanel.Controls.Add(this.RenderView);
			this.CanvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CanvasPanel.Location = new System.Drawing.Point(0, 0);
			this.CanvasPanel.Name = "CanvasPanel";
			this.CanvasPanel.Size = new System.Drawing.Size(864, 921);
			this.CanvasPanel.TabIndex = 0;
			// 
			// RenderView
			// 
			this.RenderView.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.RenderView.Location = new System.Drawing.Point(1, 1);
			this.RenderView.Name = "RenderView";
			this.RenderView.Size = new System.Drawing.Size(500, 500);
			this.RenderView.TabIndex = 0;
			this.RenderView.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1174, 851);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1174, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemLoad,
            this.MenuItemSave});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
			this.toolStripMenuItem1.Text = "File";
			// 
			// MenuItemLoad
			// 
			this.MenuItemLoad.Name = "MenuItemLoad";
			this.MenuItemLoad.Size = new System.Drawing.Size(109, 22);
			this.MenuItemLoad.Text = "Load...";
			// 
			// MenuItemSave
			// 
			this.MenuItemSave.Name = "MenuItemSave";
			this.MenuItemSave.Size = new System.Drawing.Size(109, 22);
			this.MenuItemSave.Text = "Save...";
			// 
			// HistoryGeneratorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 861);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "HistoryGeneratorWindow";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Text = "HistoryGeneratorWindow";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.CanvasPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.RenderView)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button GenerateButton;
		private System.Windows.Forms.Panel CanvasPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		public System.Windows.Forms.PictureBox RenderView;
		public System.Windows.Forms.ToolStripMenuItem MenuItemLoad;
		public System.Windows.Forms.ToolStripMenuItem MenuItemSave;
		private System.Windows.Forms.Button RenderButton;
	}
}