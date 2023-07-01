using TestReportViewer.Data.Model;

namespace TestReportViewer.Data.PostgreSQL;

internal class DbStorage : IStorage
{
    private readonly TestExecutionsContext _context;

    public DbStorage(TestExecutionsContext context)
    {
        _context = context;
    }

    public async Task Add(IEnumerable<TestExecution> testExecutions)
    {
        await _context.TestExecutions.AddRangeAsync(testExecutions.ToArray());
        await _context.SaveChangesAsync();
    }
}
