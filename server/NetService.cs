using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server
{
	public class NetService:Service
	{
		#region 字段声明
		public static IPAddress localIP;//本地IPv4地址
		/// <summary>
		/// 仅服务器端使用
		/// </summary>
		private BroadcastServer broadcastServer;//广播服务器
		private IPEndPoint GameServer_send;//广播服务器发送端地址
		private IPEndPoint GameServer_receive;//广播服务器接收端地址

		public Dictionary<int, IPEndPoint> ClientList;//客户端列表

		private UdpClient Server_receive;//接收主服务器
		private UdpClient Server_send;//发送主服务器

		public Thread ServerBroadcast_thread;//服务器地址广播线程
		private Thread RequestReceive_thread;//请求接收线程

		public Queue<String> RequestList_receive;//请求接收队列
		private Queue<String> CommandList_send;//指令发送队列

		public delegate void NewRequestRecieve();//新的请求
		public event NewRequestRecieve NewRequest;//新请求到达事件
												  //public delegate void NewClientConnection();//新的客户端连接
												  //public event NewClientConnection NewClient;//新的客户端事件
		#endregion
		public NetService()
		{
			
		}
		/// <summary>
		/// 初始化网络服务，获取本机地址，开始广播服务器地址
		/// </summary>
		/// <returns></returns>
		public bool initService()
		{
			//获取本机地址
			#region
			IPAddress[] ips = Dns.GetHostAddresses( Dns.GetHostName() );
			//获取本地可用IPv4地址
			foreach ( IPAddress ipa in ips )
			{
				if ( ipa.AddressFamily == AddressFamily.InterNetwork )
				{
					localIP = ipa;
					break;
				}
			}
			#endregion
			//广播服务器地址
			#region
			try
			{
				ClientList = new Dictionary<int, IPEndPoint>();
				RequestList_receive = new Queue<String>();//请求接收队列
				CommandList_send = new Queue<String>();//指令发送队列
				broadcastServer = new BroadcastServer( ref Server_receive, ref Server_send );//广播服务器
				GameServer_send = broadcastServer.LocalIEP_send;//广播服务器发送地址
				GameServer_receive = broadcastServer.LocalIEP_receive;//广播服务器接收地址
				ServerBroadcast_thread = new Thread( () =>
				{
					while ( true )
					{
						broadcastServer.sendStartBroadcast();
						Thread.Sleep( 1000 );
					}
				} );//服务器发送广播
				ServerBroadcast_thread.IsBackground = true;
				ServerBroadcast_thread.Start();//服务器开始广播
				state = State.Suspend;
			}
			catch ( Exception ex )
			{

				Console.WriteLine( ex.Message );
				return false;
			}
			#endregion
			return localIP != null;
		}
		/// <summary>
		/// 启动网络服务，开始接收消息队列
		/// </summary>
		/// <returns></returns>
		public bool startService()
		{
			try
			{
				StartMessageQueue_server();
				state = State.Running;
				return true;
			}
			catch ( Exception ex )
			{
				Console.WriteLine( ex.Message );
				return false;
			}
		}
		/// <summary>
		/// 暂停服务，标记为挂起状态
		/// </summary>
		/// <returns></returns>
		public bool stopService()
		{
			state = State.Suspend;
			return true;
		}
		/// <summary>
		/// 关闭服务
		/// </summary>
		/// <returns></returns>
		public bool closeService()
		{
			ServerBroadcast_thread.Abort();
			RequestReceive_thread.Abort();
			state = State.Close;
			return true;
		}
		/// <summary>
		/// 服务器开始处理消息队列
		/// </summary>
		public void StartMessageQueue_server()
		{
			RequestReceive_thread = new Thread( () =>
			{
				while ( true )
				{
					String str = ReceiveRequest();
					lock ( RequestList_receive )
					{
						RequestList_receive.Enqueue( str );//操作消息队列时应当加锁
					}
					if ( NewRequest != null )
						NewRequest();
				}
			} );
			RequestReceive_thread.IsBackground = true;
			RequestReceive_thread.Start();
		}
		/// <summary>
		/// 将发送队列里的命令全部发出
		/// 考虑到需要针对不同的客户端发送不同的消息，暂时废弃该方法
		/// </summary>
		//public void CommandSend_fun()
		//{
		//	lock ( CommandList_send )
		//		while ( CommandList_send.Count > 0 )
		//		{
		//			String str;
		//			str = CommandList_send.Dequeue();
		//			SendGameCommand( str );
		//		}
		//}
		/// <summary>
		/// 向消息队列中添加新的命令并强制异步执行
		/// </summary>
		/// <param name="str"></param>
		public void AddCommand(int id, String str )
		{
			lock ( CommandList_send )
			{
				//在这里对str进行处理，添加上id头
				CommandList_send.Enqueue( str );
			}
			ThreadPool.QueueUserWorkItem( new WaitCallback( ( object state ) =>
			{
				String tmp;
				lock ( CommandList_send )
				{
					//在这里对取出来的tmp进行处理，读出id头
					tmp = CommandList_send.Dequeue();
				}
				SendCommand( ClientList[id], tmp );//通过用户id和IP端口字典读出发送的用户地址
			} ), null );
		}
		public void SendCommand(IPEndPoint IEP,String command )
		{
			byte[] buff_send = Encoding.Unicode.GetBytes( command );
			Server_send.Send( buff_send, buff_send.Length, IEP );
		}
		/// <summary>
		/// 接收指定客户端的请求
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public String ReceiveRequest()
		{
			IPEndPoint remoteIEP = null;
			byte[] buff_receive = Server_receive.Receive( ref remoteIEP );
			return Encoding.Unicode.GetString( buff_receive );
		}
		/// <summary>
		/// 从请求队列中取出一条消息
		/// </summary>
		public String GetMessage()
		{
			String tmp;
			try
			{
				lock ( RequestList_receive )
				{
					tmp = RequestList_receive.Dequeue();
				}
				return tmp;
			}
			catch ( Exception ex )
			{
				Console.WriteLine( OperationTips.errRequestList );
				Console.WriteLine( ex.Message );
				return null;
			}
		}
		#region 广播服务器类，用于提供服务器地址
		public class BroadcastServer
		{
			private UdpClient SendBroadcastServer;//广播发送服务器
			private IPEndPoint DeclareBroadcastAreaIEP;//广播地址段
			private IPEndPoint LocalIEP_send;//本机发送IP节点

			private UdpClient RecieveBroadcastServer;//广播接收服务器
			private IPEndPoint ReceiveBroadcastAreaIEP;//接收地址段
			private IPEndPoint LocalIEP_receive;//本机接收IP节点

			public IPEndPoint ServerIEP_receive;//主服务器接收节点
			public IPEndPoint ServerIEP_send;//主服务器发送节点

			public UdpClient Server_receive;
			public UdpClient Server_send;

			private String ServerBeginRunDeclare = "This is server";//服务器验证声明
			private String Response = "This is client";//客户端验证声明
													   /// <summary>
													   /// 构造函数，创建广播发送和接收服务器，以及主服务器监听器
													   /// </summary>
													   /// <param name="tcpl"></param>
			public BroadcastServer( ref UdpClient udp1, ref UdpClient udp2 )
			{
				LocalIEP_send = new IPEndPoint( localIP, GetFirstAvailablePort() );//选取广播发送服务器地址
				SendBroadcastServer = new UdpClient( LocalIEP_send );//创建开始广播服务器
				LocalIEP_receive = new IPEndPoint( localIP, GetFirstAvailablePort() );//选取广播接收接收服务器地址
				RecieveBroadcastServer = new UdpClient( LocalIEP_receive );//创建响应接收服务器


				DeclareBroadcastAreaIEP = new IPEndPoint( IPAddress.Broadcast, 23333 );
				//广播发送范围为默认广播地址，端口为23333
				ReceiveBroadcastAreaIEP = new IPEndPoint( localIP, GetFirstAvailablePort() );//创建接收地址段


				ServerIEP_receive = new IPEndPoint( localIP, GetFirstAvailablePort() );//选取主服务器接收地址和端口
				Server_receive = udp1 = new UdpClient( ServerIEP_receive );
				ServerIEP_send = new IPEndPoint( localIP, GetFirstAvailablePort() );//选取主服务器发送地址和端口
				Server_send = udp2 = new UdpClient( ServerIEP_send );
			}
			/// <summary>
			/// 广播服务器地址
			/// </summary>
			public void sendStartBroadcast()
			{
				byte[] buff = Encoding.Unicode.GetBytes( ServerBeginRunDeclare
					+ ";BroadcastIPEndPoint;" + LocalIEP_receive.Address + ";" + LocalIEP_receive.Port
					+ ";MainServerIPEndPoint;" + ServerIEP_receive.Address
					+ ";" + ServerIEP_receive.Port );
				//创建广播内容，包括广播服务器接收地址和主服务器节点地址
				SendBroadcastServer.Send( buff, buff.Length, DeclareBroadcastAreaIEP );//向指定范围发送广播
			}
			/// <summary>
			/// 广播服务器接收回应
			/// 下面两个方法已废弃。应当由客户端主动向主服务器发起登录请求再将其登记到在线列表。
			/// </summary>
			/// <returns></returns>
			//public IPEndPoint receiveResponse()
			//{
			//	while ( true )
			//	{
			//		byte[] buff = RecieveBroadcastServer.Receive( ref ReceiveBroadcastAreaIEP );//接收响应数据
			//		String message = Encoding.Unicode.GetString( buff );//字节流->字符串
			//		String[] chips = message.Split( ';' );//拆分数据
			//		if ( chips[0].Equals( Response ) )
			//		{
			//			return new IPEndPoint( IPAddress.Parse( chips[2] ), Int32.Parse( chips[3] ) );//返回读出的客户机地址
			//		}
			//	}
			//}

			//public UdpClient getClient()
			//{
			//	while ( true )
			//	{
			//		byte[] buff = Server_receive.Receive( ref ReceiveBroadcastAreaIEP );
			//		String message = Encoding.Unicode.GetString( buff );//字节流->字符串
			//		String[] chips = message.Split( ';' );//拆分数据
			//		Console.WriteLine( message );
			//		if ( chips[0].Equals( Response ) )
			//		{
			//			UdpClient udpclient = new UdpClient( new IPEndPoint( NetService.localIP, NetService.GetFirstAvailablePort() ) );
			//			udpclient.Connect( new IPEndPoint( IPAddress.Parse( chips[1] ), Int32.Parse( chips[2] ) ) );
			//			return udpclient;//返回客户端连接
			//		}
			//	}
			//}
		}
		#endregion
		#region port helper
		public static int GetFirstAvailablePort()
		{
			int MAX_PORT = 8000; //系统tcp/udp端口数最大是65535            
			int BEGIN_PORT = 5000;//从这个端口开始检测
			for ( int i = BEGIN_PORT; i < MAX_PORT; i++ )
			{
				if ( PortIsAvailable( i ) )
					return i;
			}
			return -1;
		}
		/// <summary>
		/// 返回被占用的端口号
		/// </summary>
		/// <returns></returns>
		public static List<int> PortIsUsed()
		{
			//获取本地计算机的网络连接和通信统计数据的信息
			IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			//返回本地计算机上的所有Tcp监听程序
			IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
			//返回本地计算机上的所有UDP监听程序
			IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();
			//返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
			TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
			List<int> allPorts = new List<int>();
			foreach ( IPEndPoint ep in ipsTCP )
				allPorts.Add( ep.Port );
			foreach ( IPEndPoint ep in ipsUDP )
				allPorts.Add( ep.Port );
			foreach ( TcpConnectionInformation conn in tcpConnInfoArray )
				allPorts.Add( conn.LocalEndPoint.Port );
			return allPorts;
		}
		/// <summary>
		/// 检查指定端口是否可用
		/// </summary>
		/// <param name="port"></param>
		/// <returns></returns>
		public static bool PortIsAvailable( int port )
		{
			bool isAvailable = true;
			List<int> portUsed = PortIsUsed();
			foreach ( int p in portUsed )
			{
				if ( p == port )
				{
					isAvailable = false;
					break;
				}
			}
			return isAvailable;
		}
		#endregion
	}
}
