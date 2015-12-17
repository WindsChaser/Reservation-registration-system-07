namespace server
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.performanceCounter2 = new System.Diagnostics.PerformanceCounter();
			this.performanceCounter3 = new System.Diagnostics.PerformanceCounter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.performanceCounter4 = new System.Diagnostics.PerformanceCounter();
			this.performanceCounter5 = new System.Diagnostics.PerformanceCounter();
			this.performanceCounter6 = new System.Diagnostics.PerformanceCounter();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter3)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter6)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
			this.statusStrip1.Location = new System.Drawing.Point(0, 535);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(784, 26);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(72, 21);
			this.toolStripStatusLabel1.Text = "当前状态：";
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripProgressBar.Maximum = 300;
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.Size = new System.Drawing.Size(300, 20);
			this.toolStripProgressBar.Step = 1;
			this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.toolStripProgressBar.Value = 50;
			// 
			// toolStripStatusLabel5
			// 
			this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
			this.toolStripStatusLabel5.Size = new System.Drawing.Size(243, 21);
			this.toolStripStatusLabel5.Spring = true;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(48, 21);
			this.toolStripStatusLabel2.Text = "CPU：";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(48, 21);
			this.toolStripStatusLabel3.Text = "内存：";
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(56, 21);
			this.toolStripStatusLabel4.Text = "线程数：";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(759, 97);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.AutoSize = true;
			this.button2.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button2.ForeColor = System.Drawing.Color.Red;
			this.button2.Location = new System.Drawing.Point(653, 30);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(95, 44);
			this.button2.TabIndex = 3;
			this.button2.Text = "关闭";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.AutoSize = true;
			this.button1.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.button1.Location = new System.Drawing.Point(550, 30);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(95, 44);
			this.button1.TabIndex = 2;
			this.button1.Text = "启动";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(239, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 34);
			this.label2.TabIndex = 1;
			this.label2.Text = "关闭";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(7, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(225, 34);
			this.label1.TabIndex = 0;
			this.label1.Text = "服务器状态：";
			// 
			// performanceCounter1
			// 
			this.performanceCounter1.CategoryName = "Processor";
			this.performanceCounter1.CounterName = "% Processor Time";
			this.performanceCounter1.InstanceName = "_Total";
			// 
			// timer
			// 
			this.timer.Interval = 500;
			// 
			// performanceCounter2
			// 
			this.performanceCounter2.CategoryName = "Memory";
			this.performanceCounter2.CounterName = "% Committed Bytes In Use";
			// 
			// performanceCounter3
			// 
			this.performanceCounter3.CategoryName = "Process";
			this.performanceCounter3.CounterName = "Thread Count";
			this.performanceCounter3.InstanceName = "server";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.AutoSize = true;
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label);
			this.panel1.Location = new System.Drawing.Point(13, 117);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(203, 415);
			this.panel1.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 132);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 12);
			this.label7.TabIndex = 5;
			this.label7.Text = "已接收数据：";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 107);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(77, 12);
			this.label6.TabIndex = 4;
			this.label6.Text = "已发送数据：";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(4, 82);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 3;
			this.label5.Text = "运行时间：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 57);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(101, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "外部服务已就绪：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(101, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "网络服务已开启：";
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.Location = new System.Drawing.Point(3, 8);
			this.label.Margin = new System.Windows.Forms.Padding(3);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(89, 12);
			this.label.TabIndex = 0;
			this.label.Text = "数据库已连接：";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.listView1);
			this.groupBox2.Location = new System.Drawing.Point(231, 110);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(541, 420);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label11.Location = new System.Drawing.Point(289, 375);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(141, 27);
			this.label11.TabIndex = 4;
			this.label11.Text = "磁盘I/O：";
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label10.Location = new System.Drawing.Point(289, 330);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(348, 27);
			this.label10.TabIndex = 3;
			this.label10.Text = "网络吞吐量：（手动统计）";
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label9.Location = new System.Drawing.Point(10, 375);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(152, 27);
			this.label9.TabIndex = 2;
			this.label9.Text = "内存占用：";
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label8.Location = new System.Drawing.Point(10, 330);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(169, 27);
			this.label8.TabIndex = 1;
			this.label8.Text = "CPU占用率：";
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.BackColor = System.Drawing.SystemColors.Control;
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.listView1.ForeColor = System.Drawing.SystemColors.InfoText;
			this.listView1.Location = new System.Drawing.Point(13, 20);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(515, 291);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Uid";
			this.columnHeader1.Width = 50;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "用户名";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "在线时间";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "当前请求密度";
			this.columnHeader4.Width = 100;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "数据吞吐";
			// 
			// performanceCounter4
			// 
			this.performanceCounter4.CategoryName = "Process";
			this.performanceCounter4.CounterName = "% Processor Time";
			this.performanceCounter4.InstanceName = "server";
			// 
			// performanceCounter5
			// 
			this.performanceCounter5.CategoryName = "Process";
			this.performanceCounter5.CounterName = "Working Set";
			this.performanceCounter5.InstanceName = "server";
			// 
			// performanceCounter6
			// 
			this.performanceCounter6.CategoryName = "Process";
			this.performanceCounter6.CounterName = "IO Data Bytes/sec";
			this.performanceCounter6.InstanceName = "server";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.Text = "Server Management";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter3)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter6)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
		private System.Diagnostics.PerformanceCounter performanceCounter1;
		private System.Windows.Forms.Timer timer;
		private System.Diagnostics.PerformanceCounter performanceCounter2;
		private System.Diagnostics.PerformanceCounter performanceCounter3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Diagnostics.PerformanceCounter performanceCounter4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Diagnostics.PerformanceCounter performanceCounter5;
		private System.Diagnostics.PerformanceCounter performanceCounter6;
	}
}

