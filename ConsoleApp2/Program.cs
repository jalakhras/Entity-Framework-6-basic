using NijaDomain.Classes;
using NijaDomain.Classes.Enum;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            // InsertNinja();
            // InsertMultipleNinjas();
            // SimpleNinjaQueries();
            //QueryAndUpdateNinja();
           // DeleteNinja();
            //RetrieveDataWithFind();
            //RetrieveDataWithStoredProc();
            //DeleteNinjaWithKeyValue();
            //DeleteNinjaViaStoredProcedure();
            //QueryAndUpdateNinjaDisconnected();

            InsertNinjaWithEquipment();
            //SimpleNinjaGraphQuery();
            //ProjectionQuery();
            //QueryAndUpdateNinjaDisconnected();

            //ReseedDatabase();
            Console.ReadKey();
        }
        private static void InsertNinja()
        {
            Ninja ninja = new Ninja
            {
                Name = "Jassar",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1994, 12, 03),
                ClanId = 1

            };
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleNinjas()
        {
            Ninja ninja1 = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 1),
                ClanId = 1
            };
            Ninja ninja2 = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 1),
                ClanId = 1
            };
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQueries()
        {
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                IQueryable<Ninja> ninjas = context.Ninjas
                    .Where(n => n.DateOfBirth >= new DateTime(1984, 1, 1))
                    .OrderBy(n => n.Name)
                    .Skip(1).Take(1);

                //var query = context.Ninjas;
                // var someninjas = query.ToList();
                foreach (Ninja ninja in ninjas)
                {
                    Console.WriteLine(ninja.Name);
                }
            }
        }

        private static void QueryAndUpdateNinja()
        {
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                Ninja ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinjaDisconnected()
        {
            Ninja ninja;
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void RetrieveDataWithFind()
        {
            int keyval = 4;
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                Ninja ninja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#1:" + ninja.Name);

                Ninja someNinja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#2:" + someNinja.Name);
                ninja = null;
            }
        }

        private static void RetrieveDataWithStoredProc()
        {

            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                List<Ninja> ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas").ToList();
                //foreach (var ninja in ninjas)
                //{
                //    Console.WriteLine(ninja.Name);
                //}
            }
        }

        private static void DeleteNinja()
        {
            Ninja ninja;
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
                //context.Ninjas.Remove(ninja);
                //context.SaveChanges();
            }
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //context.Ninjas.Attach(ninja);
                //context.Ninjas.Remove(ninja);
                context.Entry(ninja).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaWithKeyValue()
        {
            int keyval = 1;
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                Ninja ninja = context.Ninjas.Find(keyval);
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaViaStoredProcedure()
        {
            int keyval = 3;
            using (NinjaContext context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand(
                    "exec DeleteNinjaViaId {0}", keyval);
            }
        }

        private static void InsertNinjaWithEquipment()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = new Ninja
                {
                    Name = "Kacy Catanzaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    ClanId = 1
                };
                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool,

                };
                var spunk = new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }

        }

        private static void SimpleNinjaGraphQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                //var ninjas = context.Ninjas.Include(n => n.EquipmentOwned)
                //    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));

                var ninja = context.Ninjas
                           .FirstOrDefault(n => n.Name.StartsWith("Kacy"));
                Console.WriteLine("Ninja Retrieved:" + ninja.Name);
                //context.Entry(ninja).Collection(n => n.EquipmentOwned).Load();
                Console.WriteLine("Ninja Equipment cout {0}", ninja.EquipmentOwned.Count());
            }

        }

        private static void ProjectionQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas
                    .Select(n => new { n.Name, n.DateOfBirth, n.EquipmentOwned })
                    .ToList();

            }
        }
    }
}
