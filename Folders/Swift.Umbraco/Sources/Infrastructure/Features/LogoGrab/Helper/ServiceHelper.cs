using $safeprojectname$.$safeprojectname$.LogoGrab.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace $safeprojectname$.$safeprojectname$.LogoGrab.Helper
{
    public static class ServiceHelper
    {
        static HttpClientHandler clientHandler = new HttpClientHandler();

        public static async Task<LogoGrabResponse> ValidateAsync(LogoGrabSettings settings, string imagePath)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "LogoGrab settings cannot be null");
            }

            if (string.IsNullOrEmpty(imagePath))
            {
                throw new ArgumentNullException("imagePath cannot be null");
            }

            var apiUrl = new Uri(settings.ApiUrl);

            using (var client = new HttpClient(clientHandler, false))
            {
                client.BaseAddress = apiUrl;
                client.DefaultRequestHeaders.Add("X-DEVELOPER-KEY", settings.DeveloperKey);

                var imageData = File.ReadAllBytes(imagePath);
                var imageFileName = Path.GetFileName(imagePath);

                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(new MemoryStream(imageData)), "mediaFile", imageFileName);
                    string result;

                    using (var response = await client.PostAsync(apiUrl, content))
                    {
                        using (var message = response.Content)
                        {
                            result = await message.ReadAsStringAsync();
                        }
                    }

                    return JsonConvert.DeserializeObject<LogoGrabResponse>(result);
                }
            }
        }
    }
}
