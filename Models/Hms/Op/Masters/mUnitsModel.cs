namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
     

    public class UnitsDto
    {
        public string? UNIT_ID { get; set; }
        public string? UNITS { get; set; }
        public string? DESCRIPTION { get; set; }
        public int? ACTIVE { get; set; }

        public string? ENTRY_USER_NAME { get; set; }
        public string? ENTRY_USER_ID { get; set; }
        public string? TERMINAL_ID { get; set; }

        public int PAGE_INDEX { get; set; }
        public int PAGE_SIZE { get; set; }
    }
}
