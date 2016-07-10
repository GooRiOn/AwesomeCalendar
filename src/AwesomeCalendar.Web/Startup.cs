using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeCalendar.Web.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AwesomeCalendar.Web.Startup))]

namespace AwesomeCalendar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AutofacConfig.Register(app);
        }
    }
}
