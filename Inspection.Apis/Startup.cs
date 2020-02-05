using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Inspection.Data;
using Inspection.Data.Reposiotories;
using Inspection.Services;
using Inspection.Services.Mappings;
using InspectionCore.Reposiotories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using Z.EntityFramework.Extensions;

namespace Inspection.Apis
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        public IHostingEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperMappings).Assembly);
            services.AddDbContext<InspectionEfContext, InspectionContext>();
            services.AddDbContext<InspectionContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();

            EntityFrameworkManager.ContextFactory = context =>
            {
                return new InspectionContext((context as InspectionContext).RequestInfo);
            };
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gama Suite API", Version = "v1" });
                options.OperationFilter<TenantParameterOperationFilter>();

                #region Swagger
                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>() }
                    });
            });

            #endregion
                  #region JWT
            var key = Encoding.ASCII.GetBytes(Configuration["JwtKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion


            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                if (Environment.IsProduction())
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                }
            });

            

            services.AddScoped<IRequestInfo>(provider =>
            {
                var context = provider.GetRequiredService<IHttpContextAccessor>();
                var claims = context.HttpContext.User.Claims;

                return new RequestInfo(this.Configuration, int.TryParse(claims.FirstOrDefault(o => o.Type == "tenantId")?.Value, out int tenantId) ? (int?)tenantId : null);
            });
            services.AddScoped<RequestScope<InspectionContext>>(provider =>
            {
                var dbContext = provider.GetRequiredService<InspectionContext>();
                var scope = provider.GetRequiredService<RequestScope>();
                var userId = provider.GetRequiredService<IHttpContextAccessor>().HttpContext.User.FindFirst(x => x.Type == "UserId")?.Value;
                return new RequestScope<InspectionContext>(scope.ServiceProvider, dbContext, scope.Logger, scope.Mapper, userId, scope.TenantId);
            });
            services.AddScoped<RequestScope<InspectionEfContext>>(provider =>
            {
                var dbContext = provider.GetRequiredService<InspectionEfContext>();
                var scope = provider.GetRequiredService<RequestScope>();
                var userId = provider.GetRequiredService<IHttpContextAccessor>().HttpContext.User.FindFirst(x => x.Type == "UserId")?. Value;
                return new RequestScope<InspectionEfContext>(scope.ServiceProvider, dbContext, scope.Logger, scope.Mapper, userId, scope.TenantId);
            });
            services.AddScoped<RequestScope>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<Program>>();
                var context = provider.GetRequiredService<IHttpContextAccessor>();
                var claims = context.HttpContext.User.Claims;
                var userId = claims.FirstOrDefault(o => o.Type == "UserId")?.Value;
                var tenantId = int.TryParse(claims.FirstOrDefault(o => o.Type == "tenantId")?.Value, out int t) ? t : default(int?);
                if (!tenantId.HasValue)
                {
                    tenantId = int.TryParse(context.HttpContext.Request.Headers["tenantId"].SingleOrDefault(), out t) ? t : default(int?);
                }
                var mapper = provider.GetRequiredService<IMapper>();

                return new RequestScope(provider, logger, mapper, userId, tenantId);
            });
            ConfigureRepositories(services);
            ConfigureAppServices(services);
        }
        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFormBuilderTypeRepository, FormBuilderTypeRepository>();
            services.AddScoped<IFormBuilderRepository, FormBuilderRepository>();
            services.AddScoped<IFormBuilderQuestionsRepository, FormBuilderQuestionsRepository>();
            services.AddScoped<IFormBuilderQuestionsResponseRepository, FormBuilderQuestionsResponseRepository>();
        }
        private void ConfigureAppServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<UserIdGenerator>();

            services.AddScoped<IFormBuilderTypeServices, FormBuilderTypeServices>();

            services.AddScoped<IFormBuilderServices, FormBuilderServices>();
            services.AddScoped<IFormBuilderQuestionsServices, FormBuilderQuestionsServices>();
            services.AddScoped<IFormBuilderQuestionsResponseServices, FormBuilderQuestionsResponseServices>();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiddleware<InspectionExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inspection API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
    public class TenantParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "tenantId",
                In = ParameterLocation.Header,
                Description = "Tenant Id"
            });
        }
    }
}
