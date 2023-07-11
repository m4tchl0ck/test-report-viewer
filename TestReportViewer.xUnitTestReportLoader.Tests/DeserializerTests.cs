using TestReportViewer.xUnitTestReportLoader.Model;

namespace TestReportViewer.xUnitTestReportLoader.Tests;

public class DeserializerTests
{
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
                new Assembly
                {
                    Errors = new Errors
                    {
                        Error = new List<Error>()
                    },
                    Name = @"C:\some\path\to\test-assembly-file.dll",
                    Collections = new List<Collection>
                    {
                        new()
                        {
                            Failed = 1,
                            Name = "Test collection for Some.Full.Class.Name",
                            Passed = 1,
                            Skipped = 0,
                            Tests = new List<Test>
                            {
                                new()
                                {
                                    Failure = new Failure
                                    {
                                        Message = "some message",
                                        StackTrace = "stack trace"
                                    },
                                    Method = "Test_Method",
                                    Name = "Some.Full.Class.Name.Test_Method",
                                    Output = "Some output data",
                                    Result = "Fail",
                                    Time = 204.048718,
                                    Traits = new Traits
                                    {
                                        Trait = new List<Trait>()
                                        {
                                            new()
                                            {
                                                Name = "Epic",
                                                Value = "some jira issue"
                                            }
                                        }
                                    },
                                    Type = "Some.Full.Class.Name",
                                },
                                new()
                                {
                                    Method = "Another_Test_Method",
                                    Name = "Some.Full.Class.Name.Another_Test_Method",
                                    Output = "Another output data",
                                    Result = "Pass",
                                    Time = 0.2350974,
                                    Traits = new Traits
                                    {
                                        Trait = new List<Trait>()
                                        {
                                            new Trait
                                            {
                                                Name = "Epic",
                                                Value = "another jira issue"
                                            }
                                        }
                                    },
                                    Type = "Some.Full.Class.Name"
                                }
                            },
                            Total = 2,
                            Time = 408.091
                        }
                    },
                    RunDate = "2023-05-05",
                    RunTime = "13:09:30",
                    Total = 1,
                    Failed = 1,
                    Time = 1524.428
                }
            });
    }
}
