using System.Xml.Serialization;

namespace TestReportViewer.xUnitTestReportLoader.Model;

[XmlRoot(ElementName = "traits")]
public class Traits
{
    [XmlElement(ElementName = "trait")]
    public List<Trait> Trait { get; set; }
}