GO
CREATE OR ALTER PROCEDURE SpOpDdGetPaidTo
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ExpensesPaidToId,
        ExpensesPaidToName
    FROM ExpensesPaidTo
    WHERE IsActive = 1
    ORDER BY ExpensesPaidToId;
END
GO
