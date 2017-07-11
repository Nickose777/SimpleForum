using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Infrastructure
{
    public class DataServiceMessage<TData> : ServiceMessage where TData : class
    {
        public TData Data { get; set; }
    }
}
