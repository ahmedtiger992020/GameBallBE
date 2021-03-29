using API.Controllers;
using Autofac;
using Core;
using GB.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Intefaces;
using Infrastructure.Context;
using Infrastructure.Mapping;
using Infrastructure.UnitOfWork;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string AllowedOrigins { get; } = "AllowedOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddControllersAsServices().AddNewtonsoftJson();
            
            IConfigurationSection originsSection = Configuration.GetSection(AllowedOrigins);
            string[] origns = originsSection.AsEnumerable().Where(s => s.Value != null).Select(a => a.Value).ToArray();
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });

                c.DescribeAllParametersInCamelCase();
            });
            #endregion
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins,
                    builder =>
                    {
                        builder.WithOrigins(origns)
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });
            });
            services.AddSignalR();
            #region AutoMapper
            services.AddAutoMapper(typeof(BookMapping).Assembly);
            #endregion

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region Register DB Context
            builder.Register(c =>
            {
                return new GBSampleContext(new DbContextOptionsBuilder<GBSampleContext>().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options);
            }).InstancePerLifetimeScope();
            #endregion

            #region Register Unit Of Work
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            #endregion

            #region Register The Genric Output Port
            //builder.RegisterGeneric(typeof(IOutputPort<>)).PropertiesAutowired();
            builder.RegisterGeneric(typeof(OutputPort<>)).As(typeof(IOutputPort<>)).PropertiesAutowired();
            #endregion

            #region Register HttpContextAccessor In Order To Access The Http Context Inside A Class Library (Core Project)
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            #endregion



            #region Register All Repositories & UseCases
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).PublicOnly().Where(t => t.IsClass && t.Name.ToLower().EndsWith("usecase")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).PublicOnly().Where(t => t.IsClass && t.Name.ToLower().EndsWith("repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            #endregion

            #region Register Controller For Property DI
            Type controllerBaseType = typeof(BookStoreController);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType);
            #endregion

            #region Register SignalR

            builder.Register<HubConnection>(c =>
            {

                HubConnection hubConnection = new HubConnectionBuilder().WithUrl(Configuration.GetSection("SignalRServerSettings:HubURL").Value, config =>
                {
                    if (bool.TryParse(Configuration.GetSection("SignalRServerSettings:IgnoreSSLCertificate").Value, out bool ignoreSSL) && ignoreSSL)
                    {
                        config.HttpMessageHandlerFactory = (x) => new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                        };
                    }
                }).Build();
                hubConnection.StartAsync().Wait();
                return hubConnection;
            }).SingleInstance();
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GBSampleContext>();
                context.Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region CORS
            app.UseCors(AllowedOrigins);
            #endregion

            #region AppBuilder
            app.UseAppMiddleware();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            #region Swagger
            IConfigurationSection SwaggerBasePath = Configuration.GetSection("SwaggerBasePath");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{ SwaggerBasePath.Value}/swagger/v1/swagger.json", "Sample Api V1");
                c.RoutePrefix = string.Empty;
            });
            #endregion

            #region EndPoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });
            #endregion

            #region StaticFiles
            app.UseStaticFiles();
            #endregion
        }
    }
}
