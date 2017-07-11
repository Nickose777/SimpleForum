using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models.Message
{
    public class MessageListModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [Display(Name = "Created: ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Modified: ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateLastModified { get; set; }

        [Display(Name = "Author: ")]
        public string SenderLogin { get; set; }
    }
}