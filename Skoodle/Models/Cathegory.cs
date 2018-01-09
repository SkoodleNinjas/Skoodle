using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skoodle.Models
{
    public class Cathegory
    {
        public Cathegory()
        {
            Topics = new HashSet<Topic>();
        }

        [Key]
        public int CathergoryId { get; set; }

        public string CathegoryName { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}