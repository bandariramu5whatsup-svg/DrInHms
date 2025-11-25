GO
CREATE TABLE Menus (
    MenuId VARCHAR(50) PRIMARY KEY,
    MenuName VARCHAR(150),
    MenuIcon VARCHAR(50),
    ModuleId VARCHAR(50),         -- FK to Modules
    ParentMenuId VARCHAR(50) NULL, -- For sub-menu
    PageUrl VARCHAR(200) NULL,
    DisplayOrder INT,
    IsActive numeric(1));
GO
INSERT INTO Menus (
    MenuId, MenuName, MenuIcon, ModuleId, ParentMenuId, PageUrl, DisplayOrder, IsActive
)
VALUES
('OP_MASTERS', 'Masters', 'fa fa-folder', 'OP', NULL, NULL, 1, 1);
