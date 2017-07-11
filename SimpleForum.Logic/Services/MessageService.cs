using SimpleForum.Data.Contracts;
using SimpleForum.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
