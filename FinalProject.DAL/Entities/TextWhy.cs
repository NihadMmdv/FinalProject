using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
	public class TextWhy:BaseEntity
	{
		public string Title { get; set; }
		public string Text { get; set; }
		public string Icon { get; set; }
		public int SettingId { get; set; }
		public Setting Setting { get; set; }
	}
}
