namespace HistoryGenerator
{
	partial class Form1
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
			this.GenerateButton = new System.Windows.Forms.Button();
			this.CanvasPanel = new System.Windows.Forms.Panel();
			this.Canvas = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.HeightMapWidthUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.HeightMapHeightUpDown = new System.Windows.Forms.NumericUpDown();
			this.HeightMapScaleUpDown = new System.Windows.Forms.NumericUpDown();
			this.HeightMapOctavesUpDown = new System.Windows.Forms.NumericUpDown();
			this.HeightMapPersitanceUpDown = new System.Windows.Forms.NumericUpDown();
			this.HeightMapLacunarityUpDown = new System.Windows.Forms.NumericUpDown();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.CanvasPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
			this.panel1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapWidthUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapHeightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapScaleUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapOctavesUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapPersitanceUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapLacunarityUpDown)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GenerateButton
			// 
			this.GenerateButton.Location = new System.Drawing.Point(3, 566);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(275, 27);
			this.GenerateButton.TabIndex = 2;
			this.GenerateButton.Text = "Generate";
			this.GenerateButton.UseVisualStyleBackColor = true;
			this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// CanvasPanel
			// 
			this.CanvasPanel.AutoScroll = true;
			this.CanvasPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.CanvasPanel.Controls.Add(this.Canvas);
			this.CanvasPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.CanvasPanel.Location = new System.Drawing.Point(564, 5);
			this.CanvasPanel.Name = "CanvasPanel";
			this.CanvasPanel.Size = new System.Drawing.Size(499, 596);
			this.CanvasPanel.TabIndex = 3;
			this.CanvasPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
			this.CanvasPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
			this.CanvasPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
			this.CanvasPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseWheel);
			// 
			// Canvas
			// 
			this.Canvas.Enabled = false;
			this.Canvas.Location = new System.Drawing.Point(-2, -2);
			this.Canvas.Name = "Canvas";
			this.Canvas.Size = new System.Drawing.Size(350, 330);
			this.Canvas.TabIndex = 0;
			this.Canvas.TabStop = false;
			this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Controls.Add(this.GenerateButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(5, 5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(283, 596);
			this.panel1.TabIndex = 4;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.White;
			this.tabPage2.Controls.Add(this.tableLayoutPanel1);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(267, 504);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "WorldGeneration";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.HeightMapLacunarityUpDown, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.HeightMapPersitanceUpDown, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.HeightMapOctavesUpDown, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.HeightMapScaleUpDown, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.HeightMapHeightUpDown, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.HeightMapWidthUpDown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 9;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(261, 530);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.label6.Location = new System.Drawing.Point(3, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(69, 15);
			this.label6.TabIndex = 3;
			this.label6.Text = "HeightMap";
			// 
			// HeightMapWidthUpDown
			// 
			this.HeightMapWidthUpDown.Location = new System.Drawing.Point(133, 23);
			this.HeightMapWidthUpDown.Name = "HeightMapWidthUpDown";
			this.HeightMapWidthUpDown.Size = new System.Drawing.Size(125, 23);
			this.HeightMapWidthUpDown.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 87);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Scale";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 117);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "Octaves";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 57);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Height";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Width";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 147);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "Persistance";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 177);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(62, 15);
			this.label7.TabIndex = 0;
			this.label7.Text = "Lacunarity";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// HeightMapHeightUpDown
			// 
			this.HeightMapHeightUpDown.Location = new System.Drawing.Point(133, 53);
			this.HeightMapHeightUpDown.Name = "HeightMapHeightUpDown";
			this.HeightMapHeightUpDown.Size = new System.Drawing.Size(125, 23);
			this.HeightMapHeightUpDown.TabIndex = 2;
			// 
			// HeightMapScaleUpDown
			// 
			this.HeightMapScaleUpDown.DecimalPlaces = 1;
			this.HeightMapScaleUpDown.Location = new System.Drawing.Point(133, 83);
			this.HeightMapScaleUpDown.Name = "HeightMapScaleUpDown";
			this.HeightMapScaleUpDown.Size = new System.Drawing.Size(125, 23);
			this.HeightMapScaleUpDown.TabIndex = 2;
			// 
			// HeightMapOctavesUpDown
			// 
			this.HeightMapOctavesUpDown.Location = new System.Drawing.Point(133, 113);
			this.HeightMapOctavesUpDown.Name = "HeightMapOctavesUpDown";
			this.HeightMapOctavesUpDown.Size = new System.Drawing.Size(125, 23);
			this.HeightMapOctavesUpDown.TabIndex = 2;
			// 
			// HeightMapPersitanceUpDown
			// 
			this.HeightMapPersitanceUpDown.DecimalPlaces = 2;
			this.HeightMapPersitanceUpDown.Location = new System.Drawing.Point(133, 143);
			this.HeightMapPersitanceUpDown.Name = "HeightMapPersitanceUpDown";
			this.HeightMapPersitanceUpDown.Size = new System.Drawing.Size(125, 23);
			this.HeightMapPersitanceUpDown.TabIndex = 2;
			// 
			// HeightMapLacunarityUpDown
			// 
			this.HeightMapLacunarityUpDown.DecimalPlaces = 2;
			this.HeightMapLacunarityUpDown.Location = new System.Drawing.Point(133, 173);
			this.HeightMapLacunarityUpDown.Name = "HeightMapLacunarityUpDown";
			this.HeightMapLacunarityUpDown.Size = new System.Drawing.Size(125, 23);
			this.HeightMapLacunarityUpDown.TabIndex = 2;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(3, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(275, 532);
			this.tabControl1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1068, 606);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.CanvasPanel);
			this.Name = "Form1";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Text = "Form1";
			this.CanvasPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
			this.panel1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapWidthUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapHeightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapScaleUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapOctavesUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapPersitanceUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightMapLacunarityUpDown)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button GenerateButton;
		private System.Windows.Forms.Panel CanvasPanel;
		private System.Windows.Forms.PictureBox Canvas;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.NumericUpDown HeightMapLacunarityUpDown;
		private System.Windows.Forms.NumericUpDown HeightMapPersitanceUpDown;
		private System.Windows.Forms.NumericUpDown HeightMapOctavesUpDown;
		private System.Windows.Forms.NumericUpDown HeightMapScaleUpDown;
		private System.Windows.Forms.NumericUpDown HeightMapHeightUpDown;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown HeightMapWidthUpDown;
		private System.Windows.Forms.Label label6;
	}
}