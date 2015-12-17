using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
	class Entities
	{
		public class User
		{
			public String password;
			public String userName;
			public int id;
		}

		public class ReUser : User
		{
			public String realName;
			public long phoneNumber;
			public String IDCard;
			public int credit;
			public String sex;

			public List<reservationList> reservations = new List<reservationList>();
		}

		public class SytManager : User
		{
		}

		public class Hospital
		{
			public int id;
			public String name;
			public String address;
			public String remark;
			public int rank;

			public List<Department> departments = new List<Department>();
		}

		public class Department
		{
			public int id;
			public String name;
			public String _class;
			public String remark;

			public List<Doctor> doctors = new List<Doctor>();
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
	}
}
