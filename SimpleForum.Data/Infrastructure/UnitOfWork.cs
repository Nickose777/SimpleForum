using SimpleForum.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleForum.Data.Contracts.Repositories;
using SimpleForum.Data.Identity;
using SimpleForum.Data.Repositories;

namespace SimpleForum.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumDbContext context;

        private IAccountManager accounts;
        private IUserRepository users;

        public IAccountManager Accounts
        {
            get { return accounts ?? (accounts = new AccountManager(context)); }
        }

        public IUserRepository Users
        {
            get { return users ?? (users = new UserRepository(context)); }
        }

        public UnitOfWork()
        {
            context = new ForumDbContext();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
