using SimpleForum.Data.Contracts;
using SimpleForum.Core;
using SimpleForum.Core.Entities;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.DTO.User;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
