using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Responses
{
    public class ApiResponse
    {
        public object items { get; set; }
        public int StatusCode { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public object itemView { get; set; }

    }
}
