﻿namespace SoftRazborki.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class User
    {
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
