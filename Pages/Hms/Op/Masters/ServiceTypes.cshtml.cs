using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;
 
namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class ServiceTypesModel : BasePageModel
    {
        public ServiceTypesModel(IHttpClientFactory factory) : base(factory) { }

        public List<ServiceType> ListServiceType { get; set; } = new();

        [BindProperty] public ServiceType NewRow { get; set; } = new();
        [BindProperty] public ServiceType EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }
        private async Task LoadUnitsAsync()
        {
            var Url = "/api/ServiceTypes/GetServiceTypes";

            var request = new ServiceType
            {
                ServiceTypeId = "",
                ServiceTypeName = Search ?? "",
                IsActive = 1,
                PageIndex = 1,
                PageSize = 50
            };

            ListServiceType = await PostApiAsync<List<ServiceType>>(Url, request)
                        ?? new List<ServiceType>();
        }
        public async Task OnGetAsync()
        {
            await LoadUnitsAsync();
        }
        public async Task<IActionResult> OnPostAddNewAsync()
        {
            await LoadUnitsAsync();
            ShowNewRow = true;
            return Page();
        }

        public IActionResult OnPostCancelNew() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveNewAsync()
        {
            var Url = "/api/ServiceTypes/SaveServiceType";
            var dto = new ServiceType
            {
                ServiceTypeId = "0",
                ServiceTypeName = NewRow.ServiceTypeName,
                Description = NewRow.Description,
                IsActive = NewRow.IsActive,
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var result = await PostApiAsync<object>(Url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Insert failed.");

                await LoadUnitsAsync();
                ShowNewRow = true;

                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            await LoadUnitsAsync();
            EditingId = id;

            EditRow = ListServiceType.First(x => x.ServiceTypeId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new ServiceType
            {
                ServiceTypeId = id,
                ServiceTypeName = EditRow.ServiceTypeName,
                Description = EditRow.Description,
                IsActive = EditRow.IsActive,
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var result = await PostApiAsync<object>("/api/Units/SaveUnits", dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Update failed.");

                await LoadUnitsAsync();
                EditingId = id;

                return Page();
            }

            return RedirectToPage();
        }
    }
}
