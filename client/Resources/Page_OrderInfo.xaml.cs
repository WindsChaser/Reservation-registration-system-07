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

namespace client.Resources
{
	/// <summary>
	/// Page_OrderInfo.xaml 的交互逻辑
	/// </summary>
	public partial class Page_OrderInfo : Page
	{
		MyTextBlock current;
		public Page_OrderInfo()
		{
			InitializeComponent();
			t1.state.Style = (Style)FindResource( "state_payed" );
			t2.state.Style = (Style)FindResource( "state_finished" );
			t3.state.Style = (Style)FindResource( "state_payed" );
			t4.state.Style = (Style)FindResource( "state_unpayed" );
		}

		private void chooseItem( object sender, MouseButtonEventArgs e )
		{
			if (current!=null)
			current.Background = new SolidColorBrush( Color.FromArgb( 2, 114, 174, 209 ) );
			( (MyTextBlock)sender ).Background = new SolidColorBrush(Color.FromArgb(204,23,116,180));
			current = (MyTextBlock)sender;
        }

		private void onItem( object sender, MouseEventArgs e )
		{
			if ( current == null || current != sender )
				( (MyTextBlock)sender ).Background = new SolidColorBrush( Color.FromArgb( 204, 114, 174, 209 ) );
		}

		private void leItem( object sender, MouseEventArgs e )
		{
			if ( current == null || current != sender )
				( (MyTextBlock)sender ).Background = new SolidColorBrush( Color.FromArgb( 02, 114, 174, 209 ) );
		}
	}
}
