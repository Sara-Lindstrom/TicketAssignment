using Microsoft.EntityFrameworkCore;
using TicketAssignment.Models.Entities;

namespace TicketAssignment.Contexts;

internal class DataContext : DbContext 
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SaraM\Desktop\repo\DataBase\TicketAssignment\TicketAssignment\Contexts\TicketAssignmentDB.mdf;Integrated Security=True;Connect Timeout=30";

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
        base.OnModelCreating(modelBuilder);
    }
    #endregion

    // Classmate Ninja told me about SLA for SLAEntity
    public DbSet<ClientEntity> Clients { get; set; } = null!;
    public DbSet<SLAEntity> SLAs { get; set; } = null!;
    public DbSet<TicketEntity> Tickets { get; set; } = null!;
    public DbSet<CommentEntity> Comments { get; set; } = null!;
}
