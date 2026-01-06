using System.Configuration;
using System.Data;
using System.Windows;
using System.Xaml;
using Microsoft.Extensions.Configuration;

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();

            string connectionstring = configuration.GetConnectionString("SQLserver");

            ServiceProvider = new ServiceProvider(connectionstring);
        }
    }

}
