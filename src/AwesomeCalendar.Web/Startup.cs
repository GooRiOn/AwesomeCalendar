using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AwesomeCalendar.Web.App_Start;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
