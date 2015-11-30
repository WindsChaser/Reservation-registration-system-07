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
			DBState = dbService.startService() ? State.Suspend : State.Close;
			netState = netService.startService() ? State.Suspend : State.Close;
			helperState = helperService.startService() ? State.Suspend : State.Close;
			safetyState = safetyService.startService() ? State.Suspend : State.Close;

			return DBState == State.Running && netState == State.Running && helperState == State.Running && safetyState == State.Running;
		}

		public bool stopService()
		{
			DBState = dbService.stopService() ? State.Suspend : State.Close;
			netState = netService.stopService() ? State.Suspend : State.Close;
			helperState = helperService.stopService() ? State.Suspend : State.Close;
			safetyState = safetyService.stopService() ? State.Suspend : State.Close;

			return DBState == State.Suspend && netState == State.Suspend && helperState == State.Suspend && safetyState == State.Suspend;
		}

		public bool closeService()
		{
			DBState = dbService.closeService() ? State.Suspend : State.Close;
			netState = netService.closeService() ? State.Suspend : State.Close;
			helperState = helperService.closeService() ? State.Suspend : State.Close;
			safetyState = safetyService.closeService() ? State.Suspend : State.Close;

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
