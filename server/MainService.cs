using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			dbService = new DBService();
			netService = new NetService();
			helperService = new HelperService();
			safetyService = new SafetyService();

			DBState = dbService.initService() ? State.Suspend : State.Close;
			netState = netService.initService() ? State.Suspend : State.Close;
			helperState = helperService.initService() ? State.Suspend : State.Close;
			safetyState = safetyService.initService() ? State.Suspend : State.Close;

			return DBState == State.Suspend && netState == State.Suspend && helperState == State.Suspend && safetyState == State.Suspend;
		}

		public bool startService()
		{
			DBState = dbService.startService() ? State.Running : State.Suspend;
			netState = netService.startService() ? State.Running : State.Suspend;
			helperState = helperService.startService() ? State.Running : State.Suspend;
			safetyState = safetyService.startService() ? State.Running : State.Suspend;

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
