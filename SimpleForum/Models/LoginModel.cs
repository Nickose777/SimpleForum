﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}