GO
CREATE PROCEDURE SpOpDdGetStates
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        StateId,
        StateName
    FROM States
    WHERE IsActive = 1   -- remove this if you want ALL
    ORDER BY StateName;
END
GO
