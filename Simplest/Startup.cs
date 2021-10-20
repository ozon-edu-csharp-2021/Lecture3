using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simplest.Middlewares;

namespace Simplest
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // if (env.IsDevelopment())
      // {
      //   app.UseDeveloperExceptionPage();
      // }

      #region Middleware

      app.Use(async (http, next) =>
      {
        Console.WriteLine($"Path is {http.Request.Path}");

        await next();

        Console.WriteLine("Request processed");
        Console.WriteLine();
      });

      app.Use(async (http, next) =>
      {
        if (http.Request.Path == "/short-circuit")
        {
          // Терминальный миддлвар
          Console.WriteLine("/short-circuit");
          await http.Response.WriteAsync("This is the end");
      
          return;
        }
      
        await next();
      });
      //

      app.UseRequestCulture();
      

      #endregion

      #region MapWhen and UseWhen

      // Map and Use
      app.MapWhen(x => x.Request.Headers["version"] == "2", mapApp =>
      {
        mapApp.Use(async (http, next) =>
        {
          await http.Response.WriteAsync("From map middleware");
          await next();
        });
        
        app.Use(async (http, next) =>
        {
          Console.WriteLine("after internal");
          await next();
        });
      });
      
      app.UseWhen(x => x.Request.Headers["version"] == "3", useApp =>
      {
        useApp.Use(async (http, next) =>
        {
          await http.Response.WriteAsync("From use middleware");
          await next();
        });
      });
      
      app.Use(async (http, next) =>
      {
        Console.WriteLine("After Map and Use");
        await next();
      });

      #endregion

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGet("/", http => http.Response.WriteAsync("Hello World!"));
        endpoints.MapGet("/time", http => http.Response.WriteAsync(DateTime.Now.ToString("f")));
      });
    }
  }
}