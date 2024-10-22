// using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

public class PersonDBContext:DbContext
{
    public DbSet<Person> People{get; set;}

    public PersonDBContext(DbContextOptions<PersonDBContext> options):base(options)
    {
        
    }
}