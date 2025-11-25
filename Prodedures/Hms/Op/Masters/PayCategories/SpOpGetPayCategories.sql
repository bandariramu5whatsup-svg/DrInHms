GO
CREATE OR ALTER PROCEDURE SpOpGetPayCategories
(
    @PayCategoryId        VARCHAR(30) = NULL,
    @PayCategoryName      VARCHAR(100) = NULL,
    @IsActive             NUMERIC(1) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        PayCategoryId,
        PayCategoryName,
        PayCategoryCode,
        PayCategoryOption,
        PayCategoryOptionText,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM PayCategories
    WHERE
        (@PayCategoryId IS NULL OR @PayCategoryId = '' OR PayCategoryId = @PayCategoryId)
        AND (@PayCategoryName IS NULL OR PayCategoryName LIKE '%' + @PayCategoryName + '%')
        --AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY PayCategoryName;
END;
GO
