using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
	public class AboutText:BaseEntity
	{
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public string Text { get; set; }
	}
}
