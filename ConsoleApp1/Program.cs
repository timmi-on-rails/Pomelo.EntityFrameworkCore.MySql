using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ConsoleApp1
{
    class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    class TestContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .UseMySql("server=localhost;port=3306;database=didab;user=didab;password=didab");
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
            testContext.Persons.Add(new Person { Name = "Lisa" });

            testContext.SaveChanges();

            Log();
        }

        private static TestContext Log()
        {
            var testContext = new TestContext();
            foreach (var person in testContext.Persons.ToList())
            {
                Console.WriteLine($"Id: {person.Id}, Name: {person.Name}");
            }

            return testContext;
        }
    }
}
