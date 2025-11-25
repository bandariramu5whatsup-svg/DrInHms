GO
CREATE SEQUENCE ConsultantDoctorId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO


create table ConsultantDoctors
(
 ConsultantDoctorId VARCHAR(20) NOT NULL PRIMARY KEY,
 ConsultantDoctorName VARCHAR(300),
 DoctorRegNo VARCHAR(300),
 DepartmentId VARCHAR(50),  --- from Departments Table
 SpecilizationId VARCHAR(50),  --- from Specilization Table
 Qualification VARCHAR(200),  --- FreeText
 MobileNO  VARCHAR(20),
 Age  numeric(10),
 Gender  numeric(1),   --- 0-female ,1-Male , 2-Others
 ConsultTimeInMinutes  numeric(10),
 ConsultTimeFromInterval  TIME,   -- Ex 10 am or 11 am
 ConsultTimeToInterval  TIME,   -- Ex 7Pm am or 8 Pm
 Active     numeric(1),    --1 Active , 0- Inactive
 ApplyReviewFeeAfterRegExpired     numeric(1),
 ApplyValidityConsultationsOldPatients     numeric(1),


 -- Fee Details
    GeneralConsultationFee        DECIMAL(10,2) NULL,   
    GeneralFeeToHospital          DECIMAL(10,2) NULL,
    GeneralFeeToDoctor            DECIMAL(10,2) NULL,

    GeneralReviewFee              DECIMAL(10,2) NULL,
    GeneralReviewFeeToHospital    DECIMAL(10,2) NULL,
    GeneralReviewFeeToDoctor      DECIMAL(10,2) NULL,

    EmergencyConsultationFee      DECIMAL(10,2) NULL,   
    EmergencyFeeToHospital        DECIMAL(10,2) NULL,
    EmergencyFeeToDoctor          DECIMAL(10,2) NULL,


    EmergencyReviewFee            DECIMAL(10,2) NULL,
    EmergencyReviewFeeToHospital  DECIMAL(10,2) NULL,
    EmergencyReviewFeeToDoctor    DECIMAL(10,2) NULL,

    IPConsultationFee             DECIMAL(10,2) NULL,
    IPFeeToHospital               DECIMAL(10,2) NULL,
    IPFeeToDoctor                 DECIMAL(10,2) NULL,

    -- Validity Conditions
    NoOfFeeDays                   INT NULL,
    NoOfReviews                   INT NULL,
   -- New Standard Audit Columns
  CreatedAt         DATETIME         DEFAULT(GETDATE()),
  UpdatedAt         DATETIME         NULL,
  CreatedByName     VARCHAR(60)      NULL,
  UpdatedByName     VARCHAR(60)      NULL,
  CreatedById       VARCHAR(60)      NULL,
  UpdatedById       VARCHAR(60)      NULL,
  WorkstationId     VARCHAR(60)      NULL
)
GO