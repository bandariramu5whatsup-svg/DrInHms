namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class PayCategories
    {
        public string? PayCategoryId { get; set; }
        public string? PayCategoryName { get; set; }
        public string? PayCategoryCode { get; set; }
        public string? PayCategoryOption { get; set; }      // 0=Cash, 1=Credit, 2=Free, 3=Insurance
        public string? PayCategoryOptionText { get; set; }  // Display text

        public int IsActive { get; set; }

        // Tracking fields (common in your project)
        public string? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }
        public string? WorkstationId { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
