﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skoodle.Models
{
    public class Room
    {
        public Room()
        {
            Users = new HashSet<ApplicationUser>();
            Topic = new HashSet<Topic>();
        }

        [Key]
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int MaxPlayers { get; set; }
        public int MaxRounds { get; set; }
        public virtual ICollection<Topic> Topic { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}