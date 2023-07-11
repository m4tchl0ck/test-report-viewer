using System.Globalization;
using TestReportViewer.xUnitTestReportLoader.Model;
using TestExecutionDataModel = TestReportViewer.Data.Model.TestExecution;

namespace TestReportViewer.xUnitTestReportLoader;

internal class Mapper
{
    public IEnumerable<TestExecutionDataModel> Map(Assemblies model)
    {
        return model.Assembly
            .SelectMany(assembly => assembly.Collections
                .SelectMany(collection => collection.Tests.Select(test => new TestExecutionDataModel
                {
                    ExecutedTimeStamp = GetExecutionTimeStamp(assembly.RunDate, assembly.RunTime),
                    ExecutionTime = TimeSpan.FromSeconds(test.Time),
                    Name = test.Name,
                    Result = test.Result
                })));
    }

    private DateTimeOffset GetExecutionTimeStamp(string runDate, string runTime)
         => DateTimeOffset.Parse(runDate,styles: DateTimeStyles.AssumeUniversal).Add(TimeSpan.Parse(runTime));
}
