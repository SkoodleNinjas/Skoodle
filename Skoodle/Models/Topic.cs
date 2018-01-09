using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TODO:
/// Make new Model for the Categories!
/// </summary>
namespace Skoodle.Models
{
    public class Topic
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual Cathegory Category { get; set; }
        public byte[] Image { get; set; }

    }
}