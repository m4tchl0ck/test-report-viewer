using TestReportViewer.xUnitTestReportLoader.Model;
using TestExecutionDataModel = TestReportViewer.Data.Model.TestExecution;

namespace TestReportViewer.xUnitTestReportLoader.Tests;

public class MapperTest
{
    private readonly AssemblyFactory _assemblyFactory;

    public MapperTest()
    {
        _assemblyFactory = new AssemblyFactory();
    }

    [Fact]
    public void ShouldMap()
    {
        // Arrange
        var mapper = new Mapper();
        var model = new Assemblies()
        {
            Assembly = new List<Assembly>
            {
                _assemblyFactory.Create()
            }
        };

        // Act
        var test = mapper.Map(model);

        // Assert
        test.Should().HaveCount(2);
        test.Should().BeEquivalentTo(
            new[]
            {
                new TestExecutionDataModel
                {
                    ExecutedTimeStamp = new DateTimeOffset(2023, 05, 05, 13, 09, 30, TimeSpan.Zero),
                    ExecutionTime = TimeSpan.FromSeconds(204.0487180),
                    Name = "Some.Full.Class.Name.Test_Method",
                    Result = "Fail",
                },
                new TestExecutionDataModel
                {
                    ExecutedTimeStamp = new DateTimeOffset(2023, 05, 05, 13, 09, 30, TimeSpan.Zero),
                    ExecutionTime = TimeSpan.FromSeconds(0.2350974),
                    Name = "Some.Full.Class.Name.Another_Test_Method",
                    Result = "Pass",
                }
            });
    }
}
