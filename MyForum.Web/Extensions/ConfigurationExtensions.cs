namespace MyForum.Web.Extensions
{
    using Microsoft.Extensions.Configuration;
    using MyForum.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
            => configuration.GetConnectionString(GlobalConstants.ConnectionName);
    }
}
