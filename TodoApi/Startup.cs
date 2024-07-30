﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;
using Microsoft.Extensions.Logging;

namespace TodoApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
                opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("CanAccess", new ApiKeyScheme
                {
                    Type = "apiKey",
                    In = "header",
                    Name = "CanAccess"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "CanAccess", new string[] {} }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Todo API", Version = "v1" });
            });
            services.AddLogging(config =>
        {
            config.AddConsole();
            config.AddDebug();
            config.SetMinimumLevel(LogLevel.Information);
        });
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
            });
            app.UseMvc();
            logger.LogInformation("Application started.");
        }
    }
}