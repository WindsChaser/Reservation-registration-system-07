using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
	}
}
