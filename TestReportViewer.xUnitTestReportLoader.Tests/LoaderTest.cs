using Moq;
using TestReportViewer.Data;
using TestReportViewer.Data.Model;

namespace TestReportViewer.xUnitTestReportLoader.Tests;

public class LoaderTest
{
    [Fact]
    public async Task ShouldLoad()
    {
        // Arrange
        var storageMock = new Mock<IStorage>();
        var loader = new Loader(storageMock.Object);
        using var streamReader = new StreamReader("TestReport.xml");

        // Act
        await loader.Load(streamReader.BaseStream);

        // Assert
        storageMock.Verify(
            x => x.Add(It.Is<IEnumerable<TestExecution>>(
                executions => executions.Count() == 2)
            )
            , Times.Once);
    }
}
