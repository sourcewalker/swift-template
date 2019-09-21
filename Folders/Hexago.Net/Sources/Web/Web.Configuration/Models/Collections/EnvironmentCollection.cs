using System.Configuration;
using $safeprojectname$.Models.Elements;

namespace $safeprojectname$.Models.Collections
{
    public class EnvironmentCollection : ConfigurationElementCollection
    {
        public new EnvironmentElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (EnvironmentElement)BaseGet(name);
            }
        }

        public EnvironmentElement this[int index]
        {
            get { return (EnvironmentElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Name.ToLower() == name)
                    return idx;
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new EnvironmentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EnvironmentElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "Environment"; }
        }
    }
}
