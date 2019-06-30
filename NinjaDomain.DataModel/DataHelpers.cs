using NijaDomain.Classes;
using NijaDomain.Classes.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NinjaDomain.DataModel
{
    public class DataHelpers
    {
        public static void NewDbWithSeed()
        {

            Database.SetInitializer(new DropCreateDatabaseAlways<NinjaContext>());
            using (NinjaContext context = new NinjaContext())
            {
                if (context.Ninjas.Any())
                {
                    return;
                }
                Clan vtClan = context.Clans.Add(new Clan { ClanName = "Vermont Clan" });
                Clan turtleClan = context.Clans.Add(new Clan { ClanName = "Turtles" });
                Clan amClan = context.Clans.Add(new Clan { ClanName = "American Ninja Warriors" });

                Ninja j = new Ninja
                {
                    Name = "JulieSan",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1980, 1, 1),
                    Clan = vtClan

                };
                Ninja s = new Ninja
                {
                    Name = "SampsonSan",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(2008, 1, 28),
                    ClanId = 1

                };
                Ninja l = new Ninja
                {
                    Name = "Leonardo",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1984, 1, 1),
                    Clan = turtleClan
                };
                Ninja r = new Ninja
                {
                    Name = "Raphael",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1985, 1, 1),
                    Clan = turtleClan
                };
                context.Ninjas.AddRange(new List<Ninja> { j, s, l, r });
                Ninja ninjaKC = new Ninja
                {
                    Name = "Kacy Catanzaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    Clan = amClan
                };
                NinjaEquipment muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool,

                };
                NinjaEquipment spunk = new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                ninjaKC.EquipmentOwned.Add(muscles);
                ninjaKC.EquipmentOwned.Add(spunk);
                context.Ninjas.Add(ninjaKC);

                context.SaveChanges();
                context.Database.ExecuteSqlCommand(
                  @"CREATE PROCEDURE GetOldNinjas
                    AS  SELECT * FROM Ninjas WHERE DateOfBirth<='1/1/1980'");

                context.Database.ExecuteSqlCommand(
                   @"CREATE PROCEDURE DeleteNinjaViaId
                     @Id int
                     AS
                     DELETE from Ninjas Where Id = @id
                     RETURN @@rowcount");
            }
        }
    }
}

