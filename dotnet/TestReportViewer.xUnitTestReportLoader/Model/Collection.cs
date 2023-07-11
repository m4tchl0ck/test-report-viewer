using System.Xml.Serialization;

namespace TestReportViewer.xUnitTestReportLoader.Model;

[XmlRoot(ElementName = "collection")]
public class Collection
{
    [XmlElement(ElementName = "test")]
    public List<Test> Tests { get; set; }

    [XmlAttribute(AttributeName = "total")]
    public int Total { get; set; }

    [XmlAttribute(AttributeName = "passed")]
    public int Passed { get; set; }

    [XmlAttribute(AttributeName = "failed")]
    public int Failed { get; set; }

    [XmlAttribute(AttributeName = "skipped")]
    public int Skipped { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "time")]
    public double Time { get; set; }
}