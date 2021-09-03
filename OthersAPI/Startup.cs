using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using OthersAPI.Reporitories;
using OthersAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Repositories;

namespace OthersAPI
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", config => {
                  config.Authority = "http://localhost:5000/";

                  config.Audience = "OthersApi";

                  config.RequireHttpsMetadata = false;
              });
            services.AddCors(confg =>
               confg.AddPolicy("JustGet",
                   p => p.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()));

            services.AddControllers();
            var mongoDBSettings = Configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
            services.AddSingleton<IMongoClient>(serviveProvider =>
            {
                
                return new MongoClient(mongoDBSettings.ConnectionString);
            });
            services.AddSingleton<IWeatherPrefRepository, WeatherPreferenceMongoDbRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OthersAPI", Version = "v1" });
            });

            services.AddHealthChecks()
                .AddMongoDb(mongoDBSettings.ConnectionString, name:"mongodb",
                timeout: TimeSpan.FromSeconds(3), tags: new[] { "ready" });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OthersAPI v1"));
            }

            app.UseCors("JustGet");

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = (chk) => chk.Tags.Contains("ready"),
                    ResponseWriter = async(context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            new
                            {
                                status = report.Status.ToString(),
                                checks = report.Entries.Select(e => new { 
                                    name = e.Key,
                                    status = e.Value.Status.ToString(),
                                    exception = e.Value.Exception != null ? e.Value.Exception.Message : "none",
                                    duration = e.Value.Duration.ToString()
                                })

                            }
                         );

                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    } 
                });
            });
        }
    }
}
