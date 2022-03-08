using BeyonSense.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeyonSense
{
    class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();

            var appSettings = ConfigurationManager.AppSettings;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(appSettings["syncfusion"]);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<SplashViewModel>();
        }
    }
}
