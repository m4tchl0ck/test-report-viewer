using TestReportViewer.Data.Memory;
using TestReportViewer.xUnitTestReportLoader;

if (args.Length == 0)
{
    Console.Error.WriteLine("Path to report file required");
    return;
}

var reportPath = args[0];
if (!File.Exists(reportPath))
{
    Console.Error.WriteLine($"{reportPath} do not exists");
    return;
}

var storage = new MemoryStorage();
var loader = new Loader(storage);
var reportStream = File.OpenRead(reportPath);
await loader.Load(reportStream);

storage.Get()
    .Where(testExecution => testExecution.Result == "Fail")
    .ToList()
    .ForEach(testExecution => Console.WriteLine($"{testExecution.Name.PadRight(100)} | {testExecution.Result.PadRight(6)} | {testExecution.ExecutionTime.ToString().PadRight(10)}"));


Console.ReadKey();
