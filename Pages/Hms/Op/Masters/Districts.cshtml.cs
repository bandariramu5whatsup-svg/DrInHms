using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc; 

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class DistrictsModel : BasePageModel
    {
        string BaseUrl = "/api/Districts/";
        public DistrictsModel(IHttpClientFactory factory) : base(factory) { }

        public List<District> ListDistricts { get; set; } = new();
        public List<State> ListStates { get; set; } = new();

        [BindProperty] public District NewRow { get; set; } = new();
        [BindProperty] public District EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }
        private async Task LoadDistrictsAsync()
        {
            var Url = BaseUrl + "GetDistricts";

            var request = new District
            {
                CountryId = "",
                StateName = Search ?? "",
                IsActive = 1,
                PageIndex = 1,
                PageSize = 50
            };

            ListDistricts = await PostApiAsync<List<District>>(Url, request)
                        ?? new List<District>();
        }
        public async Task OnGetAsync()
        {
            await LoadDistrictsAsync();
            await FillStatesAsync();
        }
        public async Task<IActionResult> OnPostAddNewAsync()
        {
            await LoadDistrictsAsync();
            await FillStatesAsync();
            ShowNewRow = true;
            return Page();
        }

        private async Task FillStatesAsync()
        {
            var Url = BaseUrl + "FillStates";

            ListStates = await PostApiAsync<List<State>>(Url, new State())
                        ?? new List<State>();
        }
        public IActionResult OnPostCancelNew() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveNewAsync()
        {
            await FillStatesAsync();

            var selectedStates = ListStates
                .FirstOrDefault(x => x.StateId == NewRow.StateId);

            var dto = new District
            {

                DistrictId = "0",
                DistrictName = NewRow.DistrictName,
                DistrictCode = NewRow.DistrictCode,
                StateId = NewRow.StateId,
                StateName = selectedStates?.StateName,               
                CountryId = "0", 
                
                IsActive = NewRow.IsActive,
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SaveDistricts";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Insert failed.");

                await LoadDistrictsAsync();
                ShowNewRow = true;

                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            await LoadDistrictsAsync();
            EditingId = id;

            EditRow = ListDistricts.First(x => x.CountryId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new District
            {
                DistrictId = id,
                DistrictName = NewRow.DistrictName,
                DistrictCode = NewRow.DistrictCode,
                IsDefaultDistrict = EditRow.IsDefaultDistrict,

                StateId = id,
                StateName = EditRow.StateName,  
                
                CountryId = id, 
                IsActive = EditRow.IsActive,
                
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };
            var url = BaseUrl + "SaveDistricts";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Update failed.");

                await LoadDistrictsAsync();
                EditingId = id;

                return Page();
            }

            return RedirectToPage();
        }
    }
}