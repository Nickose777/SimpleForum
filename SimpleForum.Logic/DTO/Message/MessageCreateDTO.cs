using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.DTO.Message
{
    public class MessageCreateDTO
    {
        public string Text { get; set; }

        public string SenderLogin { get; set; }

        public int TopicId { get; set; }
    }
}
