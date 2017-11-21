﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skoodle.Models
{
    public class Topic
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public byte[] Image { get; set; }

    }
}