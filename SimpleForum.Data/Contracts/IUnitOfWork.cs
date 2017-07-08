using SimpleForum.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountManager Accounts { get; }

        IUserRepository Users { get; }

        void Commit();
    }
}
