-- Districts Table (PascalCase + Audit Fields)
go
CREATE SEQUENCE DistrictId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
go
CREATE TABLE Districts (
    DistrictId        VARCHAR(20)      NOT NULL PRIMARY KEY,
    DistrictName      VARCHAR(100)     NULL,
    DistrictCode      VARCHAR(20)      NULL,
    IsActive          NUMERIC(1),
    IsDefaultDistrict  NUMERIC(1),

    CountryId         VARCHAR(30)      NULL,
    StateId           VARCHAR(20)      NULL,
    StateName         VARCHAR(150)     NULL,

    CreatedAt         DATETIME         DEFAULT(GETDATE()),
    UpdatedAt         DATETIME         NULL,
    CreatedById       VARCHAR(60)      NULL,
    CreatedByName     VARCHAR(100)     NULL,
    UpdatedById       VARCHAR(60)      NULL,
    UpdatedByName     VARCHAR(100)     NULL,
    WorkstationId     VARCHAR(60)      NULL
);
go