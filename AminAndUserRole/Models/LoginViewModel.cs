﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AminAndUserRole.Models
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Remember { get; set; }

    }
}