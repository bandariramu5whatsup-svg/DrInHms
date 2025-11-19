
namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class District
    {
        public string? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string? DistrictCode { get; set; }
        public int IsActive { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int IsDefaultDistrict { get; set; }

        public string? CountryId { get; set; }
        public string? StateId { get; set; }
        public string? StateName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }
        public string? WorkstationId { get; set; }
    }
}