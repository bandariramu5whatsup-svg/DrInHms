-- States Table (PascalCase + Audit Fields)
GO
CREATE SEQUENCE StateId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO
CREATE TABLE States (
    StateId         VARCHAR(20)      NOT NULL PRIMARY KEY,
    StateName       VARCHAR(100)     NULL,
    StateCode       VARCHAR(20)      NULL,
    IsActive         NUMERIC(1),
    IsDefaultState   NUMERIC(1),

    CountryId       VARCHAR(30)      NULL,
    CountryName     VARCHAR(150)     NULL,

    CreatedAt       DATETIME         DEFAULT(GETDATE()),
    UpdatedAt       DATETIME         NULL,
    CreatedById     VARCHAR(60)      NULL,
    CreatedByName   VARCHAR(100)     NULL,
    UpdatedById     VARCHAR(60)      NULL,
    UpdatedByName   VARCHAR(100)     NULL,
    WorkstationId   VARCHAR(60)      NULL
);
GO