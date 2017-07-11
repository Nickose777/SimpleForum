using SimpleForum.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleForum.Data.Contracts.Repositories;
using SimpleForum.Data.Repositories;
using SimpleForum.Core;

namespace SimpleForum.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository users;
        private ITopicRepository topics;
        private IMessageRepository messages;

        public ForumDbContext Context { get; private set; }

        public IUserRepository Users
        {
            get { return users ?? (users = new UserRepository(Context)); }
        }

        public ITopicRepository Topics
        {
            get { return topics ?? (topics = new TopicRepository(Context)); }
        }

        public IMessageRepository Messages
        {
            get { return messages ?? (messages = new MessageRepository(Context)); }
        }

        public UnitOfWork(ForumDbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
