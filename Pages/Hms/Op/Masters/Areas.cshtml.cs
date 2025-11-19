using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc; 

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class AreasModel : BasePageModel
    {
        string BaseUrl = "/api/States/";
        public AreasModel(IHttpClientFactory factory) : base(factory) { }

        public List<Area> ListAreas { get; set; } = new();

        [BindProperty] public Area NewRow { get; set; } = new();
        [BindProperty] public Area EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }
        private async Task LoadAreasAsync()
        {
            var Url = BaseUrl + "GetStates";

            var request = new Area
            {
                AreaId = Search ?? "",
                AreaName = Search ?? "",
                DistrictId = Search ?? "",
                StateId = Search ?? "",
                CountryId = Search ?? "",
                IsActive = 1,
                PageIndex = 1,
                PageSize = 50
            };

            ListAreas = await PostApiAsync<List<Area>>(Url, request)
                        ?? new List<Area>();
        }
        public async Task OnGetAsync()
        {
            await LoadAreasAsync();
        }
        public async Task<IActionResult> OnPostAddNewAsync()
        {
            await LoadAreasAsync();
            ShowNewRow = true;
            return Page();
        }

        public IActionResult OnPostCancelNew() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveNewAsync()
        {
            var dto = new Area
            {

                AreaId = "0",
                AreaName = NewRow.AreaName,
                AreaCode = NewRow.AreaCode,
                StateId = "0",
                DistrictId = "0",
                CountryId = "0",

                IsActive = NewRow.IsActive,
                IsDefaultArea = NewRow.IsDefaultArea,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SaveStates";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Insert failed.");

                await LoadAreasAsync();
                ShowNewRow = true;

                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            await LoadAreasAsync();
            EditingId = id;

            EditRow = ListAreas.First(x => x.CountryId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new Area
            {
                AreaId = id,
                AreaName = NewRow.AreaName,
                AreaCode = NewRow.AreaCode,
                DistrictId = id ,
                StateId = id,
                CountryId = id,
                IsDefaultArea = EditRow.IsDefaultArea,  
                IsActive = EditRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };
            var url = BaseUrl + "GetStates";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Update failed.");

                await LoadAreasAsync();
                EditingId = id;

                return Page();
            }

            return RedirectToPage();
        }
    }
}