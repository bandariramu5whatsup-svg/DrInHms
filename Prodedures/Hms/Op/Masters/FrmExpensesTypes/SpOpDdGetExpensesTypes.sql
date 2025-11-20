GO
CREATE OR ALTER PROCEDURE SpOpDdGetExpensesTypes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ExpensesTypeId,
        ExpensesTypeName
    FROM ExpensesTypes
    WHERE IsActive = 1
    ORDER BY ExpensesTypeName;
END
GO