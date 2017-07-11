using SimpleForum.Data.Contracts.Repositories;
using SimpleForum.Core;
using SimpleForum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Repositories
{
    class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(ForumDbContext context)
            : base(context) { }
    }
}
