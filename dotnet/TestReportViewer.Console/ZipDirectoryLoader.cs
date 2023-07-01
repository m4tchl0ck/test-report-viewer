using TestReportViewer.xUnitTestReportLoader;

namespace TestReportViewer.Console;

internal class ZipDirectoryLoader
{
    private readonly ZipFileLoader _zipFileLoader;

    public ZipDirectoryLoader(ZipFileLoader zipFileLoader)
    {
        _zipFileLoader = zipFileLoader;
    }

    public async Task Load(string zipFilesPath, string zipFilesPattern,
        string reportFilesPattern = "*.*")
    {
        if (!Directory.Exists(zipFilesPath))
        {
            throw new DirectoryNotFoundException();
        }

        foreach (var zipFilePath in Directory.GetFiles(zipFilesPath, zipFilesPattern))
        {
            await _zipFileLoader.Load(zipFilePath,
                reportFilesPattern);
        }
    }
}
