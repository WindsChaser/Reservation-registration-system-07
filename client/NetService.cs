using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace client
{
	class NetService
	{
		#region 字段声明
		public static IPAddress localIP;//本地IPv4地址

		private Queue<String> RequestList_send;//请求发送队列
		public Queue<String> MessageList_receive;//消息接收队列

		public BroadcastClient broadcastClient;//广播客户端
		private UdpClient Client_receive;//消息接收客户端
		private UdpClient Client_send;//消息发送客户端

		private Thread ConnectServer_thread;//连接服务器线程
		private Thread MessageReceive_thread;//消息接收线程
		
		public delegate void NewMessageReceive();//新的服务器消息
		public event NewMessageReceive NewMessage;//新消息到达事件
		public delegate void NewServerConnected();//连接到服务器
		public event NewServerConnected NewServer;//新的服务器

		public bool isConnecting = false;
		#endregion
		public NetService()
		{
			//获取本机地址
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
		/// <summary>
		/// 创建广播接收器
		/// </summary>
		public void CreatBroadcastLinker()
		{
			broadcastClient = new BroadcastClient( ref Client_receive, ref Client_send );//广播客户端
			RequestList_send = new Queue<String>();
			MessageList_receive = new Queue<String>();
			//ConnectServer_thread = new Thread( () =>
			//{
			//	while ( true )
			//	{
			//		broadcastClient.receiveBroadcast();
			//		broadcastClient.ConnectServer();
			//		broadcastClient.sendResponse();
			//		if ( NewServer != null )
			//		{
			//			NewServer();
			//			Thread.Sleep(0);
			//		}
			//	}
			//} );//连接服务器线程
			//ConnectServer_thread.IsBackground = true;
			//ConnectServer_thread.Start();//开始接收服务器广播
		}
		/// <summary>
		/// 连接服务器
		/// </summary>
		public void ConnectServer()
		{
			broadcastClient.receiveBroadcast();
			broadcastClient.ConnectServer();
			broadcastClient.sendResponse();
			if ( NewServer != null )
				NewServer();
		}
		/// <summary>
		/// 客户端开始处理消息队列
		/// </summary>
		public void StartMessageQueue_client()
		{
			//RequestSend_thread = new Thread(() =>
			//{
			//    while (true)
			//    {
			//        if (RequestList_send.Count > 0)
			//        {
			//            String str;
			//            lock (RequestList_send)
			//            {
			//                str = RequestList_send.Dequeue();
			//            }
			//            SendRequest(str);
			//        }
			//    }
			//});
			MessageReceive_thread = new Thread( () =>
			{
				while ( true )
				{
					String str = ReceiveMessage();
					lock ( MessageList_receive )
					{
						MessageList_receive.Enqueue( str );
					}
					if ( NewMessage != null )
						NewMessage();
				}
			} );
			MessageReceive_thread.IsBackground = true;
			//RequestSend_thread.Start();
			MessageReceive_thread.Start();
		}
		/// <summary>
		/// 将请求队列里的请求全部发出
		/// </summary>
		//public void RequestSend_fun()
		//{
		//	lock ( RequestList_send )
		//		while ( RequestList_send.Count > 0 )
		//		{
		//			String str;
		//			str = RequestList_send.Dequeue();
		//			SendRequest( str );
		//		}
		//}
		/// <summary>
		/// 向请求队列中添加请求并强制异步处理
		/// 由外部主动调用
		/// </summary>
		/// <param name="str"></param>
		public void AddRequest( String str )
		{
			lock ( RequestList_send )
			{
				RequestList_send.Enqueue( str );
			}
			ThreadPool.QueueUserWorkItem( new WaitCallback( ( object state ) =>
			{
				//RequestSend_fun();
				lock ( RequestList_send )
				{
					String tmp;
					tmp = RequestList_send.Dequeue();
					SendRequest( tmp );
				}
			} ), null );
		}
		/// <summary>
		/// 向服务器发送请求
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public void SendRequest( String command )
		{
			byte[] buff_send = Encoding.Unicode.GetBytes( command );
			Client_send.Send( buff_send, buff_send.Length );
		}
		/// <summary>
		/// 接收消息
		/// </summary>
		public String ReceiveMessage()
		{
			IPEndPoint remoteIEP = null;
			byte[] buff_receive = Client_receive.Receive( ref remoteIEP );
			return Encoding.Unicode.GetString( buff_receive );
		}
		public class BroadcastClient
        {
            private UdpClient RecieveBroadcastClient;//广播接收服务器
			private IPEndPoint LocalIEP_receive;//本机接收地址
			private IPEndPoint ReceiveBroadcastArea;//广播接收范围

			private UdpClient SendBroadcastClient;//广播发送服务器
			private IPEndPoint localIEP_send;//本机发送地址

            private String Response = "This is client";//客户端验证声明
            private String GameServerBeginRunDeclare = "This is server";//服务器验证声明

            public IPEndPoint BroadcastServerIEP;//解析出的广播服务器IP
            public IPEndPoint ServerIEP;//解析出的服务器IP

            public UdpClient Client_receive;//接收客户端
            private IPEndPoint ClientIEP_receive;
            public UdpClient Client_send;//发送客户端
            private IPEndPoint ClientIEP_send;
            /// <summary>
            /// 构造函数，创建广播接收和发送服务器，以及客户端TCP/IP连接
            /// </summary>
            /// <param name="udp1"></param>
            public BroadcastClient(ref UdpClient udp1, ref UdpClient udp2)
            {
                LocalIEP_receive = new IPEndPoint(localIP, 23333);//选取本机地址，端口为23333
                RecieveBroadcastClient = new UdpClient(LocalIEP_receive);//创建广播接收服务器
                ReceiveBroadcastArea = null;//接收范围为所有IP

                localIEP_send = new IPEndPoint(localIP, GetFirstAvailablePort());//创建广播发送服务器地址
                SendBroadcastClient = new UdpClient(localIEP_send);//创建广播发送服务器

                ClientIEP_receive = new IPEndPoint(localIP, GetFirstAvailablePort());
                Client_receive = udp1 = new UdpClient(ClientIEP_receive);//创建接收客户端

                ClientIEP_send = new IPEndPoint(localIP, GetFirstAvailablePort());
                Client_send = udp2 = new UdpClient(ClientIEP_send);//创建发送客户端
            }
            /// <summary>
            /// 接收服务器地址广播
            /// </summary>
            public void receiveBroadcast()
            {
                while (true)
                {
                    byte[] buff = RecieveBroadcastClient.Receive(ref ReceiveBroadcastArea);//接收服务器广播
                    //Console.WriteLine("broadcast-receive");
                    String message = Encoding.Unicode.GetString(buff);//字节流->字符串
                    String[] chips = message.Split(';');//拆分数据
                    if (chips[0].Equals(GameServerBeginRunDeclare))
                    {
                        BroadcastServerIEP = new IPEndPoint(IPAddress.Parse(chips[2]), Int32.Parse(chips[3]));
                        //解析广播服务器IP地址
                        ServerIEP = new IPEndPoint(IPAddress.Parse(chips[5]), Int32.Parse(chips[6]));
                        //解析服务器IP地址
                        return;
                    }
                }
            }
            /// <summary>
            /// 连接游戏服务器
            /// </summary>
            public void ConnectServer()
            {
                Client_send.Connect(ServerIEP);//连接解析出的服务器地址
                //Console.WriteLine("服务器连接成功");
                byte[] buff = Encoding.Unicode.GetBytes(Response + ";" + ClientIEP_receive.Address + ";" + ClientIEP_receive.Port);
                Client_send.Send(buff, buff.Length);
            }
            /// <summary>
            /// 向广播服务器发送回应
            /// </summary>
            public void sendResponse()
            {
                SendBroadcastClient.Connect(BroadcastServerIEP);
                byte[] buff = Encoding.Unicode.GetBytes(Response + ";My IPEndPoint is;"
                    + LocalIEP_receive.Address + ";" + LocalIEP_receive.Port);//创建发送数据
                SendBroadcastClient.Send(buff, buff.Length);//向服务器发送响应信息和本机地址
            }
        }
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
