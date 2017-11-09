#if NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetCorePal.WebStorage;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebStorageServiceCollectionExtensions
    {
        public static void AddLocalWebStorage(this IServiceCollection services)
        {
            services.AddSingleton(typeof(WebStorageProvider), typeof(LocalWebStorageProvider));
        }
    }
}
#endif