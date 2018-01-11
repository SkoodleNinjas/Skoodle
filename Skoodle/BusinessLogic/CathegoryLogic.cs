using Skoodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Skoodle.BusinessLogic
{
    public class CathegoryLogic
    {

        private RoomLogic roomLogic = new RoomLogic();

        private ApplicationDbContext db;

        public CathegoryLogic()
        {
            db = new ApplicationDbContext();
        }

        public Topic RowTopicFromCathegory(Cathegory cathegory)
        {

            List<Topic> topicsCathegory = db.Topics.Where(tp => tp.Category.CathergoryId == cathegory.CathergoryId).ToList(); 

            Random rnd = new Random();
            int rndIndx = rnd.Next();

            return topicsCathegory[rndIndx];
        }

        public Topic PickTopicForRoom(int roomId)
        {
            var cathegory = roomLogic.GetRoomCathegory(roomId);

            return RowTopicFromCathegory(cathegory);
        }

        public List<ListItem> GetCathegoryNames()
        {
            var result = new List<ListItem>();
            var list = db.Cathegories.Select(ct => ct.CathegoryName).ToList();
            foreach(var topic in list)
            {
                result.Add(new ListItem
                {
                    Text = topic,
                    Value = topic
                });
            }

            return result;
        }
    }
}