-- Insert + Update Procedure for Departments
GO
CREATE OR ALTER PROCEDURE SpOpInsertDepartment
(
    @DepartmentId      VARCHAR(30) OUTPUT,
    @DepartmentName    VARCHAR(100),
    @DepartmentCode    VARCHAR(20),
    @IsActive          BIT,

    @UserId            VARCHAR(60),
    @UserName          VARCHAR(100),
    @WorkstationId     VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Departments WHERE DepartmentId = @DepartmentId)
    BEGIN
        UPDATE Departments
        SET
            DepartmentName   = @DepartmentName,
            DepartmentCode   = @DepartmentCode,
            IsActive         = @IsActive,

            UpdatedAt        = GETDATE(),
            UpdatedById      = @UserId,
            UpdatedByName    = @UserName,
            WorkstationId    = @WorkstationId
        WHERE DepartmentId   = @DepartmentId;

        SELECT @DepartmentId AS DepartmentId;
        RETURN;
    END

    SET @DepartmentId = CONCAT('DP', NEXT VALUE FOR DepartmentId);

    INSERT INTO Departments
    (
        DepartmentId,
        DepartmentName,
        DepartmentCode,
        IsActive,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @DepartmentId,
        @DepartmentName,
        @DepartmentCode,
        @IsActive,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @DepartmentId AS DepartmentId;
END;
GO