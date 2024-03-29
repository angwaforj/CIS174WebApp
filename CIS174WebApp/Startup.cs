﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS174WebApp.Authorization;
using CIS174WebApp.Entity;
using CIS174WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CIS174WebApp
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
            //Register ApplicationDbContext with DI Container 
            services.AddDbContext<ApplicationDbContext>();
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddXmlSerializerFormatters();

            services.AddScoped<AuthorServices>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanManageAuthor",
                    policyBuilder =>policyBuilder
                        .AddRequirements(new IsAuthorOwnerRequirement()));

                options.AddPolicy(
                    "IsAdmin",
                    policyBuilder => policyBuilder.AddRequirements(
                        new MinimumAgeRequirement(18),
                        new IsAdminRequirement()));

                options.AddPolicy("ContentEditor",
                    policyBuilder =>policyBuilder
                        .AddRequirements(new ContentEditorRequirement()));
            });

            
            services.AddScoped<IAuthorizationHandler, IsAuthorOwnerHandler>();
            services.AddScoped<IAuthorizationHandler, DateOfBirthHandler>();
            services.AddScoped<IAuthorizationHandler, IsAdminHandler>();
            services.AddScoped<IAuthorizationHandler, ContentEditorHandler>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Author",
                    template: "Author/{action}/{id}",
                    defaults: new { controller = "Author", action = "Create" }
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
