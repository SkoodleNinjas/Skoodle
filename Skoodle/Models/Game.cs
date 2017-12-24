using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Skoodle.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        public virtual ICollection<UserDrawing> UserDrawing { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

    }
}