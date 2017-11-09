#if NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetCorePal.WebStorage;
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// IServiceCollection扩展
    /// </summary>
    public static class WebStorageServiceCollectionExtensions
    {
        /// <summary>
        /// 注册LocalWebStorageProvider到IServiceCollection，单例模式
        /// </summary>
        /// <param name="services"></param>
        public static void AddLocalWebStorage(this IServiceCollection services)
        {
            services.AddSingleton(typeof(WebStorageProvider), typeof(LocalWebStorageProvider));
        }
    }
}
#endif