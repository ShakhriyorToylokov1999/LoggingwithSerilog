using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Net;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Email(
                    fromEmail: "mySample@example.com",
                    toEmail: "recieverSample@example.com",
                    mailServer: "smtp.example.com",
                    networkCredentials: new NetworkCredential("username", "password"),
                    outputTemplate: "log.txt")
                    .CreateLogger();

            try
            {
                Log.Information("Starting web application");
                CreateHostBuilder(args).Build().Run();
               
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
