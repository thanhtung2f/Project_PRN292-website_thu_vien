﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectPRNver2._0.Startup))]
namespace ProjectPRNver2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
