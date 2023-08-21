using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using TravelNinjaz.B2B.WebAPI.Models.Data;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using TravelNinjaz.B2B.WebAPI.Models.Entity.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravelNinjaz.B2B.WebAPI.Configuration;
using TravelNinjaz.B2B.WebAPI.Models.EmailService;
using ClientDemo.WebAPI.Models.Repository;

namespace TravelNinjaz.B2B.WebAPI
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

            // For Entity Framework  
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            // For Identity  
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            var webSection = Configuration.GetSection("WebSettings");
            services.Configure<WebSettings>(webSection);

            var smtpSection = Configuration.GetSection("SmtpSettings");
            services.Configure<SmtpSettings>(smtpSection);
            services.AddTransient<IMailService, MailService>();

            var jwtSection = Configuration.GetSection("JwtBearerTokenSettings");
            services.Configure<JwtBearerTokenSettings>(jwtSection);


            var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();


            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtBearerTokenSettings.Issuer,
                    ValidAudience = jwtBearerTokenSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey)),
                };
            });

            // Adding Jwt Bearer  
            //.AddJwtBearer(options =>
            //{
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = Configuration["JwtBearerTokenSettings:ValidAudience"],
            //        ValidIssuer = Configuration["JwtBearerTokenSettings:ValidIssuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
            //    };
            //});

            services.AddCors(
                  options => options.AddDefaultPolicy(builder =>
                  {
                      builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                  })
                 );
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            // Add all repository here
            services.AddScoped<UserRepository>();
            services.AddScoped<DestinationRepository>();
            services.AddScoped<CityRepository>();
            services.AddScoped<HotelRepository>();
            services.AddScoped<MealPlanRepository>();
            services.AddScoped<HotelMealPlanRepository>();
            services.AddScoped<TransportRepository>();
            services.AddScoped<TransportRateRepository>();
            services.AddScoped<PackageRepository>();
            services.AddScoped<CitySiteSeeingRepository>();
            services.AddScoped<DestinationPickupAndDropRepository>();
            services.AddScoped<PackageQuotationRepository>();
            services.AddScoped<PackageQuotationUserFavoriteRepository>();
            services.AddScoped<TravellingCompanyRepository>();
            services.AddScoped<ProcessLogsRepository>();
            services.AddScoped<DashboardRepository>();
            services.AddScoped<PackageReviewsRepository>();
            services.AddScoped<OrganizationRepository>();
            //services.AddScoped<Load_dataRepository>();


            services.AddScoped<MailService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            /** 
             * Name:Sunil Kumar Bais
             * Date:31/10/2022
             * Note:- Add app.UseCors()
             */
            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });
        }
    }
}
