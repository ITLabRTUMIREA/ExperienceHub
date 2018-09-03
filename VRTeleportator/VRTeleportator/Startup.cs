using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using VRTeleportator.Models;

namespace VRTeleportator
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
            services.AddMvc();

            services.AddDbContext<AppDataBase>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("VRTeleportator"),
                b => b.MigrationsAssembly("VRTeleportator")));

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDataBase>()
                .AddDefaultTokenProviders();

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JwtOptions")["Key"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration.GetSection("JwtOptions")["Issuer"],

                            ValidateAudience = true,
                            ValidAudience = Configuration.GetSection("JwtOptions")["Audience"],

                            ValidateLifetime = true,

                            IssuerSigningKey = key,

                            ValidateIssuerSigningKey = true
                            
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });
            app.UseMvc();
        }
    }
}
