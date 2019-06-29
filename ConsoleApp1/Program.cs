
using NijaDomain.Classes;
using NinjaDomain.DataModel;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            InsertNinja();
            Console.ReadKey();
        }
        private static void InsertNinja()
        {
            Ninja ninja = new Ninja
            {
                Name = "SampsonSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2008, 1, 28),
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
