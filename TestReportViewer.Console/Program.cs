using System.IO.Compression;
using TestReportViewer.Data.Memory;
using TestReportViewer.xUnitTestReportLoader;

const string FileArg = "-f";
const string DirectoryArg = "-d";
const string ZipFileArg = "-zf";

async Task LoadFromZipFile(Loader loader, string zipFilePath, string filePattern = "*.*")
{
    if (!File.Exists(zipFilePath))
    {
        HandleWrongArgument("File not found");
    }
    
    using var zip = ZipFile.OpenRead(zipFilePath);
    var reportsPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
    zip.ExtractToDirectory(reportsPath);
    await LoadFromDirectory(loader, reportsPath, filePattern);
    Directory.Delete(reportsPath);
}

async Task LoadFromDirectory(Loader loader, string reportsPath, string filePattern = "*.*")
{
    if (!Directory.Exists(reportsPath))
    {
        HandleWrongArgument("Directory not found");
    }

    foreach (var reportPath in Directory.GetFiles(reportsPath, filePattern))
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
{DirectoryArg} <directory> <patter>
{ZipFileArg} <file> <patter>"
    );
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
    default:
        HandleWrongArgument();
        break;
}


storage.Get()
    .Where(testExecution => testExecution.Result == "Fail")
    .ToList()
    .ForEach(testExecution => Console.WriteLine($"{testExecution.Name.PadRight(100)} | {testExecution.Result.PadRight(6)} | {testExecution.ExecutionTime.ToString().PadRight(10)}"));


Console.ReadKey();
