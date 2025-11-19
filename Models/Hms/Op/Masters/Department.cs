
namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class Department
    {
        public string? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentCode { get; set; }
        public int IsActive { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }
        public string? WorkstationId { get; set; }
    }
}