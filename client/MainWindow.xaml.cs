using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using client.Resources;

namespace client
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		Rect NormalRect;
		Rect workAera = SystemParameters.WorkArea;
		Page page1, page2, page3;

		DispatcherTimer netServiceConfirm;

		bool isLoggedOn = false;
		bool isConnected = false;

		Entities.ReUser user;
		List<Entities.Hospital> hospitals;
		NetService netservice;

		public MainWindow()
		{
			InitializeComponent();
			#region 启动动画
			this.BeginAnimation( HeightProperty, new DoubleAnimation( 600, new Duration( TimeSpan.FromMilliseconds( 1200 ) ) ,FillBehavior.Stop)
			{
				EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut, Exponent = -5 }
			} );
			#endregion
			#region 定时器
			var DateTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds( 1 ) };
			DateTimer.Tick += ( object sender, EventArgs e ) =>
			{
				DateTime datetime = DateTime.Now;
				dateLabel.Content = datetime.ToString( "D" );
				timeLabel.Content = datetime.ToString( "HH:mm:ss" );
			};
			DateTimer.Start();
			#endregion

			Entities.ReUser user = new Entities.ReUser();
			List<Entities.Hospital> hospitals = new List<Entities.Hospital>();
			NetService netservice = new NetService();
			netservice.CreatBroadcastLinker();
			netservice.NewServer += () =>
			{
				isConnected = true;
			};
			netServiceConfirm = new DispatcherTimer() { Interval = TimeSpan.FromSeconds( 5 ) };
			netServiceConfirm.Tick += ( object sender, EventArgs e ) =>
			{
				if ( !isConnected && netservice.isConnecting == false )
				{
					ThreadPool.QueueUserWorkItem( ( object args ) =>
					 {
						 netservice.isConnecting = true;
						 netservice.ConnectServer();
						 netservice.isConnecting = false;
					 }, null );
				}
				else
				{

				}
			};
		}

		private void Netservice_NewServer()
		{
			throw new NotImplementedException();
		}
		#region 窗口放缩处理
		private void BackToDefaultBackground( object sender, MouseEventArgs e )
		{
			( (Canvas)sender ).Background = new SolidColorBrush(Colors.Transparent);
		}

		private void ChangeBackground( object sender, MouseEventArgs e )
		{
			( (Canvas)sender ).Background = new SolidColorBrush( Color.FromArgb( 200, 190, 210, 230 ) );
		}

		private void CloseWindow( object sender, MouseButtonEventArgs e )
		{
			Application.Current.Shutdown();
		}

		private void WindowDrag( object sender, MouseButtonEventArgs e )
		{
			if ( e.ClickCount == 2 )
				if ( this.Width != workAera.Width || this.Height != workAera.Height )
					ResizeToMax( null, null );
				else
					ResizeToNormal( null, null );
			else
				base.DragMove();
		}

		private void ResizeToMax( object sender, MouseButtonEventArgs e )
		{
			NormalRect= new Rect( this.Left, this.Top, this.Width, this.Height );
			StateChange(null,null);
			
			this.Left = 0;
			this.Top = 0;
			this.Width = workAera.Width;
			this.Height = workAera.Height;
		}

		private void ResizeToMin( object sender, MouseButtonEventArgs e )
		{
			NormalRect = new Rect( this.Left, this.Top, this.Width, this.Height );
			this.WindowState = WindowState.Minimized;
		}

		private void ResizeToNormal( object sender, MouseButtonEventArgs e )
		{
			StateChange( null, null );
			this.Left = NormalRect.Left;
			this.Top = NormalRect.Top;
			this.Width = NormalRect.Width;
			this.Height = NormalRect.Height;
		}

		private void StateChange( object sender, EventArgs e )
		{
			if ( this.BorderThickness.Right > 0 )
			{
				this.BorderThickness = new System.Windows.Thickness( 0 );
			}
			else
			{
				this.BorderThickness = new System.Windows.Thickness( 10 );
			}
		}

		private void SizeChange( object sender, SizeChangedEventArgs e )
		{
			if ( this.ActualHeight > workAera.Height || this.ActualWidth > workAera.Width )
			{
				this.WindowState = System.Windows.WindowState.Normal;
				ResizeToMax( null, null );
			}
		}
		#endregion
		private void ChangeTextColor( object sender, MouseEventArgs e )
		{
			( (Label)sender ).Foreground = new SolidColorBrush(Color.FromArgb(255,20,135,165));
		}

		private void ReturnTextColor( object sender, MouseEventArgs e )
		{
			( (Label)sender ).Foreground = new SolidColorBrush( Color.FromArgb( 204, 255, 255, 255 ) );
		}

		private void LoadPage( object sender, MouseButtonEventArgs e )
		{
			DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration( TimeSpan.FromMilliseconds( 400 ) ), FillBehavior.Stop );
			animation.Completed += (object se,EventArgs ea) =>
			{
				string tmp = ( (Label)sender ).Name;
				if ( tmp.Equals( "p1_t" ) )
				{
					if ( page1 == null )
						page1 = new Page_UserInfo();
					frame.Navigate( page1 );
				}
				else if ( tmp.Equals( "p2_t" ) )
				{
					if ( page2 == null )
						page2 = new Page_CommonInfo();
					frame.Navigate( page2 );
				}
				else if ( tmp.Equals( "p3_t" ) )
				{
					if ( page3 == null )
						page3 = new Page_OrderInfo();
					frame.Navigate( page3 );
				}

				frame.BeginAnimation( Frame.OpacityProperty, new DoubleAnimation(0, 1, new Duration( TimeSpan.FromMilliseconds( 400 ) ), FillBehavior.Stop ));
			};
            frame.BeginAnimation(Frame.OpacityProperty, animation);
		}
		/// <summary>
		/// 显示等待窗口，旋转的一个圈
		/// </summary>
		public static string showWaitForm(Point p)
		{
			//在这里显示窗口，启动动画，注册请求成功事件（在消息处理方法中定义），收到事件时主动关闭窗口，并返回一个值
			//对超时进行处理
			LoadingForm form = new LoadingForm();
			form.Left = p.X  - form.Width / 2;
			form.Top = p.Y  - form.Height / 2;
			form.ShowDialog();
			return null;
		}
	}
}
