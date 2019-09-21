using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace $safeprojectname$.Extensions
{
    public class CsvMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public CsvMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public override void WriteToStream(
            Type type,
            object value,
            Stream writeStream,
            HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                string csv = ((IEnumerable)value).ToCsv(true);
                writer.Write(csv);
            }
        }
    }
}