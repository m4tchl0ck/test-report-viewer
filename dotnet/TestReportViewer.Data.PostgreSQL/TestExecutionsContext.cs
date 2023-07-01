using Microsoft.EntityFrameworkCore;
using TestReportViewer.Data.Model;

namespace TestReportViewer.Data.PostgreSQL;

internal class TestExecutionsContext : DbContext
{
    public  DbSet<TestExecution> TestExecutions { get; set; }
}
