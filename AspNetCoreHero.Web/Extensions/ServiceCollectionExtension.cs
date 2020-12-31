using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace AspNetCoreHero.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddMultiLingualSupport(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> cultures = new List<CultureInfo>();
                cultures.Add(new CultureInfo("en"));
                cultures.Add(new CultureInfo("ar"));
                cultures.Add(new CultureInfo("fr"));

                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
        }
    }
}
