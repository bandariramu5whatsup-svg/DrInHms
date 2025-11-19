
GO
CREATE SEQUENCE AreaId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO

CREATE TABLE Areas 
(
    AreaId            VARCHAR(20)      NOT NULL PRIMARY KEY,
    AreaName          VARCHAR(100)     NULL,
    AreaCode          VARCHAR(20)      NULL,
     IsActive         NUMERIC(1),
    IsDefaultArea      NUMERIC(1),
    CountryId         VARCHAR(30)      NULL,
    StateId           VARCHAR(20)      NULL,
    DistrictId        VARCHAR(20)      NULL,

    -- New Standard Audit Columns
    CreatedAt         DATETIME         DEFAULT(GETDATE()),
    UpdatedAt         DATETIME         NULL,
    CreatedByName     VARCHAR(60)      NULL,
    UpdatedByName     VARCHAR(60)      NULL,
    CreatedById       VARCHAR(60)      NULL,
    UpdatedById       VARCHAR(60)      NULL,
    WorkstationId     VARCHAR(60)      NULL
);
GO
 