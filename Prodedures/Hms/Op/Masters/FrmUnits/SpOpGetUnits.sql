-- Get Procedure for Units
 
GO
CREATE OR ALTER PROCEDURE SpOpGetUnits
(
    @UnitId       VARCHAR(50) = NULL,
    @UnitName     VARCHAR(200) = NULL,
    @IsActive     BIT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        UnitId,
        UnitName,
        Description,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM Units
    WHERE
        (@UnitId IS NULL OR @UnitId = '' OR UnitId = @UnitId)
        AND (@UnitName IS NULL OR UnitName LIKE '%' + @UnitName + '%')
        --AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY UnitName;
END;
GO