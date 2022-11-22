using Microsoft.Extensions.DependencyInjection;
using WildcardSearcher.Common.Interfaces;
using WildcardSearcher.Services;

namespace WildcardSearcher.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterWildcardSearcher(this IServiceCollection services)
        {
            services.AddSingleton<IWildcardSearcher, LuceneWildcardSearcher>();
            return services;
        }
    }
}
