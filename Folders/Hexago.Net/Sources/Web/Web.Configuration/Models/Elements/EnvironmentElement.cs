using System.Configuration;
using $safeprojectname$.Models.Collections;

namespace $safeprojectname$.Models.Elements
{
    public class EnvironmentElement : ConfigurationElement
    {
        public EnvironmentElement()
        {
        }

        public EnvironmentElement(string name, string domain)
        {
            this.Name = name;
            this.Domain = domain;
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("domain", IsRequired = false, DefaultValue = "")]
        public string Domain
        {
            get { return (string)this["domain"]; }
            set { this["domain"] = value; }
        }

        [ConfigurationProperty("Sites", IsDefaultCollection = false)]
        public SiteCollection Sites
        {
            get { return (SiteCollection)base["Sites"]; }
        }
    }
}
