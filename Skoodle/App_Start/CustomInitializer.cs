using Skoodle.Models;
using System.Data.Entity;

namespace Skoodle.App_Start
{
    public class CustomInitializer: DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Topics.Add(new Topic { Name = "Topic1", Category = "Category1" });
            context.Topics.Add(new Topic { Name = "Topic2", Category = "Category1" });
            context.Topics.Add(new Topic { Name = "Topic3", Category = "Category2" });
            context.Topics.Add(new Topic { Name = "Topic4", Category = "Category2" });
            context.Topics.Add(new Topic { Name = "Topic5", Category = "Category3" });

            context.SaveChanges();
        }
    }
}