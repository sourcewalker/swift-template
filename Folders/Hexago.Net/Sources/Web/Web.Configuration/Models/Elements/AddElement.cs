using System.Configuration;

namespace $safeprojectname$.Models.Elements
{
    public class AddElement : ConfigurationElement
    {
        public AddElement()
        {
        }

        public AddElement(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        [ConfigurationProperty("key", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = false, DefaultValue = "")]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}
