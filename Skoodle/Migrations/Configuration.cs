namespace Skoodle.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Skoodle.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Skoodle.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Skoodle.Models.ApplicationDbContext";
        }

        protected override void Seed(Skoodle.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Topics.AddOrUpdate(
              //p => p.FullName,
              new Topic { Name = "FlyingCat", Category = "animals" },
              new Topic { Name = "CudeDogge", Category = "animals" },
              new Topic { Name = "ShortGiraffe", Category = "animals" },
              new Topic { Name = "SlimHippo", Category = "animals" },
              new Topic { Name = "LongHornedGoat", Category = "animals" },
              new Topic { Name = "PurpleZebra", Category = "animals" },
              new Topic { Name = "StylishRhino", Category = "animals" },
              new Topic { Name = "NosyLama", Category = "animals" },
              new Topic { Name = "AhKakvaSumAntilopa", Category = "animals" },
              new Topic { Name = "Brave", Category = "adjectives" },
              new Topic { Name = "Sad", Category = "adjectives" },
              new Topic { Name = "Angry", Category = "adjectives" },
              new Topic { Name = "Sleepy", Category = "adjectives" },
              new Topic { Name = "Exhausted", Category = "adjectives" },
              new Topic { Name = "Sick", Category = "adjectives" },
              new Topic { Name = "Happy", Category = "adjectives" },
              new Topic { Name = "Anxious", Category = "adjectives" },
              new Topic { Name = "Bored", Category = "adjectives" },
              new Topic { Name = "Calm", Category = "adjectives" },
              new Topic { Name = "Crazy", Category = "adjectives" },
              new Topic { Name = "Dirty", Category = "adjectives" },
              new Topic { Name = "Lazy", Category = "adjectives" },
              new Topic { Name = "Rebellious", Category = "adjectives" },
              new Topic { Name = "Weird", Category = "adjectives" },
              new Topic { Name = "Ablutophobia", Category = "phobias" },
              new Topic { Name = "Achluophobia", Category = "phobias" },
              new Topic { Name = "Ambulophobia", Category = "phobias" },
              new Topic { Name = "Amaxophobia", Category = "phobias" },
              new Topic { Name = "Bathophobia", Category = "phobias" },
              new Topic { Name = "Bufonophobia", Category = "phobias" },
              new Topic { Name = "Blennophobia", Category = "phobias" },
              new Topic { Name = "Bibliophobia", Category = "phobias" },
              new Topic { Name = "Cleptophobia", Category = "phobias" },
              new Topic { Name = "Coulrophobia", Category = "phobias" },
              new Topic { Name = "Catoptrophobia", Category = "phobias" },
              new Topic { Name = "Cathisophobia", Category = "phobias" },
              new Topic { Name = "Dutchphobia", Category = "phobias" },
              new Topic { Name = "Gymnophobia", Category = "phobias" },
              new Topic { Name = "Gynephobia", Category = "phobias" },
              new Topic { Name = "Hexakosioihexekontahexaphobia", Category = "phobias" },
              new Topic { Name = "Hippopotomonstrosesquipedaliophobia", Category = "phobias" },
              new Topic { Name = "Hobophobia", Category = "phobias" },
              new Topic { Name = "Hominophobia", Category = "phobias" },
              new Topic { Name = "Hypsiphobia", Category = "phobias" },
              new Topic { Name = "Ligyrophobia", Category = "phobias" },
              new Topic { Name = "Selenophobia", Category = "phobias" },
              new Topic { Name = "Verminophobia", Category = "phobias" },
              new Topic { Name = "Zoophobia", Category = "phobias" },
              new Topic { Name = "Xanthophobia", Category = "phobias" },
              new Topic { Name = "Superman", Category = "characters" },
              new Topic { Name = "Batman", Category = "characters" },
              new Topic { Name = "Spiderman", Category = "characters" },
              new Topic { Name = "Mario", Category = "characters" },
              new Topic { Name = "PacMan", Category = "characters" },
              new Topic { Name = "Skooby", Category = "characters" },
              new Topic { Name = "Donald", Category = "characters" },
              new Topic { Name = "WinnieThePooh", Category = "characters" },
              new Topic { Name = "Deadpool", Category = "characters" },
              new Topic { Name = "MargeSimpson", Category = "characters" },
              new Topic { Name = "Waldo", Category = "characters" },
              new Topic { Name = "ThomasTheTankEngine", Category = "characters" }
            );

        }
    }
}
