using System.IO.Compression;
using TestReportViewer.Data.Memory;
using TestReportViewer.xUnitTestReportLoader;

const string FileArg = "-f";
const string DirectoryArg = "-d";
const string ZipFileArg = "-zf";
const string ZipDirectoryArg = "-zd";

async Task LoadFromZipDirectory(Loader loader, string zipFilesPath, string zipFilesPattern, string reportFilesPattern = "*.*")
{
    if (!Directory.Exists(zipFilesPath))
    {
        HandleWrongArgument("Directory not found");
    }

    foreach (var zipFilePath in Directory.GetFiles(zipFilesPath, zipFilesPattern))
    {
        await LoadFromZipFile(loader, zipFilePath, reportFilesPattern);
    }

}

async Task LoadFromZipFile(Loader loader, string zipFilePath, string reportFilesPattern = "*.*")
{
    if (!File.Exists(zipFilePath))
    {
        HandleWrongArgument("File not found");
    }
    
    using var zip = ZipFile.OpenRead(zipFilePath);
    var reportsPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
    zip.ExtractToDirectory(reportsPath);
    await LoadFromDirectory(loader, reportsPath, reportFilesPattern);
    Directory.Delete(reportsPath, true);
}

async Task LoadFromDirectory(Loader loader, string reportsPath, string reportFilesPattern = "*.*")
{
    if (!Directory.Exists(reportsPath))
    {
        HandleWrongArgument("Directory not found");
    }

    foreach (var reportPath in Directory.GetFiles(reportsPath, reportFilesPattern))
    {
        await LoadFromFile(loader, reportPath);
    }
}

async Task LoadFromFile(Loader loader, string reportPath)
{
    if (!File.Exists(reportPath))
    {
        HandleWrongArgument("File not found");
    }

    var reportStream = File.OpenRead(reportPath);
    await loader.Load(reportStream);
}

void HandleWrongArgument(string message = "wrong parameter")
{
    Console.Error.WriteLine(@$"
{message}

parameters
{FileArg} <file>
{DirectoryArg} <directory> <report files patter>
{ZipFileArg} <file> <patter>
{ZipDirectoryArg} <directory> <zip files pattern> <report files pattern>
");
    throw new ArgumentException();
}

if (args.Length < 2)
{
    HandleWrongArgument();
}

var storage = new MemoryStorage();
var loader = new Loader(storage);

var operation = args[0];
switch (operation)
{
    case FileArg:
        await LoadFromFile(loader, args[1]);
        break;
    case DirectoryArg:
        await LoadFromDirectory(loader, args[1], args[2]);
        break;
    case ZipFileArg:
        await LoadFromZipFile(loader, args[1], args[2]);
        break;
    case ZipDirectoryArg:
        await LoadFromZipDirectory(loader, args[1], args[2], args[3]);
        break;
    default:
        HandleWrongArgument();
        break;
}


var executions = storage.Get()
    .Where(testExecution => testExecution.Result == "Fail")
    .OrderBy(testExecution => testExecution.ExecutedTimeStamp)
    .ToList();

var maxNameLength = executions.Max(testExecution => testExecution.Name.Length);

executions
    .ForEach(testExecution =>
    {
        Console.WriteLine(
                $"{testExecution.ExecutedTimeStamp} | {testExecution.Name.PadRight(maxNameLength)} | {testExecution.Result} | {testExecution.ExecutionTime }");
        Console.WriteLine(testExecution.Failure);
    });
