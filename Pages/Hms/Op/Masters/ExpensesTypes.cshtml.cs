using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class ExpensesTypesModel : BasePageModel
    {
        string BaseUrl = "/api/ExpensesTypes/";

        public ExpensesTypesModel(IHttpClientFactory factory) : base(factory) { }

        public List<ExpensesType> ListExpensesTypes { get; set; } = new();

        [BindProperty] public ExpensesType NewRow { get; set; } = new();
        [BindProperty] public ExpensesType EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }

        private async Task LoadDataAsync()
        {
            var url = BaseUrl + "GetExpensesTypes";

            var req = new ExpensesType
            {
                ExpensesTypeId = "",
                ExpensesTypeName = Search ?? "",
                PageIndex = 1,
                PageSize = 50
            };

            ListExpensesTypes =
                await PostApiAsync<List<ExpensesType>>(url, req)
                ?? new List<ExpensesType>();
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
            var dto = new ExpensesType
            {
                ExpensesTypeId = "0",
                ExpensesTypeName = NewRow.ExpensesTypeName,
                ExpensesTypeCode = NewRow.ExpensesTypeCode,
                IsActive = NewRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId"),
            };

            var url = BaseUrl + "SaveExpensesTypes";

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
            EditRow = ListExpensesTypes.First(x => x.ExpensesTypeId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new ExpensesType
            {
                ExpensesTypeId = id,
                ExpensesTypeName = EditRow.ExpensesTypeName,
                ExpensesTypeCode = EditRow.ExpensesTypeCode,
                IsActive = EditRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId"),
            };

            var url = BaseUrl + "SaveExpensesTypes";

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
