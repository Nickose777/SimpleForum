using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models.Message
{
    public class MessageCreateModel
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Max length of message is {1}")]
        public string Text { get; set; }

        [Required]
        public int TopicId { get; set; }
    }
}