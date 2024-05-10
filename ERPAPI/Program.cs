
using ERPAPI.Entity.DBContext;
using ERPAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog.Formatting.Json;

namespace ERPAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ErpDBCon")));

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            Log.Logger = new LoggerConfiguration()
                        // add console as logging target
                        .WriteTo.Console()
                        // add a logging target for warnings and higher severity logs structured in JSON format
                        .WriteTo.File(new JsonFormatter(),
                                      $"important-{DateTime.Now.ToString("dd-MM-yyyy")}.json",
                                      restrictedToMinimumLevel: LogEventLevel.Warning)
                        // add a rolling file for all logs with date in the file name
                        .WriteTo.File($"applicationLog-{DateTime.Now.ToString("dd-MM-yyyy")}.logs",
                                      rollingInterval: RollingInterval.Day,
                                      rollOnFileSizeLimit: true,
                                      retainedFileCountLimit: null)
                        // set default minimum level
                        .MinimumLevel.Debug()
                        .CreateLogger();
            //Log.Logger = new LoggerConfiguration()
            //        // add console as logging target
            //        .WriteTo.Console()
            //        // add a logging target for warnings and higher severity logs structured in JSON format
            //        .WriteTo.File(new JsonFormatter(),
            //                      Path.Combine(logFolderPath, $"important-{DateTime.Now.ToString("dd-MM-yyyy")}.json"),
            //                      restrictedToMinimumLevel: LogEventLevel.Warning)
            //        // add a rolling file for all logs with date in the file name
            //        .WriteTo.File(Path.Combine(logFolderPath, $"log-{DateTime.Now.ToString("dd-MM-yyyy")}.logs"),
            //                      rollingInterval: RollingInterval.Day,
            //                      rollOnFileSizeLimit: true,
            //                      retainedFileCountLimit: null)
            //        // set default minimum level
            //        .MinimumLevel.Debug()
            //        .CreateLogger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
