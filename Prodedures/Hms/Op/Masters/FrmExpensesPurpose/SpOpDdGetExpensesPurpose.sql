
GO
CREATE OR ALTER PROCEDURE SpOpDdGetExpensesPurpose
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ExpensesPurposeId,
        ExpensesPurposeName
    FROM ExpensesPurpose
    --WHERE IsActive = 1
    ORDER BY ExpensesPurposeName;
END
GO
