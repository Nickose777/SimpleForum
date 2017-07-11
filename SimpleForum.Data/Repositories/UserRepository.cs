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

        public override UserEntity Get(int id)
        {
            throw new InvalidOperationException("Id of user cannot be integer");
        }

        public bool Exists(string login)
        {
            return context.Users.SingleOrDefault(user => user.UserName == login) != null;
        }

        public UserEntity Get(string id)
        {
            return context.ForumUsers.SingleOrDefault(user => user.ApplicationUser.UserName == id);
        }
    }
}
