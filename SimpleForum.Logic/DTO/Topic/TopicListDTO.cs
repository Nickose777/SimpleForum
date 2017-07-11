using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.DTO.Topic
{
    public class TopicListDTO
    {
        public int Id { get; set; }

        public string CreatorLogin { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateOfLastMessage { get; set; }

        public int MessagesCount { get; set; }
    }
}
