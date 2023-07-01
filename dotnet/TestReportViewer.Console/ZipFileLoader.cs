using System.IO.Compression;
using TestReportViewer.xUnitTestReportLoader;

namespace TestReportViewer.Console;

internal class ZipFileLoader
{
    private readonly DirectoryLoader _directoryLoader;

    public ZipFileLoader(DirectoryLoader directoryLoader)
    {
        _directoryLoader = directoryLoader;
    }

    public async Task Load(string zipFilePath, string reportFilesPattern = "*.*")
    {
        if (!File.Exists(zipFilePath))
        {
            throw new FileNotFoundException();
        }

        using var zip = ZipFile.OpenRead(zipFilePath);
        var reportsPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        zip.ExtractToDirectory(reportsPath);
        await _directoryLoader.Load(reportsPath, reportFilesPattern);
        Directory.Delete(reportsPath, true);
    }
}
