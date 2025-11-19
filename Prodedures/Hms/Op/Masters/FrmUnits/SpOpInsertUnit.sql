-- Insert + Update Procedure for Units
GO
CREATE OR ALTER PROCEDURE SpOpInsertUnit
(
    @UnitId         VARCHAR(50) OUTPUT,
    @UnitName       VARCHAR(200),
    @Description    VARCHAR(MAX),
    @IsActive       BIT,

    @UserId         VARCHAR(60),
    @UserName       VARCHAR(100),
    @WorkstationId  VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Units WHERE UnitId = @UnitId)
    BEGIN
        UPDATE Units
        SET 
            UnitName      = @UnitName,
            Description   = @Description,
            IsActive      = @IsActive,

            UpdatedAt     = GETDATE(),
            UpdatedById   = @UserId,
            UpdatedByName = @UserName,
            WorkstationId = @WorkstationId
        WHERE UnitId      = @UnitId;

        SELECT @UnitId AS UnitId;
        RETURN;
    END

    SET @UnitId = CONCAT('UN', NEXT VALUE FOR UnitId);

    INSERT INTO Units
    (
        UnitId,
        UnitName,
        Description,
        IsActive,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @UnitId,
        @UnitName,
        @Description,
        @IsActive,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @UnitId AS UnitId;
END;
GO