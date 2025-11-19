using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class ExpensesPurposeModel : BasePageModel
    {
        string BaseUrl = "/api/ExpensesPurpose/";

        public ExpensesPurposeModel(IHttpClientFactory factory) : base(factory) { }

        public List<ExpensesPurpose> ListExpensesPurpose { get; set; } = new();

        [BindProperty] public ExpensesPurpose NewRow { get; set; } = new();
        [BindProperty] public ExpensesPurpose EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }

        private async Task LoadDataAsync()
        {
            var url = BaseUrl + "GetExpensesPurpose";

            var req = new ExpensesPurpose
            {
                ExpensesPurposeId = "",
                ExpensesPurposeName = Search ?? "",
                PageIndex = 1,
                PageSize = 50
            };

            ListExpensesPurpose =
                await PostApiAsync<List<ExpensesPurpose>>(url, req)
                ?? new List<ExpensesPurpose>();
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
            var dto = new ExpensesPurpose
            {
                ExpensesPurposeId = "0",
                ExpensesPurposeName = NewRow.ExpensesPurposeName,
                ExpensesPurposeCode = NewRow.ExpensesPurposeCode,
                IsActive = NewRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId"),
            };

            var url = BaseUrl + "SaveExpensesPurpose";

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
            EditRow = ListExpensesPurpose.First(x => x.ExpensesPurposeId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new ExpensesPurpose
            {
                ExpensesPurposeId = id,
                ExpensesPurposeName = EditRow.ExpensesPurposeName,
                ExpensesPurposeCode = EditRow.ExpensesPurposeCode,
                IsActive = EditRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId"),
            };

            var url = BaseUrl + "SaveExpensesPurpose";

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
    }
}
