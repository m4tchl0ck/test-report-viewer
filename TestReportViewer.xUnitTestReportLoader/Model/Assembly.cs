using System.Xml.Serialization;

namespace TestReportViewer.xUnitTestReportLoader.Model;

[XmlRoot(ElementName = "assembly")]
public class Assembly
{
    [XmlElement(ElementName = "errors")]
    public Errors Errors { get; set; }

    [XmlElement(ElementName = "collection")]
    public List<Collection> Collections { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "run-date")]
    public string RunDate { get; set; }

    [XmlAttribute(AttributeName = "run-time")]
    public string RunTime { get; set; }

    [XmlAttribute(AttributeName = "total")]
    public int Total { get; set; }

    [XmlAttribute(AttributeName = "passed")]
    public int Passed { get; set; }

    [XmlAttribute(AttributeName = "failed")]
    public int Failed { get; set; }

    [XmlAttribute(AttributeName = "skipped")]
    public int Skipped { get; set; }

    [XmlAttribute(AttributeName = "time")]
    public double Time { get; set; }
}