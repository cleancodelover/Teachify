using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teachify.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
