namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class ExpensesType
    {
        public string? ExpensesTypeId { get; set; }
        public string? ExpensesTypeName { get; set; }
        public string? ExpensesTypeCode { get; set; }
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
