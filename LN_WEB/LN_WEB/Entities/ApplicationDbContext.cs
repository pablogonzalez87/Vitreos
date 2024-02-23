using System.Collections.Generic;
using System.Data.Entity;

public class ApplicationDbContext : DbContext
{
    public DbSet<Error> Errores { get; set; }
}