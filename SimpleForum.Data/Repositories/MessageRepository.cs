using SimpleForum.Core;
using SimpleForum.Core.Entities;
using SimpleForum.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Repositories
{
    class MessageRepository : RepositoryBase<MessageEntity>, IMessageRepository
    {
        public MessageRepository(ForumDbContext context)
            : base(context) { }
    }
}
