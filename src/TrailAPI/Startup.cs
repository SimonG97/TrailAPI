using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrailAPI.Data;
using TrailAPI.Respository;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Newtonsoft.Json.Serialization;

namespace TrailAPI
{
    public class Startup
    {
        //Implementing IConfiguration interface to gain access of appsettings.json
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //registering the command repository
            services.AddScoped<ICommandRepo,PostgresApiRepo>();
            
            //registering the connection string
            var builder= new NpgsqlConnectionStringBuilder();
            builder.ConnectionString=Configuration.GetConnectionString("DefaultConnection");
            builder.Username= Configuration["UserID"];
            builder.Password= Configuration["password"];
            services.AddDbContext<DBContext>(options=>options.UseNpgsql(builder.ConnectionString));

            //registering the automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //registering the NewtonSoftJson
            services.AddControllers().AddNewtonsoftJson(s =>{
                s.SerializerSettings.ContractResolver=new CamelCasePropertyNamesContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
