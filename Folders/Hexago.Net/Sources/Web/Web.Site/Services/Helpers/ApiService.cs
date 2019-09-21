using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using $safeprojectname$.Services.Interfaces;
using $safeprojectname$.Services.Models;

namespace $safeprojectname$.Services.Helpers
{
    public class ApiService : IApiService
    {
        static HttpClientHandler httpHandler = new HttpClientHandler();

        private Uri baseUrl;
        private string username;
        private string password;

        public ApiService()
        {
            //baseUrl = new Uri(ConfigurationManager.AppSettings["Api:BaseUrl"]);
            //username = ConfigurationManager.AppSettings["Api:Username"];
            //password = ConfigurationManager.AppSettings["Api:Password"];
        }

        public async Task<string> GetPrivacyAsync()
        {
            try
            {
                var apiUrl = new Uri(baseUrl, "legal/privacy");

                using (var client = new HttpClient(httpHandler, false))
                {
                    client.BaseAddress = apiUrl;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    string credentials = $"{username}:{password}";
                    byte[] bytes = Encoding.ASCII.GetBytes(credentials);
                    string authentication = Convert.ToBase64String(bytes);

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic", authentication);

                    string result;

                    using (var response = await client.GetAsync(apiUrl))
                    {
                        using (var content = response.Content)
                        {
                            result = await content.ReadAsStringAsync();
                        }
                    }

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<LegalText>>(result);

                    if (apiResponse.Success)
                    {
                        return apiResponse.Data.Terms;
                    }

                    throw new HttpRequestException("Api Service unreachable");
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}