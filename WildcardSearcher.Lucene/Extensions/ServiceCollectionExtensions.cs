using Microsoft.Extensions.DependencyInjection;
using WildcardSearcher.Common.Interfaces;
using WildcardSearcher.Lucene.Services;

namespace WildcardSearcher.Lucene.Extensions
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
