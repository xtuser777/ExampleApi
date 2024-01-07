using System.Security.Cryptography;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Contexts;

public class ExampleApiContext(DbContextOptions<ExampleApiContext> options) : DbContext(options)
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Address>().HasKey(a => a.Id);
        mb.Entity<Address>().Property(a => a.Street).HasMaxLength(80).IsRequired();
        mb.Entity<Address>().Property(a => a.Number).HasMaxLength(5).IsRequired();
        mb.Entity<Address>().Property(a => a.Neighborhood).HasMaxLength(40).IsRequired();
        mb.Entity<Address>().Property(a => a.Complement).HasMaxLength(30);
        mb.Entity<Address>().Property(a => a.Code).HasMaxLength(10).IsRequired();
        mb.Entity<Address>().Property(a => a.City).HasMaxLength(70).IsRequired();
        mb.Entity<Address>().Property(a => a.State).HasMaxLength(2).IsRequired();

        mb.Entity<Contact>().HasKey(c => c.Id);
        mb.Entity<Contact>().Property(c => c.Phone).HasMaxLength(14).IsRequired();
        mb.Entity<Contact>().Property(c => c.Cellphone).HasMaxLength(15).IsRequired();
        mb.Entity<Contact>().Property(c => c.Email).HasMaxLength(100).IsRequired();

        mb.Entity<Individual>().HasKey(i => i.Id);
        mb.Entity<Individual>().Property(i => i.Name).HasMaxLength(100).IsRequired();
        mb.Entity<Individual>().Property(i => i.Document).HasMaxLength(14).IsRequired();
        mb.Entity<Individual>().Property(i => i.Birth).HasMaxLength(20).IsRequired();
        mb.Entity<Individual>().HasOne(i => i.Address).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        mb.Entity<Individual>().HasOne(i => i.Contact).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);

        mb.Entity<Customer>().HasKey(c => c.Id);
        mb.Entity<Customer>().HasOne(c => c.Individual).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);

        mb.Entity<User>().HasKey(u => u.Id);
        mb.Entity<User>().Property(u => u.UserName).HasMaxLength(20).IsRequired();
        mb.Entity<User>().Property(u => u.Password).HasMaxLength(150).IsRequired();
        mb.Entity<User>().HasOne(u => u.Individual).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(mb);
    }
}