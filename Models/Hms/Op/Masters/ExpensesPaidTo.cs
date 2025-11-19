namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class ExpensesPaidTo
    {
        public string? ExpensesPaidToId { get; set; }
        public string? ExpensesPaidToName { get; set; }
        public string? ExpensesPaidToCode { get; set; }
        public int IsActive { get; set; }

        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }
        public string? WorkstationId { get; set; }

        // Paging
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
