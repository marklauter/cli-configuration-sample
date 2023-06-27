using CLI.Config.Sample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/* 
sample calls can look like this:

./CLI.Config.Sample.exe --SampleOptions:Name Hello --SampleOptions:Value World
./CLI.Config.Sample.exe --SampleOptions:Name="Hello" --SampleOptions:Value="World"
./CLI.Config.Sample.exe --SampleOptions:Name sample --SampleOptions:Value="Hello, World."

notes:
  1. arguments with spaces must be quoted
  2. quoted arguments must include the equal sign
  3. command line arguments will override values in appsettings.json and environment variables

The command line args are parsed by Microsoft.Extensions.Configuration.CommandLine package.
Use IOptions<TCLIArgsClass> to inject them into your services.
You can populate as many classes from the command line as you want.
The command line format is [ClassName]:[PropertyName]=[Value].

In this sample the IOptionsWriter.WriteOptions method will write the options instance,
populated by the CLI args, to the console as JSON.
 */

using var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((builderContext, services) =>
        _ = services
            .Configure<SampleOptions>(builderContext.Configuration.GetSection(nameof(SampleOptions)))
            .AddOptionsWriter())
    .Build();

host.Services
    .GetRequiredService<IOptionsWriter>()
    .WriteOptions();

