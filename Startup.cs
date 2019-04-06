using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MathsExercise.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;

namespace MathsExercise {
    public class Startup {
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices (IServiceCollection services) {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,//validate the server
                ValidateAudience = true,//ensure that the recipient of the token is authorized to receive it 
                ValidateLifetime = true,//check that the token is not expired and that the signing key of the issuer is valid 
                ValidateIssuerSigningKey = true,//verify that the key used to sign the incoming token is part of a list of trusted keys
                ValidIssuer = Configuration["Jwt:Issuer"],//appsettings.json文件中定义的Issuer
                ValidAudience = Configuration["Jwt:Issuer"],//appsettings.json文件中定义的Audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };//appsettings.json文件中定义的JWT Key
            });
            // Console.WriteLine(Configuration.GetConnectionString("LocalConnection"));
            // services.AddDbContext<MEDBContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("LocalConnection")));
            
            services.AddMvc();

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles (configuration => {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseSpaStaticFiles ();
            app.UseAuthentication();   //enable authentication

            app.UseMvc (routes => {
                // routes.MapRoute (
                //     name: "default",
                //     template: "{controller}/{action=Index}/{id?}",
                //     defaults: new { Controller="ME" } ,
                //     constraints: new {  id = new CompositeRouteConstraint(new IRouteConstraint[] { 
                //             new IntRouteConstraint()})
                //             });
                routes.MapRoute (
                    name: "exercise",
                    template: "{controller}/{action}/{hashvalue}/{amount?}/{operation?}",
                    defaults: new { controller = "ME", action ="GetQuestion" } , 
                    constraints: new { hashvalue = new MaxLengthRouteConstraint(36) }
                );    
            });

            app.UseSpa (spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = new TimeSpan(0, 0, 20);
                    // spa.UseAngularCliServer(npmScript: "start");   //  .Net Core and Angular work at the same time
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");  // .Net Core and Angular work independently
                }
            });
        }
    }
}
