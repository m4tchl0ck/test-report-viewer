using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestReportViewer.Console;
using TestReportViewer.Data;
using TestReportViewer.Data.Memory;
using TestReportViewer.xUnitTestReportLoader;

const string FileArg = "-f";
const string DirectoryArg = "-d";
const string ZipFileArg = "-zf";
const string ZipDirectoryArg = "-zd";

using var host = Host
    .CreateDefaultBuilder()
    .ConfigureServices(services =>
        {
            services
                .AddSingleton<MemoryStorage>()
                .AddSingleton<IStorage>(sp => sp.GetRequiredService<MemoryStorage>())
                .AddTransient<Loader>()
                .AddTransient<FileLoader>()
                .AddTransient<DirectoryLoader>()
                .AddTransient<ZipFileLoader>()
                .AddTransient<ZipDirectoryLoader>();
        })
    .Build();

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

await host.StartAsync();

if (args.Length < 2)
{
    HandleWrongArgument();
}

try
{
    var operation = args[0];
    switch (operation)
    {
        case FileArg:
            await host.Services.GetRequiredService<FileLoader>().Load(args[1]);
            break;
        case DirectoryArg:
            await host.Services.GetRequiredService<DirectoryLoader>().Load(args[1], args[2]);
            break;
        case ZipFileArg:
            await host.Services.GetRequiredService<ZipFileLoader>().Load(args[1], args[2]);
            break;
        case ZipDirectoryArg:
            await host.Services.GetRequiredService<ZipDirectoryLoader>().Load(args[1], args[2], args[3]);
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

var executions = host.Services.GetRequiredService<MemoryStorage>().Get()
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
