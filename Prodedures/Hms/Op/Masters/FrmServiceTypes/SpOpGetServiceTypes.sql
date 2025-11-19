-- Get Procedure for ServiceTypes
GO
CREATE OR ALTER PROCEDURE SpOpGetServiceTypes
(
    @ServiceTypeId    VARCHAR(50) = NULL,
    @ServiceTypeName  VARCHAR(200) = NULL,
    @IsActive         BIT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ServiceTypeId,
        ServiceTypeName,
        Description,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM ServiceTypes
    WHERE
        (@ServiceTypeId IS NULL OR @ServiceTypeId = '' OR ServiceTypeId = @ServiceTypeId)
        AND (@ServiceTypeName IS NULL OR ServiceTypeName LIKE '%' + @ServiceTypeName + '%')
        AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY ServiceTypeName;
END;
GO