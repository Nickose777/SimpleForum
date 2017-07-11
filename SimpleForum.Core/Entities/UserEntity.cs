using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Core.Entities
{
    public class UserEntity
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<TopicEntity> Topics { get; set; }

        public virtual ICollection<MessageEntity> Messages { get; set; }
    }
}
