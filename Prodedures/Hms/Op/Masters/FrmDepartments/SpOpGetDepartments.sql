-- Get Procedure for Departments
GO
CREATE OR ALTER PROCEDURE SpOpGetDepartments
(
    @DepartmentId     VARCHAR(30) = NULL,
    @DepartmentName   VARCHAR(100) = NULL,
    @IsActive         BIT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        DepartmentId,
        DepartmentName,
        DepartmentCode,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM Departments
    WHERE
        (@DepartmentId IS NULL OR @DepartmentId = '' OR DepartmentId = @DepartmentId)
        AND (@DepartmentName IS NULL OR DepartmentName LIKE '%' + @DepartmentName + '%')
        AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY DepartmentName;
END;
GO