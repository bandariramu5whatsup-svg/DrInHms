GO
CREATE OR ALTER PROCEDURE SpOpInsertPayCategories
(
    @PayCategoryId         VARCHAR(30) OUTPUT,
    @PayCategoryName       VARCHAR(100),
    @PayCategoryCode       VARCHAR(20),
    @PayCategoryOption     VARCHAR(20),
    @PayCategoryOptionText VARCHAR(20),
    @IsActive              NUMERIC(1),

    @UserId                VARCHAR(60),
    @UserName              VARCHAR(100),
    @WorkstationId         VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- UPDATE CASE
    IF EXISTS (SELECT 1 FROM PayCategories WHERE PayCategoryId = @PayCategoryId)
    BEGIN
        UPDATE PayCategories
        SET 
            PayCategoryName       = @PayCategoryName,
            PayCategoryCode       = @PayCategoryCode,
            PayCategoryOption     = @PayCategoryOption,
            PayCategoryOptionText = @PayCategoryOptionText,
            IsActive              = @IsActive,

            UpdatedAt             = GETDATE(),
            UpdatedById           = @UserId,
            UpdatedByName         = @UserName,
            WorkstationId         = @WorkstationId
        WHERE PayCategoryId = @PayCategoryId;

        SELECT @PayCategoryId AS PayCategoryId;
        RETURN;
    END

    -- INSERT CASE
    SET @PayCategoryId = CONCAT('PC', NEXT VALUE FOR PayCategoryId);

    INSERT INTO PayCategories
    (
        PayCategoryId,
        PayCategoryName,
        PayCategoryCode,
        PayCategoryOption,
        PayCategoryOptionText,
        IsActive,

        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @PayCategoryId,
        @PayCategoryName,
        @PayCategoryCode,
        @PayCategoryOption,
        @PayCategoryOptionText,
        @IsActive,

        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @PayCategoryId AS PayCategoryId;
END;
GO
