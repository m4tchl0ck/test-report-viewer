using TestReportViewer.Console;
using TestReportViewer.Data.Memory;
using TestReportViewer.xUnitTestReportLoader;

const string FileArg = "-f";
const string DirectoryArg = "-d";
const string ZipFileArg = "-zf";
const string ZipDirectoryArg = "-zd";

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

try
{
    var operation = args[0];
    switch (operation)
    {
        case FileArg:
            await new FileLoader().Load(loader, args[1]);
            break;
        case DirectoryArg:
            await new DirectoryLoader(new FileLoader()).Load(loader, args[1], args[2]);
            break;
        case ZipFileArg:
            await new ZipFileLoader(new DirectoryLoader(new FileLoader())).Load(loader, args[1], args[2]);
            break;
        case ZipDirectoryArg:
            await new ZipDirectoryLoader(new ZipFileLoader(new DirectoryLoader(new FileLoader()))).Load(loader, args[1], args[2], args[3]);
            break;
        default:
            HandleWrongArgument();
            break;
    }
}
catch (Exception e)
{
    HandleWrongArgument(e.Message);
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
                $"{testExecution.ExecutedTimeStamp} | {testExecution.Name.PadRight(maxNameLength)} | {testExecution.Result} | {testExecution.ExecutionTime}");
        Console.WriteLine(testExecution.Failure);
    });
