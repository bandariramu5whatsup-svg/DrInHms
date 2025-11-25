using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class PayCategoriesModel : BasePageModel
    {
        string BaseUrl = "/api/PayCategories/";

        public PayCategoriesModel(IHttpClientFactory factory) : base(factory) { }

        public List<PayCategories> ListPayCategories { get; set; } = new();

        [BindProperty] public PayCategories NewRow { get; set; } = new();
        [BindProperty] public PayCategories EditRow { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public bool ShowNewRow { get; set; }
        public string? EditingId { get; set; }

        private async Task LoadDataAsync()
        {
            var url = BaseUrl + "GetPayCategories";

            var req = new PayCategories
            {
                PayCategoryId = "",
                PayCategoryName = Search ?? "",
                PageIndex = 1,
                PageSize = 100
            };

            ListPayCategories =
                await PostApiAsync<List<PayCategories>>(url, req)
                ?? new List<PayCategories>();
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
            var dto = new PayCategories
            {
                PayCategoryId = "0",
                PayCategoryName = NewRow.PayCategoryName,
                PayCategoryCode = NewRow.PayCategoryCode,
                PayCategoryOption = NewRow.PayCategoryOption,
                PayCategoryOptionText = GetOptionText(NewRow.PayCategoryOption),
                IsActive = NewRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SavePayCategories";
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
            EditRow = ListPayCategories.First(x => x.PayCategoryId == id);
            return Page();
        }

        public IActionResult OnPostCancelEdit() => RedirectToPage();

        public async Task<IActionResult> OnPostSaveEditAsync(string id)
        {
            var dto = new PayCategories
            {
                PayCategoryId = id,
                PayCategoryName = EditRow.PayCategoryName,
                PayCategoryCode = EditRow.PayCategoryCode,
                PayCategoryOption = EditRow.PayCategoryOption,
                PayCategoryOptionText = GetOptionText(EditRow.PayCategoryOption),
                IsActive = EditRow.IsActive,

                CreatedById = HttpContext.Session.GetString("gUserId"),
                CreatedByName = HttpContext.Session.GetString("gUserName"),
                WorkstationId = HttpContext.Session.GetString("gTerminalId")
            };

            var url = BaseUrl + "SavePayCategories";

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

        private string GetOptionText(string? opt)
        {
            return opt switch
            {
                "0" => "Cash",
                "1" => "Credit",
                "2" => "Free",
                "3" => "Insurance",
                _ => ""
            };
        }
    }
}
