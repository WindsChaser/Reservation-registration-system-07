using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
	#region static define
	public enum State
	{
		Close,
		Suspend,
		Running
	}

	public abstract class Service
	{
		public bool initService()
		{
			return false;
		}

		public bool startService()
		{
			return false;
		}

		public bool stopService()
		{
			return false;
		}

		public bool closeService()
		{
			return false;
		}
	}
	#endregion
	public class MainService : Service
	{
		public State DBState, netState, helperState, safetyState;
		public DBService dbService;
		public NetService netService;
		public HelperService helperService;
		public SafetyService safetyService;

		public MainService()
		{
			DBState = netState = helperState = safetyState = State.Close;
		}

		public bool initService()
		{
			//实例化各服务
			dbService = new DBService();
			netService = new NetService();
			helperService = new HelperService();
			safetyService = new SafetyService();
			//初始化各服务
			DBState = dbService.initService() ? State.Suspend : State.Close;
			netState = netService.initService() ? State.Suspend : State.Close;
			helperState = helperService.initService() ? State.Suspend : State.Close;
			safetyState = safetyService.initService() ? State.Suspend : State.Close;

			return DBState == State.Suspend && netState == State.Suspend && helperState == State.Suspend && safetyState == State.Suspend;
		}

		public bool startService()
		{
			//打开各服务
			DBState = dbService.startService() ? State.Running : State.Suspend;
			netState = netService.startService() ? State.Running : State.Suspend;
			helperState = helperService.startService() ? State.Running : State.Suspend;
			safetyState = safetyService.startService() ? State.Running : State.Suspend;
			//绑定事件
			netService.NewRequest += () =>
			{
				String request = netService.GetMessage();
				if ( request == null )
					return;
				ThreadPool.QueueUserWorkItem( ( Object state ) =>
				 {
					 //对取出来的客户端请求进行处理,如注册请求，检查用户名和密码，分配id，调用数据库服务写入数据库
					 //如查询请求，拆分字段，调用数据库服务查询并返回结果
					 //可以先区分请求类型，然后调用不同的方法（每种请求类型一个方法）

					 //完成请求，开始处理返回结果

					 //根据请求id号确定应发向的用户
					 //调用netService.AddCommand()

					 //id号的使用方法：创建一个全局变量（比如字典类型的），存放读出的客户端请求，并进行编号，存为id-request键值对。
					 //应当注意保存下请求编号对应的用户id（可以写在请求内容里什么的）。

					 //在网络服务类里有一个public Dictionary<int, IPEndPoint> ClientList;//客户端列表
					 //这个应当用于当服务器接收到用户的登录请求后将其ip地址写入列表
					 //但是考虑到用户有可能掉线，应当增加一个守护线程，每隔一段时间（比如10s）轮询该列表里的用户，向用户发出确认在线请求，如果一段时间里没有收到回应，即判定为掉线，删除该无效用户键值对

					 //此外，还要考虑到对于未注册用户的访问，应当如何确定返回地址。建议，通过协议实现，如果用户未登陆/注册，则在请求里标记自己的身份，主服务检查身份后将其地址写入请求内容，等完成请求准备返回结果时，主服务给网络服务的addcommand方法传入一个特殊的用户id比如0，然后网络服务检查到这个id，直接读取请求里的地址而不是查询客户端列表

					 //关于性能：首先是查询优化，这点ado.net应该内置了，无需关心，包括缓存什么的；然后是连接池，同样内置；并发读写，内置；
				 }, null );
			};

			return DBState == State.Running && netState == State.Running && helperState == State.Running && safetyState == State.Running;
		}

		public bool stopService()
		{
			DBState = dbService.stopService() ? State.Suspend : State.Running;
			netState = netService.stopService() ? State.Suspend : State.Running;
			helperState = helperService.stopService() ? State.Suspend : State.Running;
			safetyState = safetyService.stopService() ? State.Suspend : State.Running;

			return DBState == State.Suspend && netState == State.Suspend && helperState == State.Suspend && safetyState == State.Suspend;
		}

		public bool closeService()
		{
			DBState = dbService.closeService() ? State.Close : State.Suspend;
			netState = netService.closeService() ? State.Close : State.Suspend;
			helperState = helperService.closeService() ? State.Close : State.Suspend;
			safetyState = safetyService.closeService() ? State.Close : State.Suspend;

			return DBState == State.Close && netState == State.Close && helperState == State.Close && safetyState == State.Close;
		}

		public String[] getState()
		{
			return new String[] {
				DBState.ToString(),netState.ToString(),helperState.ToString(),safetyState.ToString()
			};
		}
	}
}
