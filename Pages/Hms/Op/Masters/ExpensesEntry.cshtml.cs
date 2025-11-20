using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Pages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

public class ExpensesEntryModel : BasePageModel
{
    string BaseUrl = "/api/ExpenseEntry/";

    [BindProperty]
    public ExpensesEntry Row { get; set; } = new();

    [BindProperty]
    public List<ExpensesEntry> Grid { get; set; } = new();

    public decimal TotalAmount => Grid.Sum(x => x.PaidAmount);

    public List<SelectListItem> ExpensesTypeList { get; set; } = new();
    public List<SelectListItem> PaidToList { get; set; } = new();
    public List<SelectListItem> PurposeList { get; set; } = new();

    public ExpensesEntryModel(IHttpClientFactory factory) : base(factory) { }

    public async Task OnGetAsync()
    {
        await LoadDropdownsAsync();
    }

    private async Task LoadDropdownsAsync()
    {
        ExpensesTypeList = await LoadDD<ExpensesType>("/api/ExpensesTypes/FillExpensesTypes", x => x.ExpensesTypeId!, x => x.ExpensesTypeName!);
        PaidToList = await LoadDD<ExpensesPaidTo>("/api/ExpensesPaidTo/FillExpensesPaidTo", x => x.ExpensesPaidToId!, x => x.ExpensesPaidToName!);
        PurposeList = await LoadDD<ExpensesPurpose>("/api/ExpensesPurpose/FillExpensesPurpose", x => x.ExpensesPurposeId!, x => x.ExpensesPurposeName!);
    }

    private async Task<List<SelectListItem>> LoadDD<T>(string url, Func<T, string> val, Func<T, string> txt)
    {
        var data = await PostApiAsync<List<T>>(url, new { });
        return data?.Select(x => new SelectListItem
        {
            Value = val(x),
            Text = txt(x)
        }).ToList() ?? new();
    }

    // ADD ROW
    public async Task<IActionResult> OnPostAddAsync()
    {
        await LoadDropdownsAsync();

        // Add NEW row from form
        Grid.Add(new ExpensesEntry
        {
            ExpensesTypeId = Row.ExpensesTypeId,
            ExpensesPaidToId = Row.ExpensesPaidToId,
            ExpensesPurposeId = Row.ExpensesPurposeId,
            PaidAmount = Row.PaidAmount,
            PaidDate = Row.PaidDate,
            Description = Row.Description,
            ExpensesPaidToName = PaidToList.FirstOrDefault(t => t.Value == Row.ExpensesPaidToId)?.Text,
            ExpensesPurposeName = PurposeList.FirstOrDefault(t => t.Value == Row.ExpensesPurposeId)?.Text
        });

        // CLEAR FORM
        Row = new ExpensesEntry();

        return Page();
    }

    // DELETE ROW
    public async Task<IActionResult> OnPostDeleteAsync(int index)
    {
        await LoadDropdownsAsync();

        if (index >= 0 && index < Grid.Count)
            Grid.RemoveAt(index);

        return Page();
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        await LoadDropdownsAsync();

        // 1️⃣ Convert HEADER (Row → ExpenseHeaderDto)
        var header = new ExpenseHeaderDto
        {
            ExpenseHeaderID = "",
            ExpenseDate = DateTime.Now, // you choose or Row.PaidDate?
            TotalAmount = Grid.Sum(x => x.PaidAmount),
            Remarks = Row.Description!.ToString(),

            CreatedByName = HttpContext.Session.GetString("gUserName"),
            CreatedById = HttpContext.Session.GetString("gUserId"),
            WorkstationId = HttpContext.Session.GetString("gTerminalId"),
        };

        // 2️⃣ Convert DETAILS (Grid → List<ExpenseDetailDto>)
        var details = Grid.Select(x => new ExpenseDetailDto
        {
            //ExpenseDetailsID = "",
            //ExpensesTypeId = x.ExpensesTypeId!.ToString(),
            //ExpensesPaidToId = x.ExpensesPaidToId!.ToString(),
            //ExpensesPurposeId = x.ExpensesPurposeId!.ToString(),
            //PaidAmount = x.PaidAmount,
            //PaidDate = x.PaidDate,
            //Description = x.Description


            ExpenseDetailsID = "",

            // Safe conversion
            //ExpensesTypeId = x.ExpensesTypeId ?? "0",
            //ExpensesPaidToId = x.ExpensesPaidToId ?? "0",
            //ExpensesPurposeId = x.ExpensesPurposeId ?? "0",


            ExpensesTypeId =  "0",
            ExpensesPaidToId =   "0",
            ExpensesPurposeId =  "0",

            PaidAmount = x.PaidAmount,
            PaidDate = x.PaidDate,
            Description = x.Description ?? ""
        }).ToList();

        // 3️⃣ Build main DTO
        var dto = new ExpenseDto
        {
            Header = header,
            Details = details
        };

        // 4️⃣ Call API
        var url = BaseUrl + "SaveExpenseEntry";
        var result = await PostApiAsync<object>(url, dto);

        if (result == null)
        {
            ModelState.AddModelError("", "Save failed");
            return Page();
        }

        return RedirectToPage();
    }


    // SAVE ALL TO DATABASE
    public async Task<IActionResult> OnPostSaveAsyncOld()
    {
        await LoadDropdownsAsync();

        var dto = new
        {
            Header = Row,
            Details = Grid
        };

        //var result = await PostApiAsync<object>("/api/ExpenseEntry/SaveExpenseEntry", dto);
        //var result = await PostApiAsync("api/ExpenseEntry/SaveExpenseEntry", dto);
        //var result = await PostApiAsync<object>("ExpenseEntry/SaveExpenseEntry", dto);


        var url = BaseUrl + "SaveExpenseEntry";
        var result = await PostApiAsync<object>(url, dto);

        if (result == null)
        {
            ModelState.AddModelError("", "Save failed");
            return Page();
        }

        return RedirectToPage();
    }
}
