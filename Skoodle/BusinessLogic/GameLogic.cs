using Skoodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skoodle.BusinessLogic
{
    public class GameLogic
    {
        private RoomLogic roomLogic = new RoomLogic();

        private ApplicationDbContext db;

        public GameLogic()
        {
            db = new ApplicationDbContext();
        }

        public UserDrawing CreateUserDrawing(string userId, string drawingFileName)
        {
            var user = db.Users.First(us => us.Id == userId);
            var userDrawing = new UserDrawing
            {
                Votes = 0,
                FileName = drawingFileName,
                User = user
            };

            db.UserDrawings.Add(userDrawing);
            db.SaveChanges();

            return userDrawing;
        }

        public void UpdateOrCreateRound(int gameId, int roundNum, string userId, string drawingName)
        {
            var game = db.Games.FirstOrDefault(x => x.GameId == gameId);
            var userDrawing = CreateUserDrawing(userId, drawingName);

            if (!db.Rounds.Any(rnd => rnd.Game.GameId == gameId && rnd.RoundNum == roundNum))
            {
                var newRound = new Round
                {
                    Game = game,
                    RoundNum = roundNum
                };
                db.Rounds.Add(newRound);
                game.Rounds.Add(newRound);
                db.SaveChanges();
            }

            var round = db.Rounds.First(rnd => rnd.Game.GameId == gameId && rnd.RoundNum == roundNum);
            round.Drawings.Add(userDrawing);
            db.SaveChanges();
        }

        public List<Tuple<string, int>> GetDrawingsForRound(int gameId, int roundNum, string loggedUserId)
        {
            var result = new List<Tuple<string, int>>();
            var round = db.Rounds.First(rnd => rnd.Game.GameId == gameId && rnd.RoundNum == roundNum);

            foreach (UserDrawing ud in round.Drawings)
            {
                if (ud.User.Id == loggedUserId)
                {
                    continue;
                }
                var filename = ud.FileName;
                byte[] imageBytes = System.IO.File.ReadAllBytes(filename);

                var drawingAndId = new Tuple<string, int>(
                    Convert.ToBase64String(imageBytes), ud.UserDrawingId);

                result.Add(drawingAndId);
            }

            return result;
        }

        public Game CreateGame(int roomId)
        {
            Room room = db.Rooms.Find(roomId);
            if (!db.Games.Any(gm => gm.IsGameAlive == true && gm.Room.RoomId == roomId))
            {
                Game newGame = new Game
                {
                    PlayedTime = DateTime.Now,
                    Room = room,
                    IsGameAlive = true
                };
                db.Games.Add(newGame);
                db.SaveChanges();
                return newGame;
            }
            Game game = db.Games.First((gm => gm.IsGameAlive == true && gm.Room.RoomId == roomId));
            return game;
        }

        public void AddVoteForImage(int drawingId)
        {
            var userDrawing = db.UserDrawings.First(dw => dw.UserDrawingId == drawingId);
            userDrawing.Votes += 1;
            db.SaveChanges();
        }

        public List<Tuple<int, string>> GetRoundScores(int gameId, int roundNum)
        {
            var result = new List<Tuple<int, string>>();
            var round = db.Rounds.First(rnd => rnd.Game.GameId == gameId && rnd.RoundNum == roundNum);

            foreach (UserDrawing ud in round.Drawings)
            {
                var scoreAndUser = new Tuple<int, string>(ud.Votes, ud.User.UserName);

                result.Add(scoreAndUser);
            }
            return result;
        }

        public bool IsAbleToStartRound(int roomId)
        {
            var room = db.Rooms.First(rm => rm.RoomId == roomId);
            return room.Users.Count() >= 2;
        }

        public bool IsGameActive(int gameId)
        {
            var game = db.Games.First(gm => gm.GameId == gameId);
            return game.IsGameAlive;
        }

        public void SetGameInactive(int gameId)
        {
            var game = db.Games.First(gm => gm.GameId == gameId);
            game.IsGameAlive = false;
            db.SaveChanges();
        }

        public void SetGameActive(int gameId)
        {
            var game = db.Games.First(gm => gm.GameId == gameId);
            game.IsGameAlive = true;
            db.SaveChanges();
        }

        public bool IsGameFinished(int gameId)
        {
            // var game = db.Games.FirstOrDefault(x => x.GameId == gameId);
            var rounds = db.Rounds.Where(x => x.Game.GameId == gameId).ToList();
            return rounds.Count >= roomLogic.GetRoundsForGame(gameId);
        }

        public KeyValuePair<ApplicationUser, int> FinishGame(int gameId)
        {
            var game = db.Games.First(gm => gm.GameId == gameId);

            Dictionary<ApplicationUser, int> results = new Dictionary<ApplicationUser, int>();

            foreach(Round rnd in game.Rounds) {
                foreach(UserDrawing ud in rnd.Drawings)
                {
                    if(!results.ContainsKey(ud.User))
                    {
                        results.Add(ud.User, 0);
                    }
                    results[ud.User] += ud.Votes;
                }
            }

            ApplicationUser winner = null;
            int maxPoints = 0;

            foreach(KeyValuePair<ApplicationUser, int> kv in results)
            {
                if(kv.Value > maxPoints)
                {
                    maxPoints = kv.Value;
                    winner = kv.Key;
                }
            }

            game.WinnerUser = winner;

            db.SaveChanges();

            return new KeyValuePair<ApplicationUser, int>(winner, maxPoints);
        }
    }
}