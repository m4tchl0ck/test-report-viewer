using System.Xml.Serialization;

namespace TestReportViewer.xUnitTestReportLoader.Model;

[XmlRoot(ElementName = "errors")]
public class Errors
{
    [XmlElement(ElementName = "error")]
    public List<Error> Error { get; set; }
}