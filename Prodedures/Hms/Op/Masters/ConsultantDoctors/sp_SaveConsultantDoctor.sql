GO
CREATE OR ALTER PROCEDURE sp_SaveConsultantDoctor
(
    @ConsultantDoctorId VARCHAR(20) = NULL,
    @ConsultantDoctorName VARCHAR(300),
    @DoctorRegNo VARCHAR(300),
    @DepartmentId VARCHAR(50),
    @SpecilizationId VARCHAR(50),
    @Qualification VARCHAR(200),

    @MobileNO VARCHAR(20),
    @Age INT = NULL,
    @Gender INT = NULL,
    @ConsultTimeInMinutes INT = NULL,
    @ConsultTimeFromInterval TIME = NULL,
    @ConsultTimeToInterval TIME = NULL,

    @Active INT = 1,
    @ApplyReviewFeeAfterRegExpired INT = 0,
    @ApplyValidityConsultationsOldPatients INT = 0,

    @GeneralConsultationFee DECIMAL(10,2) = NULL,
    @GeneralFeeToHospital DECIMAL(10,2) = NULL,
    @GeneralFeeToDoctor DECIMAL(10,2) = NULL,
    @GeneralReviewFee DECIMAL(10,2) = NULL,
    @GeneralReviewFeeToHospital DECIMAL(10,2) = NULL,
    @GeneralReviewFeeToDoctor DECIMAL(10,2) = NULL,

    @EmergencyConsultationFee DECIMAL(10,2) = NULL,
    @EmergencyFeeToHospital DECIMAL(10,2) = NULL,
    @EmergencyFeeToDoctor DECIMAL(10,2) = NULL,
    @EmergencyReviewFee DECIMAL(10,2) = NULL,
    @EmergencyReviewFeeToHospital DECIMAL(10,2) = NULL,
    @EmergencyReviewFeeToDoctor DECIMAL(10,2) = NULL,

    @IPConsultationFee DECIMAL(10,2) = NULL,
    @IPFeeToHospital DECIMAL(10,2) = NULL,
    @IPFeeToDoctor DECIMAL(10,2) = NULL,

    @NoOfFeeDays INT = NULL,
    @NoOfReviews INT = NULL,

    @CreatedByName VARCHAR(60),
    @CreatedById VARCHAR(60),
    @WorkstationId VARCHAR(60)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IsNew BIT = 0;

    IF (@ConsultantDoctorId IS NULL OR LTRIM(RTRIM(@ConsultantDoctorId)) = '')
    BEGIN
        SET @IsNew = 1;
        DECLARE @Seq BIGINT = NEXT VALUE FOR ConsultantDoctorId;
        SET @ConsultantDoctorId = 'CD' + RIGHT('000000' + CAST(@Seq AS VARCHAR(10)), 6);
    END

    IF EXISTS(SELECT 1 FROM ConsultantDoctors WHERE ConsultantDoctorId = @ConsultantDoctorId)
    BEGIN
        -- UPDATE
        UPDATE ConsultantDoctors
        SET
            ConsultantDoctorName = @ConsultantDoctorName,
            DoctorRegNo = @DoctorRegNo,
            DepartmentId = @DepartmentId,
            SpecilizationId = @SpecilizationId,
            Qualification = @Qualification,

            MobileNO = @MobileNO,
            Age = @Age,
            Gender = @Gender,
            ConsultTimeInMinutes = @ConsultTimeInMinutes,
            ConsultTimeFromInterval = @ConsultTimeFromInterval,
            ConsultTimeToInterval = @ConsultTimeToInterval,

            Active = @Active,
            ApplyReviewFeeAfterRegExpired = @ApplyReviewFeeAfterRegExpired,
            ApplyValidityConsultationsOldPatients = @ApplyValidityConsultationsOldPatients,

            -- General Fees
            GeneralConsultationFee = @GeneralConsultationFee,
            GeneralFeeToHospital = @GeneralFeeToHospital,
            GeneralFeeToDoctor = @GeneralFeeToDoctor,
            GeneralReviewFee = @GeneralReviewFee,
            GeneralReviewFeeToHospital = @GeneralReviewFeeToHospital,
            GeneralReviewFeeToDoctor = @GeneralReviewFeeToDoctor,

            -- Emergency Fees
            EmergencyConsultationFee = @EmergencyConsultationFee,
            EmergencyFeeToHospital = @EmergencyFeeToHospital,
            EmergencyFeeToDoctor = @EmergencyFeeToDoctor,
            EmergencyReviewFee = @EmergencyReviewFee,
            EmergencyReviewFeeToHospital = @EmergencyReviewFeeToHospital,
            EmergencyReviewFeeToDoctor = @EmergencyReviewFeeToDoctor,

            -- IP Fees
            IPConsultationFee = @IPConsultationFee,
            IPFeeToHospital = @IPFeeToHospital,
            IPFeeToDoctor = @IPFeeToDoctor,

            -- Validity
            NoOfFeeDays = @NoOfFeeDays,
            NoOfReviews = @NoOfReviews,

            UpdatedAt = GETDATE(),
            UpdatedByName = @CreatedByName,
            UpdatedById = @CreatedById,
            WorkstationId = @WorkstationId
        WHERE ConsultantDoctorId = @ConsultantDoctorId;
    END
    ELSE
    BEGIN
        -- INSERT
        INSERT INTO ConsultantDoctors (
            ConsultantDoctorId, ConsultantDoctorName, DoctorRegNo, DepartmentId, SpecilizationId, Qualification,
            MobileNO, Age, Gender, ConsultTimeInMinutes, ConsultTimeFromInterval, ConsultTimeToInterval,
            Active, ApplyReviewFeeAfterRegExpired, ApplyValidityConsultationsOldPatients,
            GeneralConsultationFee, GeneralFeeToHospital, GeneralFeeToDoctor,
            GeneralReviewFee, GeneralReviewFeeToHospital, GeneralReviewFeeToDoctor,
            EmergencyConsultationFee, EmergencyFeeToHospital, EmergencyFeeToDoctor,
            EmergencyReviewFee, EmergencyReviewFeeToHospital, EmergencyReviewFeeToDoctor,
            IPConsultationFee, IPFeeToHospital, IPFeeToDoctor,
            NoOfFeeDays, NoOfReviews,
            CreatedAt, CreatedByName, CreatedById, WorkstationId
        )
        VALUES (
            @ConsultantDoctorId, @ConsultantDoctorName, @DoctorRegNo, @DepartmentId, @SpecilizationId, @Qualification,
            @MobileNO, @Age, @Gender, @ConsultTimeInMinutes, @ConsultTimeFromInterval, @ConsultTimeToInterval,
            @Active, @ApplyReviewFeeAfterRegExpired, @ApplyValidityConsultationsOldPatients,
            @GeneralConsultationFee, @GeneralFeeToHospital, @GeneralFeeToDoctor,
            @GeneralReviewFee, @GeneralReviewFeeToHospital, @GeneralReviewFeeToDoctor,
            @EmergencyConsultationFee, @EmergencyFeeToHospital, @EmergencyFeeToDoctor,
            @EmergencyReviewFee, @EmergencyReviewFeeToHospital, @EmergencyReviewFeeToDoctor,
            @IPConsultationFee, @IPFeeToHospital, @IPFeeToDoctor,
            @NoOfFeeDays, @NoOfReviews,
            GETDATE(), @CreatedByName, @CreatedById, @WorkstationId
        );
    END

    SELECT @ConsultantDoctorId AS ConsultantDoctorId, @IsNew AS IsNew;
END
GO
