using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Skoodle.BusinessLogic;
using System.Threading;


namespace Skoodle.Hubs
{
    public class GameHub : Hub
    {

        GameLogic gameLogic = new GameLogic();
        UserLogic userLogic = new UserLogic();

        public void StartRound(int roomId, int gameId)
        {
            int i = 0;
            while(i < 4)
            {
                Clients.Group(roomId.ToString()).roundStart();
                gameLogic.SetGameActive(gameId);
                Thread.Sleep(65 * 1000);
                Clients.Group(roomId.ToString()).roundEnd();
                gameLogic.SetGameInactive(gameId);

                Thread.Sleep(1000);

                Clients.Group(roomId.ToString()).votingStart();
                Thread.Sleep(10 * 1000);
                Clients.Group(roomId.ToString()).votingEnd();

                Thread.Sleep(1000);
                Clients.Group(roomId.ToString()).UpdateStatuses();
                i++;
            }

            Clients.Group(roomId.ToString()).gameFinished();
        }

        public async Task JoinGame(int roomId, int gameId)
        {
            await Groups.Add(Context.ConnectionId, roomId.ToString());

            if (gameLogic.IsAbleToStartRound(roomId))
            {
                Thread t = new Thread(() => StartRound(roomId, gameId));
                t.Start();
            }
        }

        public Task LeaveGame(int roomId)
        {
            return Groups.Remove(Context.ConnectionId, roomId.ToString());
        }
    }
}