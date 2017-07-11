using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Core.Entities
{
    public class TopicEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateOfLastMessage { get; set; }

        public string CreatorId { get; set; }
        public virtual UserEntity Creator { get; set; }

        public virtual ICollection<MessageEntity> Messages { get; set; }
    }
}
