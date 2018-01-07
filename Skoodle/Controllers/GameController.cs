using Skoodle.BusinessLogic;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skoodle.Controllers
{
    public class GameController : Controller
    {
        GameLogic gameLogic = new GameLogic();
        UserLogic userLogic = new UserLogic();

        [HttpPost]
        public JsonResult CreateGame(int roomId)
        {
            var game = gameLogic.CreateGame(roomId);
            var id = game.GameId;

            return Json(new { gameId = id , roundNum = game.Rounds.Count() + 1});
        }

        [HttpPost]
        public void FinishRound(int gameId, int roundNum, string image)
        {
            var loggedUserId = User.Identity.GetUserId();
            var user = userLogic.GetUserById(loggedUserId);
            string fileName = gameId.ToString() + roundNum.ToString() + user.UserName + ".png";
            string fileNameWithPath = Path.Combine(Server.MapPath("~/UserImages"), Path.GetFileNameWithoutExtension(fileName));
            using (FileStream fs = new FileStream(fileNameWithPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(image);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            gameLogic.UpdateOrCreateRound(gameId, roundNum, user.Id, fileNameWithPath);
        }

        public ActionResult EndRoundScreen(int gameId, int roundNum)
        {
            var loggedUserId = User.Identity.GetUserId();
            var drawingsAndUserIds = gameLogic.GetDrawingsForRound(gameId, roundNum, loggedUserId);
            return Json(drawingsAndUserIds);
        }

        [HttpPost]
        public void VoteForImage(int drawingId)
        {
            gameLogic.AddVoteForImage(drawingId);
        }

        [HttpGet]
        public JsonResult GetUserScores(int gameId, int roundNum)
        {
            var scores = gameLogic.GetRoundScores(gameId, roundNum);
            return Json(scores, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FinishGame(int gameId)
        {

            var userAndScore = gameLogic.FinishGame(gameId);

            return Json(new { user = userAndScore.Key.UserName, score = userAndScore.Value });
        }
    }
}