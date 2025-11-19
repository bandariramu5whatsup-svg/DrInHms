GO
CREATE OR ALTER PROCEDURE SpOpGetExpensesPurpose
(
    @ExpensesPurposeId    VARCHAR(30) = NULL,
    @ExpensesPurposeName  VARCHAR(100) = NULL,
    @IsActive             NUMERIC(1) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ExpensesPurposeId,
        ExpensesPurposeName,
        ExpensesPurposeCode,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM ExpensesPurpose
    WHERE
        (@ExpensesPurposeId IS NULL OR @ExpensesPurposeId = '' OR ExpensesPurposeId = @ExpensesPurposeId)
        AND (@ExpensesPurposeName IS NULL OR ExpensesPurposeName LIKE '%' + @ExpensesPurposeName + '%')
        AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY ExpensesPurposeName;
END;
GO
