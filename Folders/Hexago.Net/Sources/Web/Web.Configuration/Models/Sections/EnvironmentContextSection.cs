using System.Configuration;
using $safeprojectname$.Models.Collections;

namespace $safeprojectname$.Sections
{
    public sealed partial class EnvironmentContextSection : ConfigurationSection
    {
        [ConfigurationProperty("Environments", IsDefaultCollection = true)]
        public EnvironmentCollection Environments
        {
            get
            {
                EnvironmentCollection hostCollection = (EnvironmentCollection)base["Environments"];
                return hostCollection;
            }
        }
    }
}
