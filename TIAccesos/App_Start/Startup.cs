﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TIAccesos.App_Start.Startup))]

namespace TIAccesos.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(
             new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
             {
                 AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                 LoginPath = new PathString("/account/login")
             });
        }
    }
}
