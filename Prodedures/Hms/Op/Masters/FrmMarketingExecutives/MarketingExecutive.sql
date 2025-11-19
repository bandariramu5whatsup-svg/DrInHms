-- MarketingExecutive Table (PascalCase + Audit Fields)
CREATE SEQUENCE MarketingExecutiveId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;

CREATE TABLE MarketingExecutive (
    MarketingExecutiveId   VARCHAR(30)      NOT NULL PRIMARY KEY,
    MarketingPersonName    VARCHAR(300)     NULL,
    MailId                 VARCHAR(150)     NULL,
    Mobile                 VARCHAR(20)      NULL,
    Dob                    DATETIME         NULL,
    Address                VARCHAR(250)     NULL,
    CountryId              VARCHAR(30)      NULL,
    StateId                VARCHAR(30)      NULL,
    DistrictId             VARCHAR(30)      NULL,
    AreaId                 VARCHAR(30)      NULL,
    PlaceId                VARCHAR(30)      NULL,
    IsActive                NUMERIC(1),

    CreatedAt              DATETIME         DEFAULT(GETDATE()),
    UpdatedAt              DATETIME         NULL,
    CreatedById            VARCHAR(60)      NULL,
    CreatedByName          VARCHAR(100)     NULL,
    UpdatedById            VARCHAR(60)      NULL,
    UpdatedByName          VARCHAR(100)     NULL,
    WorkstationId          VARCHAR(100)     NULL
);
