using System.Xml;
using System.Xml.Serialization;
using TestReportViewer.xUnitTestReportLoader.Model;

namespace TestReportViewer.xUnitTestReportLoader;

internal class Deserializer
{
    public Assemblies FromXml(Stream xml)
    {
        var serializer = new XmlSerializer(typeof(Assemblies));
        using var reader = new XmlTextReader(xml);

        return (Assemblies)serializer.Deserialize(reader)!;
    }
}