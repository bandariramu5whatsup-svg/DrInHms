using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc; 
namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class PlacesModel : BasePageModel
    {
        string BaseUrl = "/api/States/";
        public PlacesModel(IHttpClientFactory factory) : base(factory) { }

        public List<Place> ListPlaces { get; set; } = new();

        [BindProperty] public Place NewRow { get; set; } = new();
        [BindProperty] public Place EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }
        private async Task LoadAreasAsync()
        {
            var Url = BaseUrl + "GetStates";

            var request = new Place
            {
                PlaceId = Search ?? "",
                AreaId = Search ?? "",
                PlaceName = Search ?? "",
                DistrictId = Search ?? "",
                StateId = Search ?? "",
                CountryId = Search ?? "",
                IsActive = 1,
                PageIndex = 1,
                PageSize = 50
            };

            ListPlaces = await PostApiAsync<List<Place>>(Url, request)
                        ?? new List<Place>();
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
            var dto = new Place
            {

                PlaceId = "0",
                PlaceName = NewRow.PlaceName,
                PlaceCode = NewRow.PlaceCode,
                AreaId = "0", 
                StateId = "0",
                DistrictId = "0",
                CountryId = "0",

                IsActive = NewRow.IsActive,
                IsDefaultPlace = NewRow.IsDefaultPlace,

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

            EditRow = ListPlaces.First(x => x.CountryId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new Place
            {
                PlaceId = id,
                PlaceName = NewRow.PlaceName,
                PlaceCode = NewRow.PlaceCode,
                AreaId = id,
                StateId = id,
                DistrictId = id,
                CountryId = id,

                IsActive = NewRow.IsActive,
                IsDefaultPlace = NewRow.IsDefaultPlace,

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