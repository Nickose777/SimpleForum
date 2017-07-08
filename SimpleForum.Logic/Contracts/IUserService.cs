using SimpleForum.Logic.DTO.User;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Contracts
{
    public interface IUserService : IDisposable
    {
        ServiceMessage Register(UserRegisterDTO user);
    }
}
