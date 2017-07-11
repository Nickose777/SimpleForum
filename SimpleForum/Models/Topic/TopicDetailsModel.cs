﻿using SimpleForum.Models.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models.Topic
{
    public class TopicDetailsModel
    {
        public int Id { get; set; }

        [Display(Name = "Author: ")]
        public string CreatorLogin { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Created: ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateCreated { get; set; }

        public List<MessageListModel> Messages { get; set; }
    }
}