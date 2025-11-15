-- Thêm menu quản lý phân quyền vào admin menu
-- Lấy MenuId lớn nhất hiện có
SET @maxMenuId = (SELECT COALESCE(MAX(MenuId), 0) FROM tb_adminmenu);

-- Thêm menu cha "Quản lý phân quyền"
INSERT INTO `tb_adminmenu` (`MenuId`, `Title`, `Alias`, `Icon`, `ParentId`, `Positon`, `Description`, `IsActive`) 
VALUES 
(@maxMenuId + 1, 'Quản lý phân quyền', '#', 'fas fa-key', 0, 8, 'Quản lý quyền và phân quyền', 1);

-- Thêm menu con "Quản lý quyền"
INSERT INTO `tb_adminmenu` (`MenuId`, `Title`, `Alias`, `Icon`, `ParentId`, `Positon`, `Description`, `IsActive`) 
VALUES 
(@maxMenuId + 2, 'Quản lý quyền', 'Permissions', 'fas fa-list', @maxMenuId + 1, 1, 'Quản lý các quyền trong hệ thống', 1);

-- Thêm menu con "Phân quyền Role"
INSERT INTO `tb_adminmenu` (`MenuId`, `Title`, `Alias`, `Icon`, `ParentId`, `Positon`, `Description`, `IsActive`) 
VALUES 
(@maxMenuId + 3, 'Phân quyền Role', 'RolePermissions/Manage', 'fas fa-user-shield', @maxMenuId + 1, 2, 'Phân quyền cho các role', 1);

-- Thêm menu con "Quản lý người dùng"
INSERT INTO `tb_adminmenu` (`MenuId`, `Title`, `Alias`, `Icon`, `ParentId`, `Positon`, `Description`, `IsActive`) 
VALUES 
(@maxMenuId + 4, 'Quản lý người dùng', 'Users', 'fas fa-users', @maxMenuId + 1, 3, 'Quản lý người dùng và role', 1);
