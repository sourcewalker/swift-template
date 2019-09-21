using System.Configuration;

namespace $safeprojectname$.Constant
{
    public class Configuration
    {
        private static Configuration _instance;

        private static readonly object _padlock = new object();

        private Configuration() { }

        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Configuration();
                        }

                    }
                }
                return _instance;
            }
        }

        public string LocalConnectionString => ConfigurationManager.ConnectionStrings["LocalElmah"].ConnectionString;

        public string ProductionConnectionString => ConfigurationManager.ConnectionStrings["ProductionElmah"].ConnectionString;
    }
}
