using IEduZimAPI.CoreClasses;
using IEduZimAPI.CoreClasses.BaseFiles;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Repository;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IEduZimAPI
{
    public class Startup
    {
        public static IConfiguration configuration;
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("IEduPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.AddHttpClient<IHttpClientService, HttpClientService>();

            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            services.AddScoped<ILocationsRepository, LocationsRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ILessonStructureRepository, LessonStructureRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonLocationRepository, LessonLocationRepository>();
            services.AddScoped<ILessonScheduleRepository, LessonScheduleRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IHubRepository, HubRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped<IPaynowService, PaynowService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<ISearchLocationsService, SearchLocationsService>();
            services.AddScoped<IFileService, FileService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            configuration = Configuration;
            services.AddMvc().AddXmlSerializerFormatters();

            string connectionString = Configuration.GetConnectionString("Connection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDbContext<IEduContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddIdentity<IdentityUser, IdentityRole>()
                           .AddEntityFrameworkStores<IEduContext>()
                             .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IEduZimAPI", Version = "v1" });
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IISDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["Jwt:Iss"],
                    ValidAudience = Configuration["Jwt:Aud"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {

                        var accessToken = context.Request.Query["access_token"];
                        var authToken = context.Request.Headers["Authorization"].ToString();
                        var token = !string.IsNullOrEmpty(accessToken) ? accessToken.ToString() : !string.IsNullOrEmpty(authToken) ? authToken.Substring(7) : string.Empty;
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                            context.Request.Headers.Add("Authorization", "Bearer " + accessToken);
                        }
                        return Task.CompletedTask;
                    }
                };

            })
            .AddFacebook(options =>
            {
                options.AppId = Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                options.AccessDeniedPath = "/";
            });
            services.AddAuthorization(options =>
                 options.AddPolicy("ValidAccessToken", policy =>
                 {
                     policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                     policy.RequireAuthenticatedUser();
                 }));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IEduZimAPI v1");
                    c.DocExpansion(DocExpansion.None);
                });
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IEduZimAPI v1");
                c.DocExpansion(DocExpansion.None);
            });


            app.UseCors("IEduPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                                        Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = "/Files"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = "/Files"
            });

            var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            DatabaseService.Initialize(service.CreateScope().ServiceProvider);
            IdentityInitializer.SeedIdentityData(roleManager, userManager);
        }
    }
}
