using System;
using Microsoft.EntityFrameworkCore;
using Roger.Models;

namespace Roger.Data;

public class AppDbContext : DbContext
{
    public DbSet<Livro> livro { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Ivan_Roger.db");
    }
}
