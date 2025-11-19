GO
CREATE OR ALTER PROCEDURE SpOpInsertExpensesTypes
(
    @ExpensesTypeId        VARCHAR(30) OUTPUT,
    @ExpensesTypeName      VARCHAR(100),
    @ExpensesTypeCode      VARCHAR(20),
    @IsActive              NUMERIC(1),

    @UserId                VARCHAR(60),
    @UserName              VARCHAR(100),
    @WorkstationId         VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- UPDATE CASE
    IF EXISTS (SELECT 1 FROM ExpensesTypes WHERE ExpensesTypeId = @ExpensesTypeId)
    BEGIN
        UPDATE ExpensesTypes
        SET
            ExpensesTypeName = @ExpensesTypeName,
            ExpensesTypeCode = @ExpensesTypeCode,
            IsActive         = @IsActive,

            UpdatedAt        = GETDATE(),
            UpdatedById      = @UserId,
            UpdatedByName    = @UserName,
            WorkstationId    = @WorkstationId
        WHERE ExpensesTypeId = @ExpensesTypeId;

        SELECT @ExpensesTypeId AS ExpensesTypeId;
        RETURN;
    END

    -- INSERT CASE
    SET @ExpensesTypeId = CONCAT('ETP', NEXT VALUE FOR ExpensesTypeId);

    INSERT INTO ExpensesTypes
    (
        ExpensesTypeId,
        ExpensesTypeName,
        ExpensesTypeCode,
        IsActive,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @ExpensesTypeId,
        @ExpensesTypeName,
        @ExpensesTypeCode,
        @IsActive,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @ExpensesTypeId AS ExpensesTypeId;
END;
GO
