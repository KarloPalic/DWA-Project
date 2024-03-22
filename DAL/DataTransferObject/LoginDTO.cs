using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataTransferObject
{
    public class LoginDTO
    {
        public User Username { get; set; }
        public string Password { get; set; }
    }
}
