using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Simplest
{
  public class Program
  {
    public static void Main(string[] args)
    {
      IHostBuilder hostBuilder = Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          /* without Startup
          webBuilder.Configure(app =>
          {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
              endpoints.MapGet("/", http => http.Response.WriteAsync("Hello World!"));
              endpoints.MapGet("/time", http => http.Response.WriteAsync(DateTime.Now.ToString("f")));
            });
          });
          //*/
          
          //* With Startup
          webBuilder.UseStartup<Startup>();
          //*/
        });

      IHost host = hostBuilder.Build();

      host.Run();
    }
  }
}