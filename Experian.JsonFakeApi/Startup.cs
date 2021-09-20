using Experian.ApiServices.Interfaces.Services;
using Experian.ApiServices.Services;
using Experian.QueryServices.Interfaces.PhotoAlbum;
using Experian.QueryServices.PhotoAlbum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experian.JsonFakeApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHttpClient<IJsonFakeApiCallerService, JsonFakeApiCallerService>(cln =>
            {
                cln.BaseAddress = new Uri(Configuration["FakeApiConfiguration:ApiBaseUrl"]);
            });
            services.AddScoped<IPhotoAlbumQueryService, PhotoAlbumQueryService>();
            services.AddScoped<IPlaceHolderApiService, PlaceHolderApiService>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Experian.JsonFakeApi", Version = "v1" });
                c.IncludeXmlComments(GetXmlCommentsPath("Experian.JsonFakeApi.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Experian.JsonFakeApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static string GetXmlCommentsPath(string commentPath) => string.Format(commentPath, System.AppDomain.CurrentDomain.BaseDirectory);
    }
}
