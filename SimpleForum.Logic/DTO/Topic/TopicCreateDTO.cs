using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.DTO.Topic
{
    public class TopicCreateDTO
    {
        public string CreatorLogin { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
