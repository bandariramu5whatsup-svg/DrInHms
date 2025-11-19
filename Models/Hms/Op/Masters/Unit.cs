
namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class Unit
    {
        public string? UnitId { get; set; }
        public string? UnitName { get; set; }
        public string? Description { get; set; }
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