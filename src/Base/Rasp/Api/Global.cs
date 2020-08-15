using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fss.Rasp
{
    public static class Global
    {
        #region Constructor
        static Global()
        {
            ActivityDetectorAssemblies = Assembly.GetExecutingAssembly().LoadAllFrom("Fss.ActivityDetectors.*.dll") ??
                throw new Exception("No activity detector assemblies found in directory: " + AssemblyDirectory.FullName + ".");

            MonitorAssemblies = Assembly.GetExecutingAssembly().LoadAllFrom("Fss.Monitors.*.dll") ??
                throw new Exception("No monitor assemblies found in directory: " + AssemblyDirectory.FullName + ".");

            ServiceAssemblies = Assembly.GetExecutingAssembly().LoadAllFrom("Fss.Services.*.dll") ??
                throw new Exception("No service assemblies found in directory: " + AssemblyDirectory.FullName + ".");

            LoadedAssemblies = 
                MonitorAssemblies
                .Concat(ActivityDetectorAssemblies)
                .Concat(ServiceAssemblies)
                .ToList();

            CancellationTokenSource = new CancellationTokenSource();
        }
        #endregion

        #region Properties
        private static DirectoryInfo AssemblyDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

        private static Version AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

        public static List<Assembly> ActivityDetectorAssemblies { get; internal set; }

        public static List<Assembly> MonitorAssemblies { get; internal set; }

        public static List<Assembly> ServiceAssemblies { get; internal set; }

        public static string[] ExcludedAssemblyNames { get; } = new string[0];

        public static List<Assembly> LoadedAssemblies { get; internal set; }

        public static CancellationTokenSource CancellationTokenSource { get; }

        public static MessageQueue MessageQueue { get; private set; }
        #endregion

        #region Methods

        public static Type[] GetSubTypes<T>(string assemblyName = "")
        {
            IEnumerable<Assembly> assemblies = null;
            if (assemblyName.IsNotEmpty() &&
                LoadedAssemblies.Count(a => assemblyName.IsNotEmpty() && a.GetName().Name == assemblyName) > 0)
            {
                assemblies = LoadedAssemblies.Where(a => a.FullName.StartsWith(assemblyName));
            }
            else if (assemblyName.IsEmpty())
            {
                assemblies = LoadedAssemblies;
            }
            else
            {
                throw new InvalidOperationException($"The assembly {assemblyName} is not loaded.");
            }

            return assemblies
                 .Select(a => a.GetTypes())
                 .SelectMany(t => t)
                 .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)?
                 .ToArray();
        }

        public static Type[] GetTypesImplementing<T>(string assemblyName = "")
        {
            IEnumerable<Assembly> assemblies = null;
            if (assemblyName.IsNotEmpty() &&
                LoadedAssemblies.Count(a => assemblyName.IsNotEmpty() && a.GetName().Name == assemblyName) > 0)
            {
                assemblies = LoadedAssemblies.Where(a => a.FullName.StartsWith(assemblyName));
            }
            else if (assemblyName.IsEmpty())
            {
                assemblies = LoadedAssemblies;
            }
            else
            {
                throw new InvalidOperationException($"The assembly {assemblyName} is not loaded.");
            }

            IEnumerable<Type> types =
                from type in assemblies.SelectMany(a => a.GetTypes())
                where typeof(T).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract
                select type;
            return types.ToArray();
        }
        #endregion
        /**
        public static string GetAppSetting(string fileName, string key, bool throwIfNotExists = false)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception($"The configuration file {fileName} does not exist.");
            }
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = fileName;
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            if (!configuration.AppSettings.Settings.AllKeys.Contains(key) &&throwIfNotExists)
            {
                throw new Exception($"The key {key} does not exist in the file {fileName}.");
            }
            else if (!configuration.AppSettings.Settings.AllKeys.Contains(key))
            {
                return "";
            }
            else return configuration.AppSettings.Settings[key].Value;
        }
        
        public static string GetAppSetting(string key)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                throw new Exception($"The key {key} does not exist in the default configuration file.");
            }
            return ConfigurationManager.AppSettings[key];
        }
        #endregion
        **/
        #region Fields
        //private static object setupLoggerLock = new object();
        #endregion
    }
}
