using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models.Topic
{
    public class TopicListModel
    {
        public int Id { get; set; }

        [Display(Name = "Author")]
        public string CreatorLogin { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Topic created")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Last message")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateOfLastMessage { get; set; }

        [Display(Name = "Messages")]
        public int MessagesCount { get; set; }
    }
}