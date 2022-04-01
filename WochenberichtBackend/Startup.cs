using DataAccessEfCore.Repositories.InstructorRepository;
using DataAccessEfCore.Repositories.WeeklyReportRepository;
using DataAccessEfCore.UnitOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Text;
using DataAccessEfCore.DataContext;



namespace WochenberichtManagement
{
    public class Startup
    {
        #region Variables
        private readonly string _loginOrigin = "_localorigin";
        public IConfiguration Configuration { get; }
        #endregion

        #region ctor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion
        


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

            services.AddDbContext<WochenberichtDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(WochenberichtDBContext).Assembly.FullName));

            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JWTConfig:Key"]);
                var issuer = Configuration["JWTConfig:Issuer"];
                var audience = Configuration["JWTConfig:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };


            });

            services.AddCors(opt =>
            {
                opt.AddPolicy(_loginOrigin, builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    
                    
     
     
                });
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // check difference to add singleton



        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();//serve the static files like js, CSS, images, etc

            app.UseHttpsRedirection();

            app.UseCors(_loginOrigin);
            
            app.UseRouting();
            

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllers();
           });

        }
    }

    
}

//dotnet ef migrations add InitialCreate -p .\ImportExportGenerator.Data -c WochenberichtDbContext -o Migrations\WochenberichtDbMigrations
//dotnet ef database update -p .\ImportExportGenerator.Data -c WochenberichtDbContext
//add-migrations repoPattern - c WochenberichtDbContext -o Data\Migrations\WochenberichtDbMigrations
//dotnet ef migrations add repoPattern -p Data.csproj -s WochenberichtWebApp.csproj -c Data\WochenberichtDBContext -o Data\Migrations\WochenberichtDbMigrations

//EntityFrameworkCore\Add-Migration new
// EntityFrameworkCore\update-database