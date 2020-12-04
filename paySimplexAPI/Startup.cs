using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using paySimplexBusiness.Business;
using paySimplexBusiness.Contracts;
using paySimplexData.Context;
using paySimplexData.Contracts;
using paySimplexData.Repository;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace paySimplexAPI
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
            services.AddControllers();

            #region Compatibility

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                });

            #endregion

            #region Injections

            services.AddTransient<IUserContract, UserBusiness>();
            services.AddTransient<ITaskContract, TaskBusiness>();
            services.AddTransient<IStateContract, StateBusiness>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IStateRepository, StateRepository>();

            #endregion

            #region SQL

            services.AddDbContext<paySimplexContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"))
                                                                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            #endregion

            #region swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webapi api.paysimplex", Version = "v1", Description = "webapi api.paysimplex" });
                c.ResolveConflictingActions(apidescriptions => apidescriptions.First());

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                c.IncludeXmlComments(xmlpath);
            });

            #endregion

            #region json
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options => 
                                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration.GetValue<string>("SwaggerEndpoint"), "WebApi paySimplex.");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "WebApi paySimplex documentation";
                c.DocExpansion(DocExpansion.None);
            });

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
