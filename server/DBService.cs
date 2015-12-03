using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Threading;
using System.Windows.Forms;

namespace server
{
	public class DBService : Service
	{
		private SqlConnection sqlConnection;//基础数据库连接
		private int TimeOut;//连接超时时间
		private int MaxPoolSize;//最大连接池数量
		//public delegate void CommandCompleted(int id,Object ret);//指令完成委托,包含指令的id和返回值
		//public event CommandCompleted commandCompleted;//指令完成时引发的事件,在主服务里监听该事件即可在引发事件时做出响应

		public DBService()
		{
			state = State.Close;
		}

		public bool initService()
		{
			bool tag = connectSql( sqlConnection, getSource() );
			state = tag ? State.Suspend : State.Close;
			return tag;
		}

		public bool startService()
		{
			//等待主服务调用
			//限制连接池里的连接数量（在这个方法里实现）
			//对每个数据库调用请求新建一个连接并异步提交请求
			bool tag = false;
			state = tag ? State.Running : State.Suspend;
			return tag;
		}

		public bool stopServicce()
		{
			//标记数据库服务停止，从而拒绝响应
			bool tag = false;
			state = tag ? State.Suspend : State.Running;
			return tag;
		}

		public bool closeService()
		{
			//标记数据库服务关闭，关闭所有连接
			sqlConnection.Dispose();//关闭连接并清理资源

			bool tag = false;
			state = tag ? State.Close : State.Suspend;
			return tag;
		}

		public bool connectSql( SqlConnection sqlconnection, String source )
		{
			sqlConnection.ConnectionString = source;
			try
			{
				sqlConnection.Open();
			}
			catch ( SqlException ex )
			{
				Console.WriteLine( OperationTips.errOpenDB );//数据库连接失败
				return false;
			}
			catch ( InvalidOperationException ex )
			{
				Console.WriteLine( OperationTips.reOpenDB );//重复打开数据库异常
			}
			return true;
		}

		private String getSource()
		{
			return new StringBuilder().ToString();//返回连接字符串
		}

		private SqlConnection getNewConnection()
		{
			return new SqlConnection();//返回一个新的连接
		}

		public Object addNewRequest( String command )
		{
			//如果已经停止或者关闭服务，拒绝响应
			if ( state != State.Running )
				return null;

			//拆分command的内容，根据命令类型执行不同的操作
			return null;
		}

		private void Query()
		{
			//参数列表、返回值和具体操作由你们想
			SqlConnection connection = getNewConnection();//直接创建一个新的连接
			connectSql( connection, getSource() );//如果连接池已满的话会等待连接池空缺时返回或者抛出超时异常
			//balabala……
			connection.Close();//关闭连接
		}

		private void Insert()
		{
			//……
		}

		//其它命令……………
		//…………
		//……
		//…
    }
}
