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
    class TopicRepository : RepositoryBase<TopicEntity>, ITopicRepository
    {
        public TopicRepository(ForumDbContext context)
            : base(context) { }
    }
}
