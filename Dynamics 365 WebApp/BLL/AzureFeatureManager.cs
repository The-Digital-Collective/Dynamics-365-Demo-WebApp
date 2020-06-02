using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using System;

namespace Dynamics_365_WebApp.BLL
{
    public class AzureFeatureManager
    {
        private string _azureConnectionString;
        private string _featureFlag;
        private bool   _featureSwitch = false;

        public AzureFeatureManager(string featureFlag)
        {
            _azureConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AzureFeatureSwitch"].ConnectionString;
            _featureFlag = featureFlag;
        }

        public async System.Threading.Tasks.Task<bool> GetAzureFeatureFlag()
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddAzureAppConfiguration(options =>
                    {
                        options.Connect(_azureConnectionString)
                        .UseFeatureFlags();
                    }).Build();

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration).AddFeatureManagement();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IFeatureManager featureManager = serviceProvider.GetRequiredService<IFeatureManager>();

            if (await featureManager.IsEnabledAsync(_featureFlag))
                _featureSwitch = true;

            return _featureSwitch;
        }
    }
}