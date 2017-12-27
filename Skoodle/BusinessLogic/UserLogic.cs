using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Skoodle.Models;
using Skoodle.ViewModels;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Skoodle.BusinessLogic
{
    public class UserLogic
    {

        private ApplicationDbContext db;

        public UserLogic()
        {
            db = new ApplicationDbContext();
        }

        public ApplicationUser GetUserById(string id)
        {
            ApplicationUser loggedUser = db.Users.FirstOrDefault(x => x.Id == id) as ApplicationUser;
            return loggedUser;
        }
    }
}