using SimpleForum.Logic.DTO.Message;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Contracts
{
    public interface IMessageService : IDisposable
    {
        ServiceMessage Create(MessageCreateDTO messageDTO);

        ServiceMessage Edit(MessageEditDTO messageDTO);

        DataServiceMessage<MessageEditDTO> Get(int id);
    }
}
