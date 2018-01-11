using System;
using Skoodle.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skoodle.ViewModels
{
    public class CreateRoomViewModel
    {
        public string RoomName { get; set; }

        public int MaxPlayers { get; set; }
        
        public int MaxRounds { get; set; }

        public List<string> Cathegories { get; set; }
    }
}