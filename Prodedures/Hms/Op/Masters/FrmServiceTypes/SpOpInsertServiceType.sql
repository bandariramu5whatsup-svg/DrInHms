-- Insert + Update Procedure for ServiceTypes
GO
CREATE OR ALTER PROCEDURE SpOpInsertServiceType
(
    @ServiceTypeId    VARCHAR(50) OUTPUT,
    @ServiceTypeName  VARCHAR(200),
    @Description      VARCHAR(200),
    @IsActive         BIT,

    @UserId           VARCHAR(60),
    @UserName         VARCHAR(100),
    @WorkstationId    VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM ServiceTypes WHERE ServiceTypeId = @ServiceTypeId)
    BEGIN
        UPDATE ServiceTypes
        SET
            ServiceTypeName = @ServiceTypeName,
            Description     = @Description,
            IsActive        = @IsActive,

            UpdatedAt       = GETDATE(),
            UpdatedById     = @UserId,
            UpdatedByName   = @UserName,
            WorkstationId   = @WorkstationId
        WHERE ServiceTypeId = @ServiceTypeId;

        SELECT @ServiceTypeId AS ServiceTypeId;
        RETURN;
    END

    SET @ServiceTypeId = CONCAT('ST', NEXT VALUE FOR ServiceTypeId);

    INSERT INTO ServiceTypes
    (
        ServiceTypeId,
        ServiceTypeName,
        Description,
        IsActive,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @ServiceTypeId,
        @ServiceTypeName,
        @Description,
        @IsActive,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @ServiceTypeId AS ServiceTypeId;
END;
GO