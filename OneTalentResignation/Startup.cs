using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OneTalentResignation.BLL.Repository;
using OneTalentResignation.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PolicyServer;
using System;
using OneTalentResignation.BLL.Shared;
using Microsoft.AspNetCore.Http;




namespace OneTalentResignation
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
            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
            {
                options.Authority = Configuration["EnvironmentVariables:Authority"].ToString();
                options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["EnvironmentVariables:RequireHttpsMetadata"].ToString());
                options.ApiName = Configuration["EnvironmentVariables:ApiName"].ToString();
                options.ApiSecret = Configuration["EnvironmentVariables:ApiSecret"].ToString();
            });

            services.AddPolicyServerClient(options =>
            {
                options.Provider = Configuration["EnvironmentVariables:Provider"].ToString();
                options.ApplicationName = Configuration["EnvironmentVariables:ApplicationName"].ToString();
                options.PolicyName = Configuration["EnvironmentVariables:PolicyName"].ToString();

                options.ClientId = "PolicyServerClient";
                options.Authority = Configuration["EnvironmentVariables:Authority"].ToString();
                options.ClientSecret = Configuration["EnvironmentVariables:ApiSecret"].ToString();
                options.ApiName = Configuration["EnvironmentVariables:PolicyServerApiName"].ToString();
            }).AddAuthorizationPermissionPolicies();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IClaims, Claims>();
            services.AddCors();
            services.AddServices(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Log/1Talent-Logs.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors(builder =>
                builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
            );

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
