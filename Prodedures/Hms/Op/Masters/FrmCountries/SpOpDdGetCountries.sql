CREATE PROCEDURE SpOpDdGetCountries
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CountryId,
        CountryName
    FROM Countries
    WHERE IsActive = 1   -- remove this if you want ALL
    ORDER BY CountryName;
END
GO
