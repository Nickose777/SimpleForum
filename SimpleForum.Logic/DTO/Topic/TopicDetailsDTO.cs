using SimpleForum.Logic.DTO.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.DTO.Topic
{
    public class TopicDetailsDTO
    {
        public int Id { get; set; }

        public string CreatorLogin { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public List<MessageListDTO> Messages { get; set; }
    }
}
