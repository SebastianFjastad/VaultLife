using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vaultlife.Models;

namespace Vaultlife
{
    public partial class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);


            // Enable OAuth token based authentication
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromHours(4.0),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

           
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            ModelMetadataProviders.Current = new CustomModelMetadataProvider();

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");


            // Facebook
            const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
            var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions
           {
               // AppId and AppSecret now stored per instance in web.config.
               AppId = ConfigurationManager.AppSettings["fbAppId"],
               AppSecret = ConfigurationManager.AppSettings["fbAppSecret"],
               Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider
               {
                   OnAuthenticated = (context) =>
                   {
                       context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
                       foreach (var x in context.User)
                       {
                           var claimType = string.Format("urn:facebook:{0}", x.Key);
                           string claimValue = x.Value.ToString();
                           if (!context.Identity.HasClaim(claimType, claimValue))
                               context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, XmlSchemaString, "Facebook"));

                       }
                       return Task.FromResult(0);
                   }
               }
           };

            //Way to specify additional scopes
            facebookOptions.Scope.Add("email");
           


            app.UseFacebookAuthentication(facebookOptions);
         
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}