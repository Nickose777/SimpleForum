using SimpleForum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Contracts.Repositories
{
    public interface IMessageRepository : IRepository<MessageEntity>
    {
    }
}
