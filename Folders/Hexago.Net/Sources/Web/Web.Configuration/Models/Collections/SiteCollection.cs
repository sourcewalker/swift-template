using System.Configuration;
using $safeprojectname$.Models.Elements;

namespace $safeprojectname$.Models.Collections
{
    public class SiteCollection : ConfigurationElementCollection
    {
        public new SiteElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (SiteElement)BaseGet(name);
            }
        }

        public SiteElement this[int index]
        {
            get { return (SiteElement)BaseGet(index); }
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
            return new SiteElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SiteElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "Site"; }
        }
    }
}
