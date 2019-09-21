using System.Configuration;
using $safeprojectname$.Models.Collections;

namespace $safeprojectname$.Models.Elements
{
    public class SiteElement : ConfigurationElement
    {
        public SiteElement()
        {
        }

        public SiteElement(string name, string culture, string domain)
        {
            this.Name = name;
            this.Culture = culture;
            this.Domain = domain;
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("culture", IsRequired = false, IsKey = true)]
        public string Culture
        {
            get { return (string)this["culture"]; }
            set { this["culture"] = value; }
        }

        [ConfigurationProperty("domain", IsRequired = false, IsKey = true)]
        public string Domain
        {
            get { return (string)this["domain"]; }
            set { this["domain"] = value; }
        }

        [ConfigurationProperty("Configs", IsDefaultCollection = false)]
        public AddCollection Adds
        {
            get { return (AddCollection)base["Configs"]; }
        }
    }
}
