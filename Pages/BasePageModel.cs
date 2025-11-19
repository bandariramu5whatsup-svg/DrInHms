
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace HanuMediSoftCore.Pages
{
    public class BasePageModel : PageModel
    {
        protected readonly HttpClient Api;

        public BasePageModel(IHttpClientFactory factory)
        {
            Api = factory.CreateClient("apiClient");

            // You can add global headers here (optional)
            // Api.DefaultRequestHeaders.Add("X-App", "HanuMediSoft");
        }

        // -----------------------------------------
        // Generic POST -> returns T
        // -----------------------------------------
        protected async Task<T?> PostApiAsync<T>(string url, object payload)
        {
            var response = await Api.PostAsJsonAsync(url, payload);

            if (!response.IsSuccessStatusCode)
                return default;

            return await response.Content.ReadFromJsonAsync<T>();
        }

        // -----------------------------------------
        // Generic GET -> returns T
        // -----------------------------------------
        protected async Task<T?> GetApiAsync<T>(string url)
        {
            var response = await Api.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return default;

            return await response.Content.ReadFromJsonAsync<T>();
        }

        // -----------------------------------------
        // Generic PUT -> returns success/fail
        // -----------------------------------------
        protected async Task<bool> PutApiAsync(string url, object payload)
        {
            var response = await Api.PutAsJsonAsync(url, payload);
            return response.IsSuccessStatusCode;
        }

        // -----------------------------------------
        // Generic DELETE -> returns success/fail
        // -----------------------------------------
        protected async Task<bool> DeleteApiAsync(string url)
        {
            var response = await Api.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
