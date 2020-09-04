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
			this.GenerateButton = new System.Windows.Forms.Button();
			this.CanvasPanel = new System.Windows.Forms.Panel();
			this.RenderView = new System.Windows.Forms.PictureBox();
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
			// GenerateButton
			// 
			this.GenerateButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.GenerateButton.Location = new System.Drawing.Point(0, 0);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(300, 30);
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button GenerateButton;
		private System.Windows.Forms.Panel CanvasPanel;
		public System.Windows.Forms.PictureBox RenderView;
	}
}