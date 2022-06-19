using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorDeveloperProject.Models
{
    public class ResponseBase
    {
        public IEnumerable<UserBase> UserList { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
}
