﻿using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace WebAPI.ServicesConfig
{
    public static class AddLocalizationServiceConfig
    {
        public static IServiceCollection AddLocalizationService(this IServiceCollection services)
        {
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("my-MM")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });

            return services;
        }
    }
}
