namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    // Models/ConsultantDoctorDto.cs
    public class ConsultantDoctorDto
    {
        public string ConsultantDoctorId { get; set; }
        public string ConsultantDoctorName { get; set; }
        public string DoctorRegNo { get; set; }
        public string DepartmentId { get; set; }
        public string SpecilizationId { get; set; }
        public string Qualification { get; set; }

        public string MobileNO { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; } // 0/1/2
        public int? ConsultTimeInMinutes { get; set; }
        public TimeSpan? ConsultTimeFromInterval { get; set; }
        public TimeSpan? ConsultTimeToInterval { get; set; }

        public bool Active { get; set; }
        public bool ApplyReviewFeeAfterRegExpired { get; set; }
        public bool ApplyValidityConsultationsOldPatients { get; set; }

        // Fees — General
        public decimal? GeneralConsultationFee { get; set; }
        public decimal? GeneralFeeToHospital { get; set; }
        public decimal? GeneralFeeToDoctor { get; set; }

        public decimal? GeneralReviewFee { get; set; }
        public decimal? GeneralReviewFeeToHospital { get; set; }
        public decimal? GeneralReviewFeeToDoctor { get; set; }

        // Fees — Emergency
        public decimal? EmergencyConsultationFee { get; set; }
        public decimal? EmergencyFeeToHospital { get; set; }
        public decimal? EmergencyFeeToDoctor { get; set; }

        public decimal? EmergencyReviewFee { get; set; }
        public decimal? EmergencyReviewFeeToHospital { get; set; }
        public decimal? EmergencyReviewFeeToDoctor { get; set; }

        // Fees — IP
        public decimal? IPConsultationFee { get; set; }
        public decimal? IPFeeToHospital { get; set; }
        public decimal? IPFeeToDoctor { get; set; }

        // Validity
        public int? NoOfFeeDays { get; set; }
        public int? NoOfReviews { get; set; }

        // Audit
        public string CreatedByName { get; set; }
        public string CreatedById { get; set; }
        public string WorkstationId { get; set; }
    }




}

