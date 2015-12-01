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
		public static IPAddress localIP;//本地IPv4地址
		//public int PlayerCount;//玩家数目限制
		//private int MessageLength = 256;
		/// <summary>
		/// 仅服务器端使用
		/// </summary>
		private BroadcastServer broadcastServer;//广播服务器
		private IPEndPoint GameServer_send;//广播服务器发送端地址
		private IPEndPoint GameServer_receive;//广播服务器接收端地址
		private List<IPEndPoint> GameClientsBroadcastList;//在线玩家广播地址表
		public List<UdpClient> Clients;//在线玩家连接表
		private UdpClient Server_receive;//游戏接收服务器
		private UdpClient Server_send;//游戏发送服务器
		public Thread CreatGame_thread;//游戏创建线程
		public Thread WaitClient_thread;//等待客户端线程
		private Thread RequestReceive_thread;//游戏请求接收线程
		public Queue<String> RequestList_receive;//游戏请求接收队列
		private Queue<String> CommandList_send;//游戏指令发送队列
		public delegate void NewRequestRecieve();//新的游戏请求
		public event NewRequestRecieve NewRequest;//新请求到达事件
		public delegate void NewClientConnection();//新的客户端连接
		public event NewClientConnection NewClient;//新的客户端

		public NetService()
		{
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
		}

		#region 服务器类
		public class BroadcastServer
		{
			private UdpClient GameServerBroadcast;//游戏开始广播服务器
			private IPEndPoint DeclareBroadcastAreaIEP;//广播地址段
			public IPEndPoint LocalIEP_send;//本机发送IP节点

			private UdpClient GameServerBroadcastRecieve;//游戏广播响应接收服务器
			private IPEndPoint ReceiveBroadcastAreaIEP;//接收地址段
			public IPEndPoint LocalIEP_receive;//本机接收IP节点

			public IPEndPoint GameServerIEP_receive;//游戏服务器接收节点
			public IPEndPoint GameServerIEP_send;//游戏服务器发送节点

			public UdpClient GameServer_receive;
			public UdpClient GameServer_send;

			private String GameServerBeginRunDeclare = "This is the game \"Bomb-man\" server";//游戏服务器验证声明
			private String Response = "This is a game client";//客户端验证声明
															  /// <summary>
															  /// 构造函数，创建广播发送和接收服务器，以及游戏服务器监听器
															  /// </summary>
															  /// <param name="tcpl"></param>
			public BroadcastServer( ref UdpClient udp1, ref UdpClient udp2 )
			{
				LocalIEP_send = new IPEndPoint( NetService.localIP, NetService.GetFirstAvailablePort() );//选取开始广播服务器地址
				GameServerBroadcast = new UdpClient( LocalIEP_send );//创建开始广播服务器
				LocalIEP_receive = new IPEndPoint( NetService.localIP, NetService.GetFirstAvailablePort() );//选取响应接收服务器地址
				GameServerBroadcastRecieve = new UdpClient( LocalIEP_receive );//创建响应接收服务器
				DeclareBroadcastAreaIEP = new IPEndPoint( IPAddress.Broadcast, 23333 );
				//广播发送范围为默认广播地址，端口为23333
				ReceiveBroadcastAreaIEP = new IPEndPoint( NetService.localIP, NetService.GetFirstAvailablePort() );//创建接收地址段

				GameServerIEP_receive = new IPEndPoint( NetService.localIP, NetService.GetFirstAvailablePort() );//选取游戏服务器地址和端口
				GameServer_receive = udp1 = new UdpClient( GameServerIEP_receive );
				GameServerIEP_send = new IPEndPoint( NetService.localIP, NetService.GetFirstAvailablePort() );//选取游戏服务器地址和端口
				GameServer_send = udp2 = new UdpClient( GameServerIEP_send );
				//Console.WriteLine(LocalIEP_send.ToString());
				//Console.WriteLine(LocalIEP_receive.ToString());
				//Console.WriteLine(GameServerListener.ToString());
			}
			/// <summary>
			/// 发送游戏开始广播
			/// </summary>
			public void sendStartBroadcast()
			{
				byte[] buff = Encoding.Unicode.GetBytes( GameServerBeginRunDeclare
					+ ";My BroadcastIPEndPoint is;" + LocalIEP_receive.Address + ";" + LocalIEP_receive.Port
					+ ";My GameServerIPEndPoint is;" + GameServerIEP_receive.Address
					+ ";" + GameServerIEP_receive.Port );
				//创建广播内容，包括广播服务器接收地址和游戏服务器节点地址
				GameServerBroadcast.Send( buff, buff.Length, DeclareBroadcastAreaIEP );//向指定范围发送广播
			}
			/// <summary>
			/// 广播服务器接收回应
			/// </summary>
			/// <returns></returns>
			public IPEndPoint receiveResponse()
			{
				while ( true )
				{
					byte[] buff = GameServerBroadcastRecieve.Receive( ref ReceiveBroadcastAreaIEP );//接收响应数据
					String message = Encoding.Unicode.GetString( buff );//字节流->字符串
					String[] chips = message.Split( ';' );//拆分数据
					Console.WriteLine( "BroadResponse: " + message );
					if ( chips[0].Equals( Response ) )
					{
						return new IPEndPoint( IPAddress.Parse( chips[2] ), Int32.Parse( chips[3] ) );//返回读出的客户机地址
					}
				}
			}

			public UdpClient getClient()
			{
				while ( true )
				{
					byte[] buff = GameServer_receive.Receive( ref ReceiveBroadcastAreaIEP );
					String message = Encoding.Unicode.GetString( buff );//字节流->字符串
					String[] chips = message.Split( ';' );//拆分数据
					Console.WriteLine( message );
					if ( chips[0].Equals( Response ) )
					{
						UdpClient udpclient = new UdpClient( new IPEndPoint( NetService.localIP, NetService.GetFirstAvailablePort() ) );
						udpclient.Connect( new IPEndPoint( IPAddress.Parse( chips[1] ), Int32.Parse( chips[2] ) ) );
						return udpclient;//返回客户端连接
					}
				}
			}
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
