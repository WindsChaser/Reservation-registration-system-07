using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			////
			//DBService test = new DBService();
			//test.initService();
			//test.startService();
			//string ret=test.addNewRequest(null) as string;




			timer.Tick += (object sender,EventArgs e) =>
			{
				float CPUutilization = performanceCounter1.NextValue();
                toolStripStatusLabel2.Text = "CPU: " + CPUutilization.ToString( "F2" ) + " %";
				toolStripProgressBar.Value = (int)( CPUutilization * 3 );

				CPUutilization = performanceCounter4.NextValue();
				label8.Text="CPU占用率： "+ CPUutilization.ToString( "F2" ) + " %";

				float MemoryUtilization = performanceCounter2.NextValue();
				toolStripStatusLabel3.Text = "已提交内存: " + MemoryUtilization.ToString( "F2" ) + " %";

				MemoryUtilization = performanceCounter5.NextValue();
				label9.Text = "内存占用： " + (MemoryUtilization/1024/1024).ToString("F2")+" MB";

				float IOUtilization = performanceCounter6.NextValue();
				label11.Text = "磁盘I/O： " + IOUtilization.ToString( "F2" );


				//以下代码仅能运行在非托管状态，调试时请勿使用
				int ThreadCount = (int)performanceCounter3.NextValue();
				toolStripStatusLabel4.Text = "已运行线程： " + ThreadCount;


			};
			timer.Start();
		}

		private void button1_Click( object sender, EventArgs e )
		{

		}
	}
}
