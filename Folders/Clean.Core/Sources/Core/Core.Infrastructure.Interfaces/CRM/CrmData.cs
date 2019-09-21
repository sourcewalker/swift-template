using System.Collections.Generic;
using System.Dynamic;

namespace $safeprojectname$.CRM
{
    public class CrmData
    {
        public dynamic Data { get; set; }

        public CrmData()
        {
            Data = new ExpandoObject();
        }

        public void AddSetting(string key, object value)
        {
            var p = Data as IDictionary<string, object>;
            p[key] = value;
        }

        public T GetSetting<T>(string key)
        {
            var p = Data as IDictionary<string, object>;

            return p.ContainsKey(key) ? (T)p[key] : default;
        }
    }
}
