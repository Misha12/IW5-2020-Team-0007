using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MovieDatabase.BL.Web.Facades;
using MovieDatabase.Web;
using System.Net.Http;

namespace MovieDatabase.BL.Web.Installer
{
    public class BLWebInstaller
    {
        public void Installer(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<HttpClient>();
            serviceCollection.AddTransient<UserFacade>();
            serviceCollection.AddScoped<IUsersControllerClient, UsersControllerClient>();
            serviceCollection.AddScoped<IClient, Client>();
        }
    }
}
