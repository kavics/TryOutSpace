using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MidWarez.ClassLibrary1;

namespace MidWarez.Webapp
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseResponseManipulator();
            app.UseMvc();


            ////----------------------- Response manipulation demo

            //app.Use(async (context, next) =>
            //{
            //    var newContent = string.Empty;
            //    var originalBody = context.Response.Body;
            //    using (var newBody = new MemoryStream())
            //    {
            //        context.Response.Body = newBody;
            //        await next();
            //        context.Response.Body = originalBody;

            //        newBody.Seek(0, SeekOrigin.Begin);
            //        newContent = new StreamReader(newBody).ReadToEnd();
            //        newContent += ", World!";

            //        await context.Response.WriteAsync(newContent);
            //    }
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello");
            //});
        }
    }
}
