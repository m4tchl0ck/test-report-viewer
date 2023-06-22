using TestReportViewer.Data;

namespace TestReportViewer.xUnitTestReportLoader;

public class Loader
{
    private readonly IStorage _storage;
    private readonly Deserializer _deserialize;
    private readonly Mapper _mapper;

    public Loader(IStorage storage)
    {
        _storage = storage;
        _deserialize = new Deserializer();
        _mapper = new Mapper();
    }

    public async Task Load(Stream stream)
    {
        var model = _deserialize.FromXml(stream);
        var testExecutions = _mapper.Map(model);
        await _storage.Add(testExecutions);
    }
}
