using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models.Message
{
    public class MessageEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Max length of message is {1}")]
        public string Text { get; set; }
    }
}