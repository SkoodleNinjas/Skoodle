namespace Skoodle.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Skoodle.Models;
    using System.Collections;
    using System.Collections.Generic;

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

            context.Cathegories.AddOrUpdate(
                new Cathegory { CathegoryName = "animals" },
                new Cathegory { CathegoryName = "states" },
                new Cathegory { CathegoryName = "phobias" },
                new Cathegory { CathegoryName = "characters" }
                );

            context.SaveChanges();
            Dictionary<string, Cathegory> nameCathegories = new Dictionary<string, Cathegory>();

            List<Cathegory> allCathegories = context.Cathegories.ToList();

            foreach (var cat in allCathegories)
            {
                nameCathegories.Add(cat.CathegoryName, cat);
            }

            context.Topics.AddOrUpdate(
              //p => p.FullName,{
              new Topic { Name = "FlyingCat", Category = nameCathegories["animals"] },
              new Topic { Name = "CudeDogge", Category = nameCathegories["animals"] },
              new Topic { Name = "ShortGiraffe", Category = nameCathegories["animals"] },
              new Topic { Name = "SlimHippo", Category = nameCathegories["animals"] },
              new Topic { Name = "LongHornedGoat", Category = nameCathegories["animals"] },
              new Topic { Name = "PurpleZebra", Category = nameCathegories["animals"] },
              new Topic { Name = "StylishRhino", Category = nameCathegories["animals"] },
              new Topic { Name = "NosyLama", Category = nameCathegories["animals"] },
              new Topic { Name = "AhKakvaSumAntilopa", Category = nameCathegories["animals"] },
              new Topic { Name = "Brave", Category = nameCathegories["states"] },
              new Topic { Name = "Sad", Category = nameCathegories["states"] },
              new Topic { Name = "Angry", Category = nameCathegories["states"] },
              new Topic { Name = "Sleepy", Category = nameCathegories["states"] },
              new Topic { Name = "Exhausted", Category = nameCathegories["states"] },
              new Topic { Name = "Sick", Category = nameCathegories["states"] },
              new Topic { Name = "Happy", Category = nameCathegories["states"] },
              new Topic { Name = "Anxious", Category = nameCathegories["states"] },
              new Topic { Name = "Bored", Category = nameCathegories["states"] },
              new Topic { Name = "Calm", Category = nameCathegories["states"] },
              new Topic { Name = "Crazy", Category = nameCathegories["states"] },
              new Topic { Name = "Dirty", Category = nameCathegories["states"] },
              new Topic { Name = "Lazy", Category = nameCathegories["states"] },
              new Topic { Name = "Rebellious", Category = nameCathegories["states"] },
              new Topic { Name = "Weird", Category = nameCathegories["states"] },
              new Topic { Name = "Ablutophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Achluophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Ambulophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Amaxophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Bathophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Bufonophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Blennophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Bibliophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Cleptophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Coulrophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Catoptrophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Cathisophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Dutchphobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Gymnophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Gynephobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Hexakosioihexekontahexaphobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Hippopotomonstrosesquipedaliophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Hobophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Hominophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Hypsiphobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Ligyrophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Selenophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Verminophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Zoophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Xanthophobia", Category = nameCathegories["phobias"] },
              new Topic { Name = "Superman", Category = nameCathegories["characters"] },
              new Topic { Name = "Batman", Category = nameCathegories["characters"] },
              new Topic { Name = "Spiderman", Category = nameCathegories["characters"] },
              new Topic { Name = "Mario", Category = nameCathegories["characters"] },
              new Topic { Name = "PacMan", Category = nameCathegories["characters"] },
              new Topic { Name = "Skooby", Category = nameCathegories["characters"] },
              new Topic { Name = "Donald", Category = nameCathegories["characters"] },
              new Topic { Name = "WinnieThePooh", Category = nameCathegories["characters"] },
              new Topic { Name = "Deadpool", Category = nameCathegories["characters"] },
              new Topic { Name = "MargeSimpson", Category = nameCathegories["characters"] },
              new Topic { Name = "Waldo", Category = nameCathegories["characters"] },
              new Topic { Name = "ThomasTheTankEngine", Category = nameCathegories["characters"] }
            );
            context.SaveChanges();
        }
    }
}