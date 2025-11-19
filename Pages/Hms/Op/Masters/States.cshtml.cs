
using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class StatesModel : BasePageModel
    {
        string BaseUrl = "/api/States/";
        public StatesModel(IHttpClientFactory factory) : base(factory) { }

        public List<State> ListStates { get; set; } = new();
        public List<Country> ListCountries { get; set; } = new();

        [BindProperty] public State NewRow { get; set; } = new();
        [BindProperty] public State EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }

        private async Task LoadStatesAsync()
        {
            var Url = BaseUrl + "GetStates";

            var request = new State
            {
                CountryId = "",
                StateName = Search ?? "",
                IsActive = 1,
                PageIndex = 1,
                PageSize = 50
            };

            ListStates = await PostApiAsync<List<State>>(Url, request)
                        ?? new List<State>();
        }

        private async Task FillCountriesAsync()
        {
            var Url = BaseUrl + "FillCountries";

            ListCountries = await PostApiAsync<List<Country>>(Url, new State())
                        ?? new List<Country>();
        }

        public async Task OnGetAsync()
        {
            await LoadStatesAsync();
            await FillCountriesAsync();
        }

        public async Task<IActionResult> OnPostAddNewAsync()
        {
            await LoadStatesAsync();
            await FillCountriesAsync();

            ShowNewRow = true;
            return Page();
        }

        public IActionResult OnPostCancelNew() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveNewAsync()
        {
            await FillCountriesAsync();

            var selectedCountry = ListCountries
                .FirstOrDefault(x => x.CountryId == NewRow.CountryId);

            var dto = new State
            {
                StateId = "0",
                StateName = NewRow.StateName,
                CountryId = NewRow.CountryId,
                CountryName = selectedCountry?.CountryName,
                StateCode = NewRow.StateCode,
                IsActive = NewRow.IsActive,
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SaveStates";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                await LoadStatesAsync();
                await FillCountriesAsync();
                ShowNewRow = true;
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            await LoadStatesAsync();
            await FillCountriesAsync();

            EditingId = id;

            EditRow = ListStates.First(x => x.StateId == id);

            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            await FillCountriesAsync();

            var selectedCountry = ListCountries
                .FirstOrDefault(x => x.CountryId == EditRow.CountryId);

            var dto = new State
            {
                StateId = id,
                StateName = EditRow.StateName,
                StateCode = EditRow.StateCode,
                CountryId = EditRow.CountryId,
                CountryName = selectedCountry?.CountryName,
                IsActive = EditRow.IsActive,
                IsDefaultState = EditRow.IsDefaultState,
                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SaveStates";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                await LoadStatesAsync();
                await FillCountriesAsync();
                EditingId = id;
                return Page();
            }

            return RedirectToPage();
        }
    }
}

//using HanuMediSoftCore.Models.Hms.Op.Masters;
//using Microsoft.AspNetCore.Mvc; 

//namespace HanuMediSoftCore.Pages.Hms.Op.Masters
//{
//    public class StatesModel : BasePageModel
//    {
//        string BaseUrl = "/api/States/";
//        public StatesModel(IHttpClientFactory factory) : base(factory) { }

//        public List<State> ListStates { get; set; } = new();
//        public List<Country> ListCountries { get; set; } = new();

//        [BindProperty] public State NewRow { get; set; } = new();
//        [BindProperty] public State EditRow { get; set; } = new();

//        [BindProperty(SupportsGet = true)]
//        public string? Search { get; set; }

//        public bool ShowNewRow { get; set; }
//        public string? EditingId { get; set; }
//        private async Task LoadStatesAsync()
//        {
//            var Url = BaseUrl + "GetStates";

//            var request = new State
//            {
//                CountryId = "",
//                StateName = Search ?? "",
//                IsActive = 1,
//                PageIndex = 1,
//                PageSize = 50
//            };

//            ListStates = await PostApiAsync<List<State>>(Url, request)
//                        ?? new List<State>();
//        }

//        private async Task FillCountriesAsync()
//        {
//            var Url = BaseUrl + "FillCountries";

//            var request = new State
//            {

//            };

//            ListCountries = await PostApiAsync<List<Country>>(Url, request)
//                        ?? new List<Country>();
//        }
//        public async Task OnGetAsync()
//        {
//            await LoadStatesAsync();
//            await FillCountriesAsync();
//        }
//        public async Task<IActionResult> OnPostAddNewAsync()
//        {
//            await LoadStatesAsync();
//            await FillCountriesAsync();
//            ShowNewRow = true;
//            return Page();
//        }

//        public IActionResult OnPostCancelNew() => RedirectToPage();

//        public async Task<IActionResult> OnPostSaveNewAsync()
//        {

//            var selectedCountry = ListCountries
//                    .FirstOrDefault(x => x.CountryId == EditRow.CountryId);
//            var selectedCountryName = selectedCountry?.CountryName;


//            var dto = new State
//            {
//                StateId = "0",
//                StateName = NewRow.StateName,
//                CountryId = "0",
//                CountryName = NewRow.CountryName,
//                StateCode = NewRow.StateCode,
//                IsActive = NewRow.IsActive,
//                CreatedById = HttpContext.Session.GetString("gUserId"),
//                CreatedByName = HttpContext.Session.GetString("gUserName"),
//                WorkstationId = HttpContext.Session.GetString("gTerminalId")
//            };

//            var url = BaseUrl + "SaveStates";

//            var result = await PostApiAsync<object>(url, dto);

//            if (result == null)
//            {
//                ModelState.AddModelError("", "Insert failed.");

//                await LoadStatesAsync();
//                ShowNewRow = true;

//                return Page();
//            }

//            return RedirectToPage();
//        }

//        public async Task<IActionResult> OnPostEditAsync(string id)
//        {
//            await LoadStatesAsync();
//            EditingId = id;

//            EditRow = ListStates.First(x => x.CountryId == id);
//            return Page();
//        }

//        public IActionResult OnPostCancelEdit() => RedirectToPage();

//        public async Task<IActionResult> OnPostSaveEditAsync(string id)
//        {
//            var dto = new State
//            {
//                StateId = id,
//                StateName = EditRow.StateName,
//                StateCode = EditRow.StateCode,
//                CountryId = id,
//                CountryName = EditRow.CountryName,
//                IsActive = EditRow.IsActive,
//                IsDefaultState = EditRow.IsDefaultState,
//                CreatedById = HttpContext.Session.GetString("gUserId"),
//                CreatedByName = HttpContext.Session.GetString("gUserName"),
//                WorkstationId = HttpContext.Session.GetString("gTerminalId")
//            };
//            var url = BaseUrl + "GetStates";

//            var result = await PostApiAsync<object>(url, dto);

//            if (result == null)
//            {
//                ModelState.AddModelError("", "Update failed.");

//                await LoadStatesAsync();
//                EditingId = id;

//                return Page();
//            }

//            return RedirectToPage();
//        }
//    }
//}