using System.Configuration;
using $safeprojectname$.Models.Elements;

namespace $safeprojectname$.Models.Collections
{
    public class AddCollection : ConfigurationElementCollection
    {
        public new AddElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (AddElement)BaseGet(name);
            }
        }

        public AddElement this[int index]
        {
            get { return (AddElement)BaseGet(index); }
        }

        public int IndexOf(string key)
        {
            key = key.ToLower();

            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Key.ToLower() == key)
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
            return new AddElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AddElement)element).Key;
        }

        protected override string ElementName
        {
            get { return "add"; }
        }
    }
}
