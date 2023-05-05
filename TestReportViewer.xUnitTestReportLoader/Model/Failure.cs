using System.Xml.Serialization;

namespace TestReportViewer.xUnitTestReportLoader.Model;

[XmlRoot(ElementName = "failure")]
public class Failure
{
    [XmlElement(ElementName = "message")]
    public string Message { get; set; }

    [XmlElement(ElementName = "stack-trace")]
    public string StackTrace { get; set; }
    
    [XmlAttribute(AttributeName = "exception-type")]
    public string ExceptionType { get; set; }
}