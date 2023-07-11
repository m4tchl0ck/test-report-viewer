namespace TestReportViewer.Data.Model;

public class TestExecution
{
    public string Name { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public string Result { get; set; }
    public DateTimeOffset ExecutedTimeStamp { get; set; }
    public string Failure { get; set; }
}
