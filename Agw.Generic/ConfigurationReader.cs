using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AU.CodeFirst.Autocad
{
    /// <summary>
    /// From http://blog.rodhowarth.com/2009/07/how-to-use-appconfig-file-in-dll-plugin.html
    /// </summary>
    public class ConfigurationReader
    {
        /// <summary>
        /// Get the configuration for the supplied type
        /// </summary>
        /// <param name="type">type of class asking - to get right assembly</param>
        /// <returns></returns>
        public static Configuration GetConfig(Type type)
        {
            //workout app.config location
            string dllLocation = type.Assembly.Location + ".config";
            if (dllLocation == null)
                throw new Exception("Could not find config file, add .config in DLL location");
            //create config
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = dllLocation;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(
                fileMap, ConfigurationUserLevel.None);
            return config;
        }

        /// <summary>
        /// Gets a specific config property
        /// </summary>
        /// <param name="key">the property to get</param>
        /// <param name="type">type of class asking - to get right assembly</param>
        /// <returns></returns>
        public static string GetConfigProperty(string key, Type type)
        {
            Configuration config = GetConfig(type);
            return config.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// Gets a connectionString property
        /// </summary>
        /// <param name="connectionName">the connection to get</param>
        /// <param name="type">type of class asking - to get right assembly</param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionName, Type type)
        {
            var config = GetConfig(type);
            return config.ConnectionStrings.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}
