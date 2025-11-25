GO
CREATE OR ALTER PROCEDURE Sp_GetNavigationTree
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ModuleId, ModuleName, DisplayOrder
    FROM Modules
    WHERE IsActive = 1
    ORDER BY DisplayOrder;

    SELECT MenuId, MenuName, ModuleId, ParentMenuId, DisplayOrder, PageUrl
    FROM Menus
    WHERE IsActive = 1
    ORDER BY DisplayOrder;

    SELECT FormId, FormName, MenuId, PageUrl, DisplayOrder
    FROM Forms
    WHERE IsActive = 1
    ORDER BY DisplayOrder;
END
GO