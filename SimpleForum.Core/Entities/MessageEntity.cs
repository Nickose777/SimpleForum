using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Core.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateLastModified { get; set; }

        public string SenderId { get; set; }
        public virtual UserEntity Sender { get; set; }

        public int TopicId { get; set; }
        public virtual TopicEntity Topic { get; set; }
    }
}
