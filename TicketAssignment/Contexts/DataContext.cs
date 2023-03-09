using Microsoft.EntityFrameworkCore;
using System;
using TicketAssignment.Models.Entities;

namespace TicketAssignment.Contexts;

internal class DataContext : DbContext 
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SaraM\Desktop\repo\DataBase\TicketAssignment\TicketAssignment\Contexts\TicketDB.mdf;Integrated Security=True;Connect Timeout=30";

    #region Constructors;

    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    #endregion

    #region Overrides
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SLAEntity>()
        .HasData(
            new SLAEntity { Id = -1, Severity = SeverityEnum.Minor },
            new SLAEntity { Id = -2, Severity = SeverityEnum.Important },
            new SLAEntity { Id = -3, Severity = SeverityEnum.Urgent });
        base.OnModelCreating(modelBuilder);
    }
    #endregion

    // Classmate Ninja told me about SLA for SLAEntity
    public DbSet<ClientEntity> Clients { get; set; } = null!;
    public DbSet<SLAEntity> SLAs { get; set; } = null!;
    public DbSet<TicketEntity> Tickets { get; set; } = null!;
    public DbSet<CommentEntity> Comments { get; set; } = null!;
}
