using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Contracts.Initializer;
using System;

namespace Portfolio.Identity
{
    public static class IdentityApplicationBuilderExtension
    {

        public static void SeeddRole(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IIdentityDbInitializer>();
                dbInitializer.Initialize();
                dbInitializer.SeedRoles();
            }
        }
    }
}
