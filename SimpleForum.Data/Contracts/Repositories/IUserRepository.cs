﻿using SimpleForum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Contracts.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        bool Exists(string login);

        UserEntity Get(string login);
    }
}
