using Aplicacion.Mappers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Dominio.Exceptions;
using Dominio.Models.Regla;
using FluentMigrator.Runner;
using Infraestructura.Data;
using Infraestructura.Filters;
using Infraestructura.Migrations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebApi.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHandlers();
            services.AddContextConfiguration(Configuration);
            services.AddScoped<UnitOfWordFilter>();
            services.AddAplicacionServices();
            services.AddTokenConfiguration(Configuration);
            services.AddHttpContextAccessor();
            services.AddRedis(Configuration);
            services.AddCorsConfig();
            services.AddSwaggerConf();
            services.AddAutoMapper(typeof(CatalogoToDtoCatalogo));
            services.AddMail();
            services.AddPermisos();
            services.AddSefinClient();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.Scan(scan => scan.FromAssemblyOf<ReciboGestionado>().AddClasses(classes => classes.AssignableTo(typeof(IRegla))).AsImplementedInterfaces().WithTransientLifetime());
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(UnitOfWordFilter));
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(Configuration.GetConnectionString("DefaultConnection"))
                .ScanIn(typeof(FirstMigrations).Assembly).For.Migrations());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpException();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/alpha/swagger.json", "Documentacion Sistema de Pagos");
            });
            app.UseCors("ApiCorsPolicy");
            app.UseMvc();
            using var scope = app.ApplicationServices.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<RecibosContext>();
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            // runner.Rollback(1);
            // runner.MigrateUp();
        }
    }
}
