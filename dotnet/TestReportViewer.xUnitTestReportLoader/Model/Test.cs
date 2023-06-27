using System.Xml.Serialization;

namespace TestReportViewer.xUnitTestReportLoader.Model;

[XmlRoot(ElementName = "test")]
public class Test
{
    [XmlElement(ElementName = "output")]
    public string Output { get; set; }

    [XmlElement(ElementName = "reason")]
    public string Reason { get; set; }

    [XmlElement(ElementName = "failure")]
    public Failure Failure { get; set; }

    [XmlElement(ElementName = "traits")]
    public Traits Traits { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "method")]
    public string Method { get; set; }

    [XmlAttribute(AttributeName = "time")]
    public double Time { get; set; }

    [XmlAttribute(AttributeName = "result")]
    public string Result { get; set; }
}