using HanuMediSoftCore.Models.Hms.Op.Masters; 
using Microsoft.AspNetCore.Mvc; 

namespace HanuMediSoftCore.Pages.Hms.Op.Masters
{
    public class SpecializationModel :  BasePageModel
    {
        public SpecializationModel(IHttpClientFactory factory) : base(factory) { }

    public List<Specialization> ListSpecializations { get; set; } = new();

    [BindProperty] public Specialization NewRow { get; set; } = new();
    [BindProperty] public Specialization EditRow { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public bool ShowNewRow { get; set; }
    public string? EditingId { get; set; }
    private async Task LoadUnitsAsync()
    {
        var Url = "/api/Units/GetUnits";

        var request = new Specialization
        {
            SpecializationId = "",
            SpecializationName = Search ?? "",
            IsActive = 1,
            PageIndex = 1,
            PageSize = 50
        };

            ListSpecializations = await PostApiAsync<List<Specialization>>(Url, request)
                    ?? new List<Specialization>();
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
        var dto = new Specialization
        {
            SpecializationId = "0",
            SpecializationName = NewRow.SpecializationName,
            SpecializationCode = NewRow.SpecializationCode,
            DepartmentId = NewRow.DepartmentId,
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

        EditRow = ListSpecializations.First(x => x.SpecializationId == id);
        return Page();
    }

    public IActionResult OnPostCancelEdit() => RedirectToPage();

    public async Task<IActionResult> OnPostSaveEditAsync(string id)
    {
        var dto = new Specialization
        {
            SpecializationId = id,
            SpecializationName = EditRow.SpecializationName,
            SpecializationCode = EditRow.SpecializationCode,
            DepartmentId = EditRow.DepartmentId,
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
