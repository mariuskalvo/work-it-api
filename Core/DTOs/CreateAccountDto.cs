﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class CreateAccountDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
