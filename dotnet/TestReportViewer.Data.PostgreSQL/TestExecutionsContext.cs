using Microsoft.EntityFrameworkCore;
using TestReportViewer.Data.Model;

namespace TestReportViewer.Data.PostgreSQL;

internal class TestExecutionsContext : DbContext
{
    public DbSet<TestExecution> TestExecutions { get; set; }

    public TestExecutionsContext(DbContextOptions<TestExecutionsContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("test_executions");

        modelBuilder
            .Entity<TestExecution>()
            .ToTable("execution")
            .HasKey("Id");

        modelBuilder
            .Entity<TestExecution>()
            .Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        modelBuilder
            .Entity<TestExecution>()
            .Property(e => e.Name)
            .HasColumnName("test_name");

        modelBuilder
            .Entity<TestExecution>()
            .Property(e => e.Result)
            .HasColumnName("result");

        modelBuilder
            .Entity<TestExecution>()
            .Property(e => e.ExecutedTimeStamp)
            .HasColumnName("execution_time_stamp");

        modelBuilder
            .Entity<TestExecution>()
            .Property(e => e.ExecutionTime)
            .HasColumnName("execution_time");

        modelBuilder
            .Entity<TestExecution>()
            .Ignore(e => e.Failure);

    }
}
