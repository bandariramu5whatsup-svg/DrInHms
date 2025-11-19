using HanuMediSoftCore.Models.Hms.Op.Masters; 
using Microsoft.AspNetCore.Mvc; 

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class DepartmentsModel :  BasePageModel
    {
        public DepartmentsModel(IHttpClientFactory factory) : base(factory) { }

    public List<Department> ListDepartments { get; set; } = new();

    [BindProperty] public Department NewRow { get; set; } = new();
    [BindProperty] public Department EditRow { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public bool ShowNewRow { get; set; }
    public string? EditingId { get; set; }
    private async Task LoadUnitsAsync()
    {
        var Url = "/api/Units/GetUnits";

        var request = new Department
        {
            DepartmentId = "",
            DepartmentName = Search ?? "",
            IsActive = 1,
            PageIndex = 1,
            PageSize = 50
        };

            ListDepartments = await PostApiAsync<List<Department>>(Url, request)
                    ?? new List<Department>();
    }
    public async Task OnGetAsync()
    {
        await LoadUnitsAsync();
    }
    public async Task<IActionResult> OnPostAddNewAsync()
    {
        await LoadUnitsAsync();
        ShowNewRow = true;
        return Page();
    }

    public IActionResult OnPostCancelNew() => RedirectToPage();

    public async Task<IActionResult> OnPostSaveNewAsync()
    {
        var dto = new Department
        {
            DepartmentId = "0",
            DepartmentName = NewRow.DepartmentName,
            DepartmentCode = NewRow.DepartmentCode,
            IsActive = NewRow.IsActive,
            CreatedById = HttpContext.Session.GetString("gUserId"),
            CreatedByName = HttpContext.Session.GetString("gUserName"),
            WorkstationId = HttpContext.Session.GetString("gTerminalId")
        };

        var result = await PostApiAsync<object>("/api/Units/SaveUnits", dto);

        if (result == null)
        {
            ModelState.AddModelError("", "Insert failed.");

            await LoadUnitsAsync();
            ShowNewRow = true;

            return Page();
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync(string id)
    {
        await LoadUnitsAsync();
        EditingId = id;

        EditRow = ListDepartments.First(x => x.DepartmentId == id);
        return Page();
    }

    public IActionResult OnPostCancelEdit() => RedirectToPage();

    public async Task<IActionResult> OnPostSaveEditAsync(string id)
    {
        var dto = new Department
        {
            DepartmentId = id,
            DepartmentName = EditRow.DepartmentName,
            DepartmentCode = EditRow.DepartmentCode,
            IsActive = EditRow.IsActive,
            CreatedById = HttpContext.Session.GetString("gUserId"),
            CreatedByName = HttpContext.Session.GetString("gUserName"),
            WorkstationId = HttpContext.Session.GetString("gTerminalId")
        };

        var result = await PostApiAsync<object>("/api/Units/SaveUnits", dto);

        if (result == null)
        {
            ModelState.AddModelError("", "Update failed.");

            await LoadUnitsAsync();
            EditingId = id;

            return Page();
        }

        return RedirectToPage();
    }
}
}
