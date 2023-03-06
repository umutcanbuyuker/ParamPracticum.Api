using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ParamApi;
using ParamPracticum.Base.Jwt;
using ParamPracticum.Data.Models;
using ParamPracticum.Data.Repository.Abstract;
using ParamPracticum.Data.Repository.Concrete;
using ParamPracticum.Data.Uow;
using ParamPracticum.Service.Abstract;
using ParamPracticum.Service.Concrete;
using ParamPracticum.Service.Mapper;

namespace ParamPracticum.Api.Extensions
{
    public static class DIExtension
    {
        public static JwtConfig JwtConfig { get; private set; }
        public static void AddServiceDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGenericRepository<Account>,GenericRepository<Account>>();
            services.AddScoped<IGenericRepository<Person>,GenericRepository<Person>>();

            services.AddScoped<JwtConfig>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAccountService, AccounService>();
            services.AddScoped<ITokenManagementService, TokenManagementService>();  

            JwtConfig = configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));



            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
        public static void AddCustomizeSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Param Api Management", Version = "v1.0" });
                c.OperationFilter<ExtensionSwaggerFileOperationFilter>();

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Techa Management for IT Company",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // Must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });
        }
    }
}
