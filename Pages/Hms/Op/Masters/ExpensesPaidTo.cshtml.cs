using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class ExpensesPaidToModel : BasePageModel
    {
        string BaseUrl = "/api/ExpensesPaidTo/";
        public ExpensesPaidToModel(IHttpClientFactory factory) : base(factory) { }

        public List<ExpensesPaidTo> ListExpensesPaidTo { get; set; } = new();

        [BindProperty] public ExpensesPaidTo NewRow { get; set; } = new();
        [BindProperty] public ExpensesPaidTo EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }

        private async Task LoadDataAsync()
        {
            var url = BaseUrl + "GetExpensesPaidTo";

            var request = new ExpensesPaidTo
            {
                ExpensesPaidToId = "",
                ExpensesPaidToName = Search ?? "",
                PageIndex = 1,
                PageSize = 50
            };

            ListExpensesPaidTo =
                await PostApiAsync<List<ExpensesPaidTo>>(url, request)
                ?? new List<ExpensesPaidTo>();
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
            var dto = new ExpensesPaidTo
            {
                ExpensesPaidToId = "0",
                ExpensesPaidToName = NewRow.ExpensesPaidToName,
                ExpensesPaidToCode = NewRow.ExpensesPaidToCode,
                IsActive = NewRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SaveExpensesPaidTo";

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
            EditRow = ListExpensesPaidTo.First(x => x.ExpensesPaidToId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new ExpensesPaidTo
            {
                ExpensesPaidToId = id,
                ExpensesPaidToName = EditRow.ExpensesPaidToName,
                ExpensesPaidToCode = EditRow.ExpensesPaidToCode,
                IsActive = EditRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SaveExpensesPaidTo";

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
