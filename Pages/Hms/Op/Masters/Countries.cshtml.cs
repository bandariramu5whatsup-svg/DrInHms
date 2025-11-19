using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc; 


namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class CountriesModel : BasePageModel
    {

        string BaseUrl = "/api/Countries/";
        public CountriesModel(IHttpClientFactory factory) : base(factory) { }

        public List<Country> ListCountries { get; set; } = new();

        [BindProperty] public Country NewRow { get; set; } = new();
        [BindProperty] public Country EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }
        private async Task LoadCountriesAsync()
        {
            var Url = BaseUrl+"GetCountries";

            var request = new Country
            {
                CountryId = "",
                CountryCode = Search ?? "",
                IsActive = 1,
                PageIndex = 1,
                PageSize = 50
            };

            ListCountries = await PostApiAsync<List<Country>>(Url, request)
                        ?? new List<Country>();
        }
        public async Task OnGetAsync()
        {
            await LoadCountriesAsync();
        }
        public async Task<IActionResult> OnPostAddNewAsync()
        {
            await LoadCountriesAsync();
            ShowNewRow = true;
            return Page();
        }

        public IActionResult OnPostCancelNew() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveNewAsync()
        {
            var dto = new Country
            {
                CountryId = "0",
                CountryName = NewRow.CountryName,
                CountryCode = NewRow.CountryCode,
                IsActive = NewRow.IsActive,
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

          var url = BaseUrl + "SaveCountries";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Insert failed.");

                await LoadCountriesAsync();
                ShowNewRow = true;

                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            await LoadCountriesAsync();
            EditingId = id;

            EditRow = ListCountries.First(x => x.CountryId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new Country
            {
                CountryId = id,
                CountryName = EditRow.CountryName,
                CountryCode = EditRow.CountryCode,
                IsActive = EditRow.IsActive,
                IsDefaultCountry = EditRow.IsDefaultCountry,
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };
            var url = BaseUrl + "SaveCountries";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Update failed.");

                await LoadCountriesAsync();
                EditingId = id;

                return Page();
            }

            return RedirectToPage();
        }
    }
}