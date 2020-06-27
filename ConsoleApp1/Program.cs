using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    class TestContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=didab;user=didab;password=didab");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using var testContext = new TestContext();
            testContext.Database.EnsureDeleted();
            testContext.Database.EnsureCreated();

            testContext.Persons.Add(new Person { Name = "Tom" });
            testContext.SaveChanges();
            var count = testContext.Persons.Count();
            Console.WriteLine("Hello World!");
        }
    }
}
