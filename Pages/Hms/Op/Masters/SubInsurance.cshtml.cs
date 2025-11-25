using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class SubInsuranceModel : BasePageModel
    {
        string BaseUrl = "/api/SubInsurance/";

        public SubInsuranceModel(IHttpClientFactory factory) : base(factory) { }

        public List<SubInsurance> ListSubInsurance { get; set; } = new();
        public List<InsuranceMaster> ListInsuranceMaster { get; set; } = new();

        [BindProperty] public SubInsurance NewRow { get; set; } = new();
        [BindProperty] public SubInsurance EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }

        private async Task LoadInsuranceAsync()
        {
            var url = "/api/InsuranceMaster/GetInsuranceMaster";

            var req = new InsuranceMaster
            {
                InsuranceId = "",
                InsuranceName = "",
                PageIndex = 1,
                PageSize = 200
            };

            ListInsuranceMaster =
                await PostApiAsync<List<InsuranceMaster>>(url, req)
                ?? new List<InsuranceMaster>();
        }

        private async Task LoadDataAsync()
        {
            await LoadInsuranceAsync();

            var url = BaseUrl + "GetSubInsurance";

            var req = new SubInsurance
            {
                SubInsuranceId = "",
                SubInsuranceName = Search ?? "",
                PageIndex = 1,
                PageSize = 200
            };

            ListSubInsurance =
                await PostApiAsync<List<SubInsurance>>(url, req)
                ?? new List<SubInsurance>();
        }

        public async Task OnGetAsync() => await LoadDataAsync();

        public async Task<IActionResult> OnPostAddNewAsync()
        {
            await LoadDataAsync();
            ShowNewRow = true;
            return Page();
        }

        public IActionResult OnPostCancelNew() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveNewAsync()
        {
            var dto = new SubInsurance
            {
                SubInsuranceId = "0",
                SubInsuranceName = NewRow.SubInsuranceName,
                SubInsuranceCode = NewRow.SubInsuranceCode,
                InsuranceId = NewRow.InsuranceId,
                IsActive = NewRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SaveSubInsurance";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Insert failed.");
                await LoadDataAsync();
                ShowNewRow = true;
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            await LoadDataAsync();
            EditingId = id;
            EditRow = ListSubInsurance.First(x => x.SubInsuranceId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new SubInsurance
            {
                SubInsuranceId = id,
                SubInsuranceName = EditRow.SubInsuranceName,
                SubInsuranceCode = EditRow.SubInsuranceCode,
                InsuranceId = EditRow.InsuranceId,
                IsActive = EditRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SaveSubInsurance";

            var result = await PostApiAsync<object>(url, dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Update failed.");
                await LoadDataAsync();
                EditingId = id;
                return Page();
            }

            return RedirectToPage();
        }

        public string GetInsuranceName(string? id)
        {
            return ListInsuranceMaster
                .FirstOrDefault(x => x.InsuranceId == id)?.InsuranceName ?? "";
        }
    }
}
