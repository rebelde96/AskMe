﻿using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
    public class SendMessageViewModel
    {
        public int Id { get; set; }

        public string MessageText { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
