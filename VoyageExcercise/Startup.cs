using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VoyageExcercise.DAL;
using VoyageExcercise.Helpers;
using VoyageExcercise.Interfaces;

namespace VoyageExcercise
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            ConfigureDatabaseServices(services);
            ConfigureDefaultServices(services);
            services.AddControllers();
            
        }

        protected void ConfigureDefaultServices(IServiceCollection services)
        {
            //dependency injection
            services.AddTransient<IServices, ServicesHelper>();
            services.AddTransient<IInvoice, InvoiceHelper>();
            services.AddSwaggerGen(c => {
                //set Api Info
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = ".net Core Voyage API",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Rodrigo Hernandez",
                            Email = "rodrigo.hernandez@arkusnexus.com",
                            Url = new Uri("https://www.linkedin.com/in/rodrigo-hern%C3%A1ndez-morales-61b936aa/"),
                        },
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // We have to override this method in our TestStartup, because we want to inject our custom database services
        protected virtual void ConfigureDatabaseServices(IServiceCollection services)
        {
            //configure sqlite db context
            services.AddDbContext<AppDBContext>(opts => opts.UseSqlite(
                Configuration.GetConnectionString("cs"),
                builder => builder.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
                ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });

                //swagger, swagger-ui initialize
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Net Core VOYAGE API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
