using Microsoft.OpenApi.Models;
using WebAPI.Common;

namespace WebAPI.ServicesConfig
{
    public static class AddSwaggerGenServiceConfig
    {
        public static IServiceCollection AddSwaggerGenService(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                config =>
                {
                    config.SwaggerDoc(
                        "v1",
                        new OpenApiInfo { Title = "Sample Inventory API", Version = "v1" }
                    );
                    config.OperationFilter<AcceptLanguageHeaderFilter>();
                    config.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Please enter a valid token",
                            Name = "Authorization",
                            Type = SecuritySchemeType.Http,
                            BearerFormat = "JWT",
                            Scheme = "Bearer"
                        }
                    );
                    config.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {  }
                            }
                        }
                    );
                }
            );

            return services;
        }
    }
}
