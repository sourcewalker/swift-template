using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;

namespace $safeprojectname$.$safeprojectname$.Captcha.Helper
{
    public static class GoogleRecaptchaHelper
    {
        public static bool ValidateReCaptchaV2(string captchaResponse)
        {
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Captcha:ServerValidationEnabled"]))
            {
                return true;
            }

            var apiRequest = ConfigurationManager.AppSettings["Captcha:ApiRequest"]
                                                 .ToString(CultureInfo.InvariantCulture);
            var secretKey = ConfigurationManager.AppSettings["Captcha:SecretKey"]
                                                .ToString(CultureInfo.InvariantCulture);
            apiRequest = string.Format(apiRequest, secretKey, captchaResponse);

            var response = false;

            using (var webClient = new WebClient())
            {
                var jsonStr = webClient.DownloadString(apiRequest);

                JToken token = JObject.Parse(jsonStr);
                var success = token.SelectToken("success").ToString().ToLower();

                if (!string.IsNullOrEmpty(success) && success != "false")
                    response = true;
            }

            return response;
        }
    }
}
