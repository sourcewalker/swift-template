using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Umbraco.$safeprojectname$.DTO
{
    public class CrmConfiguration
    {
        public dynamic Settings { get; set; }

        public List<string> Exceptions = new List<string>();

        public CrmConfiguration()
        {
            Settings = new ExpandoObject();
        }

        public void AddSetting(string key, object value)
        {
            var p = Settings as IDictionary<string, object>;
            p[key] = value;
        }

        public T GetSetting<T>(string key)
        {
            var p = Settings as IDictionary<string, object>;

            return p.ContainsKey(key) ? (T)p[key] : default;
        }

        public void AddException(string value)
        {
            Exceptions.Add(value);
        }
    }
}
