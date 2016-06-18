# AzureB2CWithAspNetCoreSample
This sample shows how to secure an ASP.NET Core Web API using Azure AD B2C.

## How To Run This Sample

To run this sample you will need:
- Visual Studio 2015
- ASP.NET Core SDK
- An Internet connection
- An Azure AD B2C tenant

Steps to follow:

1. If you don't have an Azure AD B2C tenant, you can follow [those instructions](https://azure.microsoft.com/en-us/documentation/articles/active-directory-b2c-get-started/) to create one. 
2. If you don't have a "Sign Up or Sign In" Policy, then create one as described [here](https://azure.microsoft.com/en-us/documentation/articles/active-directory-b2c-reference-policies/#how-to-create-a-sign-up-policy).
3. Configure the Tenant, Client Id & Policy Id in the "TenantConfig.cs" file.

## Key Takeaway
The key component for securing the API is the following code:

    public void Configure(IApplicationBuilder app)
    {
      var tokenValidationParameters = new TokenValidationParameters
      {
        // Configure the Web API to accept tokens requested only for it
        ValidAudience = TenantConfig.ClientId,
      };

      app.UseJwtBearerAuthentication(new JwtBearerOptions
      {
          // Configure the Discoery Document URL for configuring JWT settings
          MetadataAddress = $"https://login.microsoftonline.com/{TenantConfig.Tenant}/v2.0/.well-known/openid-configuration?p={TenantConfig.PolicyId}",
          TokenValidationParameters = tokenValidationParameters
      });

      app.UseMvc();
    }  

