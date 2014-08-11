namespace NServiceBus.Logging
{
    using System;
    using System.Web;

    /// <summary>
    /// The default <see cref="LoggingFactoryDefinition"/>.
    /// </summary>
    public class DefaultFactory : LoggingFactoryDefinition
    {

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultFactory"/>.
        /// </summary>
        public DefaultFactory()
        {
            directory = FindDefaultLoggingDirectory();
            level = LogLevelReader.GetDefaultLogLevel();
        }
        /// <summary>
        /// <see cref="LoggingFactoryDefinition.GetLoggingFactory"/>.
        /// </summary>
        public override ILoggerFactory GetLoggingFactory()
        {
            return new DefaultLoggerFactory(level, directory);            
        }

        LogLevel level;

        /// <summary>
        /// Controls the <see cref="LogLevel"/>.
        /// </summary>
        public DefaultFactory Level(LogLevel level)
        {
            this.level = level;
            return this;
        }

        public string directory { get; set; }

        /// <summary>
        /// The directory to log files to.
        /// </summary>
        public DefaultFactory Directory(string directory)
        {
            if (System.IO.Directory.Exists(directory))
            {
                throw new Exception(string.Format("Could not find logging directory: {0}", directory));
            }
            this.directory = directory;
            return this;
        }

        static string FindDefaultLoggingDirectory()
        {
            //use appdata if it exists
            if (HttpContext.Current != null)
            {
                var appDataPath = HttpContext.Current.Server.MapPath("~/App_Data/");
                if (System.IO.Directory.Exists(appDataPath))
                {
                    return appDataPath;
                }
            }
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}