using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skoodle.ViewModels
{
    public class NewRoomViewModel
    {
        public string RoomName { get; set; }

        public int MaxPlayers { get; set; }

        public int MaxRounds { get; set; }

        public string CathegoryName { get; set; }
    }
}