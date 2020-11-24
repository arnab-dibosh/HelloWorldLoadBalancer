using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestHelloWorld.Model
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
           : base(options) {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<SampleBank1> SampleBank1Info { get; set; }
        public DbSet<SampleBank2> SampleBank2Info { get; set; }
    }
}
