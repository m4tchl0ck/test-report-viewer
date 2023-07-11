using TestReportViewer.Data.Model;

namespace TestReportViewer.Data;

public interface IStorage
{
    Task Add(IEnumerable<TestExecution> testExecutions);
}
