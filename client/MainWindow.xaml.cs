using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace client
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		Rect NormalRect;
		Rect workAera = SystemParameters.WorkArea;
		public MainWindow()
		{
			InitializeComponent();
			this.BeginAnimation( HeightProperty, new DoubleAnimation( 600, new Duration( TimeSpan.FromMilliseconds( 1200 ) ) ,FillBehavior.Stop)
			{
				EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut, Exponent = -5 }
			} );
			var DateTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds( 1 ) };
			DateTimer.Tick += ( object sender, EventArgs e ) =>
			{
				DateTime datetime = DateTime.Now;
				dateLabel.Content = datetime.ToString( "D" );
				timeLabel.Content = datetime.ToString( "HH:mm:ss" );
			};
			DateTimer.Start();
		}

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

		}
	}
}
