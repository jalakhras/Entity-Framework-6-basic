using NijaDomain.Classes;
using NinjaDomain.DataModel;
using System;
using System.Data.Entity;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            InsertNinja();
            // InsertMultipleNinjas();
            //SimpleNinjaQueries();
            //QueryAndUpdateNinja();
            //DeleteNinja();
            //RetrieveDataWithFind();
            //RetrieveDataWithStoredProc();
            //DeleteNinjaWithKeyValue();
            //DeleteNinjaViaStoredProcedure();
            //QueryAndUpdateNinjaDisconnected();

            //InsertNinjaWithEquipment();
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
    }
}
