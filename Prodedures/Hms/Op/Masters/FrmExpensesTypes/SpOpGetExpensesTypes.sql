GO
CREATE OR ALTER PROCEDURE SpOpGetExpensesTypes
(
    @ExpensesTypeId    VARCHAR(30) = NULL,
    @ExpensesTypeName  VARCHAR(100) = NULL,
    @IsActive          NUMERIC(1) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ExpensesTypeId,
        ExpensesTypeName,
        ExpensesTypeCode,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM ExpensesTypes
    WHERE
        (@ExpensesTypeId IS NULL OR @ExpensesTypeId = '' OR ExpensesTypeId = @ExpensesTypeId)
        AND (@ExpensesTypeName IS NULL OR ExpensesTypeName LIKE '%' + @ExpensesTypeName + '%')
        --AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY ExpensesTypeName;
END;
GO
