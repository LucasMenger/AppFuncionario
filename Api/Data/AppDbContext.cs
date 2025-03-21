using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Employee>().ToTable("employees");
        base.OnModelCreating(modelBuilder);

        // Configuração do relacionamento entre Usuario e Gerente
        modelBuilder.Entity<Employee>()
            .HasOne(u => u.Gerente)
            .WithMany()
            .HasForeignKey(u => u.GerenteId)
            .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata

        // Índice único para CPF
        modelBuilder.Entity<Employee>()
            .HasIndex(u => u.CPF)
            .IsUnique();

        // Índice único para Email
        modelBuilder.Entity<Employee>()
            .HasIndex(u => u.Email)
            .IsUnique();

    }
}