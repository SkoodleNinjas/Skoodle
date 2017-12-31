using System;
using System.Collections.Generic;

namespace Skoodle.ViewModels
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }

        public string Name { get; set; }

        public string CurrentTopic { get; set; }

        public List<String> CurrentUserNames { get; set; }
        public string GameId { get; set; }
    }
}