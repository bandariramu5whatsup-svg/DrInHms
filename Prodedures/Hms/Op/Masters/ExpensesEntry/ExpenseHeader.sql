GO
CREATE SEQUENCE ExpenseHeaderID
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO

CREATE TABLE ExpenseHeader
(
    ExpenseHeaderID VARCHAR(50) PRIMARY KEY DEFAULT('HDR' + RIGHT('' + CAST(NEXT VALUE FOR ExpenseHeaderID AS VARCHAR(5)), 5)),
    ExpenseDate     DATE        NOT NULL,
    EmployeeID      INT         NULL,
    TotalAmount     DECIMAL(18,2) NULL,
    Remarks         VARCHAR(500) NULL,

    CreatedAt       DATETIME DEFAULT(GETDATE()),
    UpdatedAt       DATETIME NULL,
    CreatedByName   VARCHAR(60) NULL,
    UpdatedByName   VARCHAR(60) NULL,
    CreatedById     VARCHAR(60) NULL,
    UpdatedById     VARCHAR(60) NULL,
    WorkstationId   VARCHAR(60) NULL
);
GO
