using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Events;

namespace FnbPrintingMiddleware
{

    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                                                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                                  .Enrich.FromLogContext()
                                                  .WriteTo.Console()
                                                  .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
                                                  .WriteTo.File(@"C:\Logs\Payment_Middleware_.txt", rollingInterval: RollingInterval.Day)
                                                  .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder => {
                    var pathToContentRoot = Directory.GetCurrentDirectory();

                    var webHostArgs = args.Where(arg => arg != "--console").ToArray();
                    var ConfigurationFile = "appsettings.json";
                    var PortNo = "5001";

                    var pathToExe = Environment.ProcessPath;
                    pathToContentRoot = Path.GetDirectoryName(pathToExe);

                    var AppJsonFilePath = Path.Combine(pathToContentRoot, ConfigurationFile);
                    var CurrentIPAddress = "localhost";

                    if (File.Exists(AppJsonFilePath))
                    {
                        using (StreamReader sr = new StreamReader(AppJsonFilePath))
                        {
                            string jsonData = sr.ReadToEnd();
                            JObject jObject = JObject.Parse(jsonData);
                            if (jObject["ServicePort"] != null)
                            {
                                PortNo = jObject["ServicePort"].ToString();
                                CurrentIPAddress = jObject["IPAddress"].ToString();
                            }
                        }
                    }
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("http://" + CurrentIPAddress + ":" + PortNo);
                });
    }
}