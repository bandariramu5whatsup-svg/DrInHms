using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class ExpensesPaidToModel : BasePageModel
    {
        private readonly ILogger<ExpensesPaidToModel> _log;

        const string BaseUrl = "/api/ExpensesPaidTo/";

        public ExpensesPaidToModel(IHttpClientFactory factory, ILogger<ExpensesPaidToModel> logger)
            : base(factory)
        {
            _log = logger;
        }

        public List<ExpensesPaidTo> ListExpensesPaidTo { get; set; } = new();

        [BindProperty] public ExpensesPaidTo NewRow { get; set; } = new();
        [BindProperty] public ExpensesPaidTo EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }

        private async Task LoadDataAsync()
        {
            try
            {
                var request = new ExpensesPaidTo
                {
                    ExpensesPaidToId = "",
                    ExpensesPaidToName = Search ?? "",
                    PageIndex = 1,
                    PageSize = 50
                };

                ListExpensesPaidTo =
                    await PostApiAsync<List<ExpensesPaidTo>>(BaseUrl + "GetExpensesPaidTo", request)
                    ?? new List<ExpensesPaidTo>();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Failed to load ExpensesPaidTo list.");
                ListExpensesPaidTo = new();
            }
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
            if (!ModelState.IsValid)
            {
                await LoadDataAsync();
                ShowNewRow = true;
                return Page();
            }

            var dto = CreateDto("0", NewRow);

            var success = await SaveToApiAsync(dto);
            if (!success)
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
            EditRow = ListExpensesPaidTo.FirstOrDefault(x => x.ExpensesPaidToId == id) ?? new();
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                await LoadDataAsync();
                EditingId = id;
                return Page();
            }

            var dto = CreateDto(id, EditRow);

            var success = await SaveToApiAsync(dto);
            if (!success)
            {
                ModelState.AddModelError("", "Update failed.");
                await LoadDataAsync();
                EditingId = id;
                return Page();
            }

            return RedirectToPage();
        }

        // Shared DTO creator
        private ExpensesPaidTo CreateDto(string id, ExpensesPaidTo source)
        {
            return new ExpensesPaidTo
            {
                ExpensesPaidToId = id,
                ExpensesPaidToName = source.ExpensesPaidToName,
                ExpensesPaidToCode = source.ExpensesPaidToCode,
                IsActive = source.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };
        }

        // Shared save method
        private async Task<bool> SaveToApiAsync(ExpensesPaidTo dto)
        {
            try
            {
                var result = await PostApiAsync<object>(BaseUrl + "SaveExpensesPaidTo", dto);
                return result != null;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Save operation failed.");
                return false;
            }
        }
    }
}
