using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models.Topic
{
    public class TopicCreateModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} must contain max {1} characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} must contain max {1} characters")]
        public string Description { get; set; }
    }
}