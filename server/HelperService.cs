using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace server
{
	public class HelperService:Service
	{

		/// <summary>
		/// 提交账户名和密码尝试支付
		/// </summary>
		/// <param name="type"></param>
		/// <param name="name"></param>
		/// <param name="password"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		static bool tryPay( String type, String name, String password, double amount )
		{
			return true;
		}
		/// <summary>
		/// 查询账单
		/// </summary>
		/// <param name="type"></param>
		/// <param name="name"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		static Object getChecks( String type, String name, String password )
		{
			return null;
		}
		/// <summary>
		/// 确认身份是否合法
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		static bool isIdentityLegal( String ID, String name )
		{
			return true;
		}
		/// <summary>
		/// 记录操作日志
		/// </summary>
		/// <param name="Summary"></param>
		/// <param name="date"></param>
		static void Log( String Summary, DateTime date )
		{

		}
		//还有什么需要的都可以在这里写，反正也不一定要实现……
	}
}
