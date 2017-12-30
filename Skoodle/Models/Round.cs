using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skoodle.Models
{
    public class Round
    {
        public Round()
        {
            Drawings = new HashSet<UserDrawing>();
        }

        [Key]
        public int RoundId { get; set; }

        public int RoundNum { get; set; }

        public virtual Game Game { get; set; }

        public virtual ICollection<UserDrawing> Drawings { get; set; }
    }
}