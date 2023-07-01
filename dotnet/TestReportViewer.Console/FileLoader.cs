using TestReportViewer.xUnitTestReportLoader;

namespace TestReportViewer.Console;

internal class FileLoader
{
    private readonly Loader _loader;

    public FileLoader(Loader loader)
    {
        _loader = loader;
    }

    public async Task Load(string reportPath)
    {
        if (!File.Exists(reportPath))
        {
            throw new FileNotFoundException();
        }

        var reportStream = File.OpenRead(reportPath);
        await _loader.Load(reportStream);
    }
}

internal record FileLoaderOptions(string Path);
