using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skoodle.Models
{
    public class Game
    {
        public Game()
        {
            this.Rounds = new HashSet<Round>();
        }

        [Key]
        public int GameId { get; set; }

        public DateTime PlayedTime { get; set; }

        public virtual Room Room { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }

    }
}