using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
	public enum Access
	{
		unknown,
		unregistered,
		registered,
		manager,
		system
	}

	public class User
	{
		public Access access
		{
			get; set;
		}
	}

	public class ReUser : User
	{
		public String password;
		public String userName;
		public int id;
		public String realName;
		public long phoneNumber;
		public String IDCard;
		public int credit;
	}

	public class SytManager : User
	{
		public String password;
		public String userName;
		public int id;
	}

	public class Hospital
	{
		public int id;
		public String name;
		public String address;
		public String remark;
		public int rank;
	}

	public class Department
	{
		public int id;
		public String name;
		public String _class;
		public String remark;
	}

	public class Doctor
	{
		public int id;
		public String name;
		public String title;
		public Hospital hospital;
		public Department department;
		public String remark;
	}

	public class reservationList
	{
		public int id;
		public Hospital hospital;
		public Department department;
		public Doctor doctor;
		public DateTime time;
		public double fee;
	}

	public class OperationTips
	{
		public static String reOpenDB = "系统基础数据库已连接！请勿重复连接";
		public static String errOpenDB = "系统基础数据库连接失败！";
	}
}
