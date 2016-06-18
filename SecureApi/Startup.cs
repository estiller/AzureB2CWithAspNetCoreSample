using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SecureApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = TenantConfig.ClientId,
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                MetadataAddress = $"https://login.microsoftonline.com/{TenantConfig.Tenant}/v2.0/.well-known/openid-configuration?p={TenantConfig.PolicyId}",
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseMvc();
        }
    }
}
