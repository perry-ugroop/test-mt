using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace Common
{
    public static class Utils
    {
        public static IConfig GetConfiguration(string section)
        {
          var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       ;

           var configObj = builder.Build(); 

           return new ConcreteConfig(configObj, section);
        }
    }

    class ConcreteConfig : IConfig
    {
        private IConfigurationRoot config;
        private string sectionName;

        public ConcreteConfig(IConfigurationRoot config, string sectionName)
        {
           this.config = config;
           this.sectionName = sectionName.Trim();
        }

        public string Get(string keyName)
        {
           return config.GetSection($"{this.sectionName}:{keyName.Trim()}").Value;
        }

        public int GetInt(string keyName)
        {
            return int.Parse(Get(keyName));
        }
    }
}