using TestReportViewer.xUnitTestReportLoader;

namespace TestReportViewer.Console;

internal class FileLoader
{
    public async Task Load(Loader loader, string reportPath)
    {
        if (!File.Exists(reportPath))
        {
            throw new FileNotFoundException();
        }

        var reportStream = File.OpenRead(reportPath);
        await loader.Load(reportStream);
    }
}
