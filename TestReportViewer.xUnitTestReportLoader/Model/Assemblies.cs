using System.Xml.Serialization;

namespace TestReportViewer.xUnitTestReportLoader.Model;

[XmlRoot(ElementName = "assemblies")]
public class Assemblies
{
    [XmlElement(ElementName = "assembly")]
    public List<Assembly> Assembly { get; set; }

    [XmlAttribute(AttributeName = "timestamp")]
    public string Timestamp { get; set; }
}