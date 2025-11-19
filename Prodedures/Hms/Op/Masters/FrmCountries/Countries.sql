
GO
CREATE SEQUENCE CountryId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO

CREATE TABLE Countries
(
    CountryId         VARCHAR(30)     NOT NULL PRIMARY KEY,
    CountryName       VARCHAR(100)    NULL,
    CountryCode       VARCHAR(20)     NULL,
    IsActive           NUMERIC(1),
    IsDefaultCountry   NUMERIC(1),

    CreatedAt         DATETIME        DEFAULT(GETDATE()),
    UpdatedAt         DATETIME        NULL,
    CreatedById       VARCHAR(60)     NULL,
    CreatedByName     VARCHAR(100)    NULL,
    UpdatedById       VARCHAR(60)     NULL,
    UpdatedByName     VARCHAR(100)    NULL,
    WorkstationId     VARCHAR(100)    NULL
);
GO
 