﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    public class PersonDetail
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsSelfUser { get; set; }
    }
}