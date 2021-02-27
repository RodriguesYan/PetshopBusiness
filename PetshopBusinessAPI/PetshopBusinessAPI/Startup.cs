using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetshopBusinessAPI.Filters;
using PetshopDB.Models;

namespace PetshopBusinessAPI
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
            services.AddCors();
            services.AddControllers();

            services.AddDbContext<PetshopDbContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString(@"Data Source=.\SQLEXPRESS;Initial Catalog=MyOtherProject;User ID=Rodrigues;Password=@Puta00001")));
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection"), b => b.MigrationsAssembly("PetshopBusinessAPI")));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ErrorResponseFilter));
            });

            //Desabilita a validação automatica do ModelState. Bom para tratar erros e exibir no response
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            //Informa o .net que estou usando autenticação
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    //ValidateLifeTime = true,
                    ValidateIssuer = false,//"PetshotBusinessAPI"
                    ValidateAudience = false //"Postman
                    //ClockSkew = TimeSpan.FromMinutes(5) => tempo de validade do token
                };
            });

            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                //Para fazer a v2, só copiar e por
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Petshop API",
                    Description = "API Documentation",
                    Version = "1.0"
                });

                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "Autenticação Bearer via JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            //Scheme = "Bearer",
                            //Name = "Bearer",
                            //In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                c.OperationFilter<AuthResponsesOperationFilter>();

                c.DocumentFilter<TagDescriptionsDocumentFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1.0");
                //c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            });
        }
    }
}
