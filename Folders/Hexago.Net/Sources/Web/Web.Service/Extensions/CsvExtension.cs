using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace $safeprojectname$.Extensions
{
    public static class CsvExtension
    {
        public static string ToCsv(
            this IEnumerable data,
            bool headerRow = false)
        {
            using (var writer = new StringWriter())
            {
                CsvHelper.WriteCSV(data, writer, headerRow);

                return writer.ToString();
            }
        }
    }
}