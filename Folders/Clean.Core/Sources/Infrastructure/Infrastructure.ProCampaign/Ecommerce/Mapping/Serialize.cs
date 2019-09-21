using $safeprojectname$.Ecommerce.Product;
using $safeprojectname$.Ecommerce.Order;
using $safeprojectname$.Ecommerce.Response;
using $safeprojectname$.Ecommerce.Authentication;
using Newtonsoft.Json;

namespace $safeprojectname$.Ecommerce.Mapping
{
    public static class Serialize
    {
        public static string ToJson(this EcommerceProductRequest self) => 
            JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this EcommerceOrderRequest self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this EcommerceApiResponse self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this EcommerceAuthForm self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
