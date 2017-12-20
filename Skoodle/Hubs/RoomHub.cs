using Microsoft.AspNet.SignalR;
using Skoodle.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skoodle.WebSockets
{    
    public class RoomHub: Hub
    {
        public async Task JoinRoom(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            Clients.Group(roomName).addChatMessage(Context.User.Identity.Name + " joined.", "");
            Clients.OthersInGroup(roomName).addUser(Context.User.Identity.Name);
        }

        public async Task SendMessage(string roomName, string message)
        {
            Clients.Group(roomName).addChatMessage(Context.User.Identity.Name, message);
        }

        public Task LeaveRoom(string roomName)
        {
            Clients.Group(roomName).userLeft(Context.User.Identity.Name);
            return Groups.Remove(Context.ConnectionId, roomName);
        }
    }
}