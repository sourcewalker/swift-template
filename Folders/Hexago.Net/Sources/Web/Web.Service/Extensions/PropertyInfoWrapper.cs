using System.Reflection;

namespace $safeprojectname$.Extensions
{
    public class PropertyInfoWrapper
    {
        public int Order { get; set; }

        public PropertyInfo PropertyInfo { get; private set; }

        public PropertyInfoWrapper(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }
    }
}