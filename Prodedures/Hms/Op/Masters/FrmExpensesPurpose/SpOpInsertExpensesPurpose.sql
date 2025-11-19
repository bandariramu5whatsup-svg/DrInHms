GO
CREATE OR ALTER PROCEDURE SpOpInsertExpensesPurpose
(
    @ExpensesPurposeId        VARCHAR(30) OUTPUT,
    @ExpensesPurposeName      VARCHAR(100),
    @ExpensesPurposeCode      VARCHAR(20),
    @IsActive                 NUMERIC(1),

    @UserId                   VARCHAR(60),
    @UserName                 VARCHAR(100),
    @WorkstationId            VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- UPDATE CASE
    IF EXISTS (SELECT 1 FROM ExpensesPurpose WHERE ExpensesPurposeId = @ExpensesPurposeId)
    BEGIN
        UPDATE ExpensesPurpose
        SET 
            ExpensesPurposeName = @ExpensesPurposeName,
            ExpensesPurposeCode = @ExpensesPurposeCode,
            IsActive            = @IsActive,

            UpdatedAt           = GETDATE(),
            UpdatedById         = @UserId,
            UpdatedByName       = @UserName,
            WorkstationId       = @WorkstationId
        WHERE ExpensesPurposeId = @ExpensesPurposeId;

        SELECT @ExpensesPurposeId AS ExpensesPurposeId;
        RETURN;
    END

    -- INSERT CASE
    SET @ExpensesPurposeId = CONCAT('EPR', NEXT VALUE FOR ExpensesPurposeId);

    INSERT INTO ExpensesPurpose
    (
        ExpensesPurposeId,
        ExpensesPurposeName,
        ExpensesPurposeCode,
        IsActive,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @ExpensesPurposeId,
        @ExpensesPurposeName,
        @ExpensesPurposeCode,
        @IsActive,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @ExpensesPurposeId AS ExpensesPurposeId;
END;
GO
