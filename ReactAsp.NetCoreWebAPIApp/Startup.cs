using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReactAsp.NetCoreWebAPIApp.Business.ServicesExtensions;
using ReactAsp.NetCoreWebAPIApp.Core.Profiles;
using ReactAsp.NetCoreWebAPIApp.Data.BaseOperation;
using ReactAsp.NetCoreWebAPIApp.Data.EntityModels;
using System.Reflection;
using System.Text;

namespace ReactAsp.NetCoreWebAPIApp
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

            //services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
            //{
            //    builder.WithOrigins("http://localhost:3000")
            //           .AllowAnyMethod()
            //           .AllowAnyHeader();
            //}));

            services.AddDbContext<CoreWebAppDbContext>(x => x.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("ConStr")));


            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CoreWebAppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.AddAutoMapper(typeof(CustomerProfile));
            services.AddAutoMapper(typeof(PropertyProfile));

            services.AddMyLibraryServices();
            services.AddControllers()
       .AddFluentValidation(fv =>
       {
           fv.ImplicitlyValidateChildProperties = true;
           fv.ImplicitlyValidateRootCollectionElements = true;

           fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
       });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReactAsp.NetCoreWebAPIApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CoreWebAppDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
                context.Database.EnsureCreated();
                RelationalDatabaseCreator databaseCreator =
                 (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReactAsp.NetCoreWebAPIApp v1"));
            }

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
