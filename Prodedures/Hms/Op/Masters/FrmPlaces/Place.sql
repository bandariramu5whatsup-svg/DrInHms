-- Place Table (PascalCase + Audit Fields)
CREATE SEQUENCE PlaceId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;

CREATE TABLE Place (
    PlaceId         VARCHAR(30)     NOT NULL PRIMARY KEY,
    PlaceName       VARCHAR(100)    NULL,
    PlaceCode       VARCHAR(20)     NULL,
    IsActive         NUMERIC(1),
    IsDefaultPlace   NUMERIC(1),

    AreaId          VARCHAR(30)     NULL,
    DistrictId      VARCHAR(30)     NULL,
    StateId         VARCHAR(30)     NULL,
    CountryId       VARCHAR(30)     NULL,

    CreatedAt       DATETIME        DEFAULT(GETDATE()),
    UpdatedAt       DATETIME        NULL,
    CreatedById     VARCHAR(60)     NULL,
    CreatedByName   VARCHAR(100)    NULL,
    UpdatedById     VARCHAR(60)     NULL,
    UpdatedByName   VARCHAR(100)    NULL,
    WorkstationId   VARCHAR(100)    NULL
);
