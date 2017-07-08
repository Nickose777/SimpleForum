using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Infrastructure
{
    public class ServiceMessage
    {
        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }
    }
}
