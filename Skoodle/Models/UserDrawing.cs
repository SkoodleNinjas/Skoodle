using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skoodle.Models
{
    public class UserDrawing
    {
        [Key]
        public int UserDrawingId { get; set; }

        public string FileName { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}