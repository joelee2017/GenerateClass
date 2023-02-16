using System;
using System.ComponentModel;

namespace Member
{
	public class Member
	{
		/// <summary>
		/// 姓名
		/// </summary>
		[Description("Name")]
		public string Name { get; set; }

		/// <summary>
		/// 年齡
		/// </summary>
		[Description("Age")]
		public int? Age { get; set; }

	}
}
