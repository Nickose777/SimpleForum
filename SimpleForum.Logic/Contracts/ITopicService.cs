using SimpleForum.Logic.DTO.Topic;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Contracts
{
    public interface ITopicService : IDisposable
    {
        ServiceMessage Create(TopicCreateDTO topicDTO);

        DataServiceMessage<IEnumerable<TopicListDTO>> GetAll();
    }
}
