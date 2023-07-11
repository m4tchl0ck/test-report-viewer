namespace TestReportViewer.xUnitTestReportLoader.Tests;

public class DeserializerTests
{
    private readonly AssemblyFactory _assemblyFactory;

    public DeserializerTests()
    {
        _assemblyFactory = new AssemblyFactory();
    }

    [Fact]
    public void ShouldDeserializeModelFromXml()
    {
        // Arrange
        var deserializer = new Deserializer();
        using var streamReader = new StreamReader("TestReport.xml");

        // Act
        var model = deserializer.FromXml(streamReader.BaseStream);

        // Asset
        model.Timestamp.Should().Be("05/05/2023 13:09:30");
        model.Assembly.Should().BeEquivalentTo(
            new[]
            {
                _assemblyFactory.Create()
            });
    }
}