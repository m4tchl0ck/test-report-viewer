using TestReportViewer.Data.Model;

namespace TestReportViewer.Data.Memory;
public class MemoryStorage : IStorage
{
    private readonly List<TestExecution> _testExecutions = new();

    public Task Add(IEnumerable<TestExecution> testExecutions)
    {
        _testExecutions.AddRange(testExecutions);
        return Task.CompletedTask;
    }

    public IQueryable<TestExecution> Get()
    {
        return _testExecutions.AsQueryable();
    }
}
