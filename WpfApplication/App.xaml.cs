using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Composition;
using ViewModel.Windows;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void On_Startup(object sender, StartupEventArgs e)
        {
            NameValueCollection plugins = (NameValueCollection)ConfigurationManager.GetSection("dirpaths");
            string[] pluginsCatalogs = plugins.AllKeys;
            foreach (string pluginsCatalog in pluginsCatalogs)
            {
                if (Directory.Exists(pluginsCatalog))
                    Compose.Instance.AddCatalog(new DirectoryCatalog(pluginsCatalog));
            }
            Compose.Instance.CreateCompositionContainer();
        }

    }

}
