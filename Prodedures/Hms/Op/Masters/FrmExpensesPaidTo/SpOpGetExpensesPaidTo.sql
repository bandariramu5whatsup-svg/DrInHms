 GO
CREATE OR ALTER PROCEDURE SpOpGetExpensesPaidTo
(
    @ExpensesPaidToId    VARCHAR(30) = NULL,
    @ExpensesPaidToName  VARCHAR(100) = NULL,
    @IsActive            NUMERIC(1) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ExpensesPaidToId,
        ExpensesPaidToName,
        ExpensesPaidToCode,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM ExpensesPaidTo
    WHERE
        (@ExpensesPaidToId IS NULL OR @ExpensesPaidToId = '' OR ExpensesPaidToId = @ExpensesPaidToId)
        AND (@ExpensesPaidToName IS NULL OR ExpensesPaidToName LIKE '%' + @ExpensesPaidToName + '%')
        --AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY ExpensesPaidToName;
END;
GO
