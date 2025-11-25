using System.Text.Json;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class ConsultantDoctorModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _config;

        public ConsultantDoctorModel(IHttpClientFactory factory, IConfiguration config)
        {
            _httpFactory = factory;
            _config = config;
        }

        [BindProperty]
        public ConsultantDoctorDto Input { get; set; } = new();

        public List<SimpleItem> Departments { get; set; } = new();
        public List<SimpleItem> Specializations { get; set; } = new();

        public string ResultMessage { get; set; }

        public async Task OnGetAsync(string? id)
        {
            var client = _httpFactory.CreateClient("ApiClient");

            // load dropdowns
            try
            {
                Departments = await client.GetFromJsonAsync<List<SimpleItem>>("api/lookup/departments") ?? new();
                Specializations = await client.GetFromJsonAsync<List<SimpleItem>>("api/lookup/specializations") ?? new();
            }
            catch { }

            // edit mode
            if (!string.IsNullOrWhiteSpace(id))
            {
                try
                {
                    var dto = await client.GetFromJsonAsync<ConsultantDoctorDto>($"api/ConsultantDoctors/{id}");
                    if (dto != null) Input = dto;
                }
                catch { }
            }
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            Input.CreatedById ??= User?.Identity?.Name ?? "SYSTEM";
            Input.CreatedByName ??= User?.Identity?.Name ?? "SYSTEM";
            Input.WorkstationId ??= Environment.MachineName;

            try
            {
                var client = _httpFactory.CreateClient("ApiClient");
                var resp = await client.PostAsJsonAsync("api/ConsultantDoctors/save", Input);

                if (resp.IsSuccessStatusCode)
                {
                    var obj = await resp.Content.ReadFromJsonAsync<JsonElement>();
                    if (obj.TryGetProperty("success", out var succ) && succ.GetBoolean())
                    {
                        var id = obj.GetProperty("id").GetString();
                        ResultMessage = $"Saved successfully. Id: {id}";
                    }
                    else
                    {
                        ResultMessage = "Save returned false: " + obj.ToString();
                    }
                }
                else
                {
                    var txt = await resp.Content.ReadAsStringAsync();
                    ResultMessage = $"API error: {resp.StatusCode} - {txt}";
                }
            }
            catch (Exception ex)
            {
                ResultMessage = "Save exception: " + ex.Message;
            }

            await OnGetAsync(Input.ConsultantDoctorId); // reload
            return Page();
        }
    }

    public class SimpleItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
