--GO
--CREATE OR ALTER PROCEDURE SaveExpenseEntry
--(
--    @ExpenseDate       DATE,
--    @EmployeeID        VARCHAR(500),
--    @TotalAmount       DECIMAL(18,2) = NULL,
--    @Remarks           VARCHAR(500),

--    @CreatedByName     VARCHAR(60),
--    @CreatedById       VARCHAR(60),
--    @WorkstationId     VARCHAR(60),

--    @DetailsJson       NVARCHAR(MAX),

--    @NewHeaderID       VARCHAR(50) OUTPUT
--)
--AS
--BEGIN
--    SET NOCOUNT ON;
--    BEGIN TRAN;

--    ------------------------------------------
--    -- Insert HEADER
--    ------------------------------------------
--    INSERT INTO ExpenseHeader
--    (
--        ExpenseDate,
--        EmployeeID,
--        TotalAmount,
--        Remarks,
--        CreatedByName,
--        CreatedById,
--        WorkstationId
--    )
--    VALUES
--    (
--        @ExpenseDate,
--        @EmployeeID,
--        @TotalAmount,
--        @Remarks,
--        @CreatedByName,
--        @CreatedById,
--        @WorkstationId
--    );

--    SELECT @NewHeaderID = ExpenseHeaderID
--    FROM ExpenseHeader
--    WHERE ExpenseHeaderID = SCOPE_IDENTITY();

--    ------------------------------------------
--    -- Insert DETAILS (Based on your DB schema)
--    ------------------------------------------
--    INSERT INTO ExpenseDetails
--    (
--        ExpenseHeaderID,
--        ExpenseTypeID,
--        Amount,
--        Remarks,
--        CreatedByName,
--        CreatedById,
--        WorkstationId
--    )
--    SELECT
--        @NewHeaderID,
--        JSON_VALUE(value, '$.ExpensesTypeId'),
--        JSON_VALUE(value, '$.PaidAmount'),
--        JSON_VALUE(value, '$.Description'),
--        @CreatedByName,
--        @CreatedById,
--        @WorkstationId
--    FROM OPENJSON(@DetailsJson);

--    COMMIT;
--END
--GO

GO
CREATE OR ALTER PROCEDURE SaveExpenseEntry
(
    @ExpenseDate       DATE,
    @EmployeeID        INT = NULL,
    @TotalAmount       DECIMAL(18,2),
    @Remarks           VARCHAR(500),

    @CreatedByName     VARCHAR(60),
    @CreatedById       VARCHAR(60),
    @WorkstationId     VARCHAR(60),

    @DetailsJson       NVARCHAR(MAX),

    @NewHeaderID       VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;

    ------------------------------------------
    -- 1️⃣ Insert HEADER
    ------------------------------------------
    INSERT INTO ExpenseHeader
    (
        ExpenseDate,
        EmployeeID,
        TotalAmount,
        Remarks,
        CreatedByName,
        CreatedById,
        WorkstationId
    )
    VALUES
    (
        @ExpenseDate,
        @EmployeeID,
        @TotalAmount,
        @Remarks,
        @CreatedByName,
        @CreatedById,
        @WorkstationId
    );

    ------------------------------------------
    -- 2️⃣ Get NEW HeaderID (Correct for VARCHAR PK)
    ------------------------------------------
    SELECT TOP 1 @NewHeaderID = ExpenseHeaderID
    FROM ExpenseHeader
    ORDER BY CreatedAt DESC;

    ------------------------------------------
    -- 3️⃣ Insert DETAILS (CAST all numeric values)
    ------------------------------------------
    INSERT INTO ExpenseDetails
    (
        ExpenseHeaderID,
        ExpenseTypeID,
        Amount,
        Remarks,
        CreatedByName,
        CreatedById,
        WorkstationId
    )
    SELECT
        @NewHeaderID,
        CAST(JSON_VALUE(value, '$.ExpensesTypeId') AS INT),
        CAST(JSON_VALUE(value, '$.PaidAmount') AS DECIMAL(18,2)),
        JSON_VALUE(value, '$.Description'),
        @CreatedByName,
        @CreatedById,
        @WorkstationId
    FROM OPENJSON(@DetailsJson);

    COMMIT;
END
GO



 