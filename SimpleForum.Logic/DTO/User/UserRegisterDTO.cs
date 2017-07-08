using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.DTO.User
{
    public class UserRegisterDTO : RegisterBaseDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
