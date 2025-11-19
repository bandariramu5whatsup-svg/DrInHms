using Microsoft.AspNetCore.Mvc; 
using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Helpers;


namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    [PagePermission("UNITS")]
    public class UnitsModel : BasePageModel
    {
        public UnitsModel(IHttpClientFactory factory) : base(factory) { }

        public List<Unit> ListUnits { get; set; } = new();

        [BindProperty] public Unit NewRow { get; set; } = new();
        [BindProperty] public Unit EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; } 
        private async Task LoadUnitsAsync()
        {
            var Url = "/api/Units/GetUnits";

            var request = new Unit
            {
                UnitId = "",
                UnitName = Search ?? "",
                IsActive = 1,
                PageIndex = 1,
                PageSize = 50
            };

            ListUnits = await PostApiAsync<List<Unit>>(Url, request)
                        ?? new List<Unit>();
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
            var dto = new Unit 
            {
                UnitId = "0",
                UnitName = NewRow.UnitName,
                Description = NewRow.Description,
                IsActive = NewRow.IsActive,
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var result = await PostApiAsync<object>("/api/Units/SaveUnits", dto);

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

            EditRow = ListUnits.First(x => x.UnitId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();
         
        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new Unit
            {
                UnitId = id,
                UnitName = EditRow.UnitName,
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


//namespace HanuMediSoftCore.Pages.Hms.Op.Masters
//{
//    public class UnitsModel : BasePageModel
//    {
//        private readonly HttpClient _client;

//        public UnitsModel(IHttpClientFactory factory) : base(factory) { }

//        public List<UnitsDto> ListUnits { get; set; } = new();

//        [BindProperty] public UnitsDto NewRow { get; set; } = new();
//        [BindProperty] public UnitsDto EditRow { get; set; } = new();

//        [BindProperty(SupportsGet = true)]
//        public string? Search { get; set; }

//        public bool ShowNewRow { get; set; }
//        public string? EditingId { get; set; }




//        private async Task LoadUnitsAsync()
//        {
//            var request = new UnitsDto
//            {
//                UNIT_ID = "",
//                UNITS = Search ?? "",
//                ACTIVE = 1,
//                PAGE_INDEX = 1,
//                PAGE_SIZE = 50
//            };

//            ListUnits = await PostApiAsync<List<UnitsDto>>("/api/Units/GetUnits", request)
//                      ?? new List<UnitsDto>();
//        }

//        public async Task OnGetAsync()
//        {
//            await LoadUnitsAsync();
//        }

//        public async Task<IActionResult> OnPostAddNewAsync()
//        {
//            await LoadUnitsAsync();
//            ShowNewRow = true;
//            return Page();
//        }


//        public IActionResult OnPostCancelNew() => RedirectToPage();


//        public async Task<IActionResult> OnPostSaveNewAsync()
//        {
//            var dto = new UnitsDto
//            {
//                UNIT_ID = "0",
//                UNITS = NewRow.UNITS,
//                DESCRIPTION = NewRow.DESCRIPTION,
//                ACTIVE = NewRow.ACTIVE,
//                ENTRY_USER_ID = "1",
//                ENTRY_USER_NAME = "Admin",
//                TERMINAL_ID = "PC1"
//            };

//            //var result = await PostToApi<object>("/api/Units/SaveUnits", dto);

//            var result = await ApiHelper.PostAsync<List<UnitsDto>>(
//                _client,
//                "/api/Units/SaveUnits",
//                dto
//            ) ?? new List<UnitsDto>();

//            if (result == null)
//            {
//                ModelState.AddModelError("", "Insert failed.");
//                await LoadUnitsAsync();
//                ShowNewRow = true;
//                return Page();
//            }

//            return RedirectToPage();
//        }


//        public async Task<IActionResult> OnPostEditAsync(string id)
//        {
//            await LoadUnitsAsync();
//            EditingId = id;
//            EditRow = ListUnits.First(x => x.UNIT_ID == id);
//            return Page();
//        }

//        public IActionResult OnPostCancelEdit() => RedirectToPage();



//        public async Task<IActionResult> OnPostSaveEditAsync(string id)
//        {
//            var dto = new UnitsDto
//            {
//                UNIT_ID = id,
//                UNITS = EditRow.UNITS,
//                DESCRIPTION = EditRow.DESCRIPTION,
//                ACTIVE = EditRow.ACTIVE,
//                ENTRY_USER_ID = "1",
//                ENTRY_USER_NAME = "Admin",
//                TERMINAL_ID = "PC1"
//            };

//            //var result = await PostToApi<object>("/api/Units/SaveUnits", dto);

//            var result = await ApiHelper.PostAsync<List<UnitsDto>>(
//               _client,
//               "/api/Units/SaveUnits",
//               dto
//           ) ?? new List<UnitsDto>();

//            if (result == null)
//            {
//                ModelState.AddModelError("", "Update failed.");
//                await LoadUnitsAsync();
//                EditingId = id;
//                return Page();
//            }

//            return RedirectToPage();
//        }
//    }
//}


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using HanuMediSoftCore.Models.Hms.Op.Masters;

//namespace HanuMediSoftCore.Pages.Hms.Op.Masters
//{


//    public class UnitsModel : PageModel
//    {
//        private readonly IHttpClientFactory _factory;

//        public UnitsModel(IHttpClientFactory factory)
//        {
//            _factory = factory;
//        }

//        private HttpClient CreateClient() => _factory.CreateClient("apiClient");

//        public List<UnitsDto> ListUnits { get; set; } = new();

//        [BindProperty]
//        public UnitsDto NewRow { get; set; } = new();

//        [BindProperty]
//        public UnitsDto EditRow { get; set; } = new();

//        [BindProperty(SupportsGet = true)]
//        public string? Search { get; set; }

//        public bool ShowNewRow { get; set; }
//        public string? EditingId { get; set; }

//        // -------------------------
//        // LOAD DATA USING SP
//        // -------------------------
//        private async Task LoadFromApi()
//        {
//            var client = CreateClient();

//            var req = new UnitsDto
//            {
//                UNIT_ID = "",
//                UNITS = Search ?? "",
//                ACTIVE = 1,
//                PAGE_INDEX = 1,
//                PAGE_SIZE = 50
//            };

//            var response = await client.PostAsJsonAsync("/api/Units/GetUnits", req);

//            if (response.IsSuccessStatusCode)
//            {
//                var list = await response.Content.ReadFromJsonAsync<List<UnitsDto>>();

//                ListUnits = list ?? new List<UnitsDto>();
//            }

//        }

//        // -------------------------
//        // ADD NEW ROW
//        // -------------------------
//        public async Task<IActionResult> OnPostAddNewAsync()
//        {
//            await LoadFromApi();
//            ShowNewRow = true;
//            return Page();
//        }

//        public IActionResult OnPostCancelNew() => RedirectToPage();

//        // -------------------------
//        // SAVE NEW ROW
//        // -------------------------
//        public async Task<IActionResult> OnPostSaveNewAsync()
//        {
//            var client = CreateClient();

//            var dto = new UnitsDto
//            {
//                UNIT_ID = "0",
//                UNITS = NewRow.UNITS,
//                DESCRIPTION = NewRow.DESCRIPTION,
//                ACTIVE = NewRow.ACTIVE,
//                ENTRY_USER_ID = "1",
//                ENTRY_USER_NAME = "Admin",
//                TERMINAL_ID = "PC1"
//            };

//            var response = await client.PostAsJsonAsync("/api/Units/SaveUnits", dto);

//            if (!response.IsSuccessStatusCode)
//            {
//                ModelState.AddModelError(string.Empty, "Insert failed.");
//                await LoadFromApi();
//                ShowNewRow = true;
//                return Page();
//            }

//            return RedirectToPage();
//        }

//        // -------------------------
//        // EDIT
//        // -------------------------
//        public async Task<IActionResult> OnPostEditAsync(string id)
//        {
//            await LoadFromApi();
//            EditingId = id;

//            EditRow = ListUnits.First(x => x.UNIT_ID == id);

//            return Page();
//        }

//        public IActionResult OnPostCancelEdit() => RedirectToPage();

//        // -------------------------
//        // SAVE EDIT
//        // -------------------------
//        public async Task<IActionResult> OnPostSaveEditAsync(string id)
//        {
//            var client = CreateClient();

//            var dto = new UnitsDto
//            {
//                UNIT_ID = id,
//                UNITS = EditRow.UNITS,
//                DESCRIPTION = EditRow.DESCRIPTION,
//                ACTIVE = EditRow.ACTIVE,
//                ENTRY_USER_ID = "1",
//                ENTRY_USER_NAME = "Admin",
//                TERMINAL_ID = "PC1"
//            };

//            var response = await client.PostAsJsonAsync("/api/Units/SaveUnits", dto);

//            if (!response.IsSuccessStatusCode)
//            {
//                ModelState.AddModelError("", "Update failed.");
//                await LoadFromApi();
//                EditingId = id;
//                return Page();
//            }

//            return RedirectToPage();
//        }

//        public async Task OnGetAsync()
//        {
//            await LoadFromApi();
//        }
//    }
//}
