GO
CREATE SEQUENCE ExpensesTypeId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO

CREATE TABLE ExpensesTypes
(
    ExpensesTypeId         VARCHAR(30)     NOT NULL PRIMARY KEY,
    ExpensesTypeName       VARCHAR(100)    NULL,
    ExpensesTypeCode       VARCHAR(20)     NULL,
    IsActive           NUMERIC(1),
   

    CreatedAt         DATETIME        DEFAULT(GETDATE()),
    UpdatedAt         DATETIME        NULL,
    CreatedById       VARCHAR(60)     NULL,
    CreatedByName     VARCHAR(100)    NULL,
    UpdatedById       VARCHAR(60)     NULL,
    UpdatedByName     VARCHAR(100)    NULL,
    WorkstationId     VARCHAR(100)    NULL
);
GO
 