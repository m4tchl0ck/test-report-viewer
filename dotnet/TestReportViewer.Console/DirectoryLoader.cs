using TestReportViewer.xUnitTestReportLoader;

namespace TestReportViewer.Console;

internal class DirectoryLoader
{
    private readonly FileLoader _fileLoader;

    public DirectoryLoader(FileLoader fileLoader)
    {
        _fileLoader = fileLoader;
    }

    public async Task Load(Loader loader, string reportsPath, string reportFilesPattern = "*.*")
    {
        if (!Directory.Exists(reportsPath))
        {
            throw new DirectoryNotFoundException();
        }

        foreach (var reportPath in Directory.GetFiles(reportsPath, reportFilesPattern))
        {
            await _fileLoader.Load(loader, reportPath);
        }
    }
}
