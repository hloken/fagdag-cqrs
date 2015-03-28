﻿using System.Web.Cors;
using Microsoft.Owin.Cors;
using Owin;

namespace RestApi.Bootstrapping
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(new CorsOptions { CorsEngine = new CorsEngine(), PolicyProvider = new FagdagCqrsCorsProvider() });
            app.UseNancy();
        }
    }
}