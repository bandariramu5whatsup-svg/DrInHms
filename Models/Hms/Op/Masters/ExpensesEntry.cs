namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class ExpensesEntry
    {
        public string? ExpensesTypeId { get; set; }
        public string? ExpensesPaidToId { get; set; }
        public string? ExpensesPurposeId { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? Description { get; set; }
        public string? Remarks { get; set; }

        // For UI
        public string? ExpensesPaidToName { get; set; }
        public string? ExpensesTypeName { get; set; }
        public string? ExpensesPurposeName { get; set; }
    }
}

public class ExpenseDto
{
    public ExpenseHeaderDto? Header { get; set; }
    public List<ExpenseDetailDto>? Details { get; set; }
}

public class ExpenseHeaderDto
{
    public int? EmployeeID { get; set; }
    public string? ExpenseHeaderID { get; set; }
    public DateTime ExpenseDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Remarks { get; set; }

    public string? CreatedByName { get; set; }
    public string? CreatedById { get; set; }
    public string? WorkstationId { get; set; }
}

public class ExpenseDetailDto
{
    public string? ExpenseDetailsID { get; set; }
    public string? ExpensesTypeId { get; set; }
    public string? ExpensesPaidToId { get; set; }
    public string? ExpensesPurposeId { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime? PaidDate { get; set; }
    public string? Description { get; set; }
}


//public class ExpenseDto
//{
//    public ExpenseHeaderDto Header { get; set; }
//    public List<ExpenseDetailDto> Details { get; set; }
//}
//public class ExpenseHeaderDto
//{
//    public string ExpenseHeaderID { get; set; }   // For edit
//    public DateTime ExpenseDate { get; set; }
//    public int? EmployeeID { get; set; }
//    public decimal TotalAmount { get; set; }
//    public string Remarks { get; set; }

//    public string CreatedByName { get; set; }
//    public string CreatedById { get; set; }
//    public string WorkstationId { get; set; }
//}
//public class ExpenseDetailDto
//{
//    public string ExpenseDetailsID { get; set; }  // For edit
//    public int ExpenseTypeID { get; set; }
//    public decimal Amount { get; set; }
//    public string Remarks { get; set; }
//}
