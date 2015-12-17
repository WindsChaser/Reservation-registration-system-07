using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace client.Resources
{
	/// <summary>
	/// Regist.xaml 的交互逻辑
	/// </summary>
	public partial class Regist : Window
	{
		public delegate void RegistSubmit(string username,string password,string name,string IDNumber,string phoneNumber,string email);
		public event RegistSubmit newSubmit;

		public Regist()
		{
			InitializeComponent();
		}

		private void closeWindow( object sender, MouseButtonEventArgs e )
		{
			this.Close();
		}

		private void submit( object sender, MouseButtonEventArgs e )
		{
			if ( !isUserName( username.Text ) )
			{
				MessageBox.Show( "用户名不合法！请填写纯数字和字母组合并且长度大于四位。" );
				return;
			}
			else if ( !isKeyWord( password.Password ) )
			{
				MessageBox.Show( "密码不合法！请填写纯数字和字母组合并且长度大于八位。" );
				return;
			}
			else if ( !password.Password.Equals( password2.Password ) )
			{
				MessageBox.Show( "密码不合法！确认密码应当和密码一样。" );
				return;
			}
			else if ( isName( name.Text ) )
			{
				MessageBox.Show( "姓名不合法！请输入中文字符。" );
				return;
			}
			else if ( isIDNumber( IDNumber.Text ) )
			{
				MessageBox.Show( "身份证号不合法！" );
				return;
			}
			else if ( isPhone( phoneNumber.Text ) )
			{
				MessageBox.Show( "手机号码不合法！" );
				return;
			}
			else if ( isEmail( email.Text ) )
			{
				MessageBox.Show( "邮箱不合法！" );
				return;
			}
			else if ( newSubmit != null )
			{
				newSubmit( username.Text, password.Password, name.Text, IDNumber.Text, phoneNumber.Text, email.Text );
				string ret = MainWindow.showWaitForm(this.PointToScreen(new Point(this.ActualWidth/2,this.ActualHeight/2)));//这是一个阻塞方法
			}
		}

		private bool isEmail( string str )
		{
			return new Regex( @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+" ).IsMatch( str );
		}

		private bool isPhone( string str )
		{
			return new Regex( @"^13\\d{9}$" ).IsMatch( str );
        }

		private bool isIDNumber( string str )
		{
			return new Regex( @"/^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$/" ).IsMatch( str );
		}

		private bool isName( string str )
		{
			return new Regex( @"^[\u4e00-\u9fa5]+$" ).IsMatch( str );
		}

		private bool isUserName( string str )
		{
			return new Regex( @"^[A-Za-z0-9]+$" ).IsMatch( str ) && str.Length >= 4;
		}

		private bool isKeyWord( string str )
		{
			return new Regex( @"^[A-Za-z0-9]+$" ).IsMatch( str ) && str.Length >= 8;
		}
	}
}
