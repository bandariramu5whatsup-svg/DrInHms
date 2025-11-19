
namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class Place
    {
        public string? PlaceId { get; set; }
        public string? PlaceName { get; set; }
        public string? PlaceCode { get; set; }
        public int IsActive { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool IsDefaultPlace { get; set; }

        public string? AreaId { get; set; }
        public string? DistrictId { get; set; }
        public string? StateId { get; set; }
        public string? CountryId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }
        public string? WorkstationId { get; set; }
    }
}