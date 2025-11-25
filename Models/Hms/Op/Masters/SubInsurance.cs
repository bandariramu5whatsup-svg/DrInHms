namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class SubInsurance
    {
        public string? SubInsuranceId { get; set; }
        public string? SubInsuranceName { get; set; }
        public string? SubInsuranceCode { get; set; }
        public string? InsuranceId { get; set; }

        public int IsActive { get; set; }

        public string? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }
        public string? WorkstationId { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
