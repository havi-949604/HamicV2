-- Tạo bảng quyền (Permissions)
CREATE TABLE IF NOT EXISTS `tb_permission` (
  `PermissionId` int(11) NOT NULL AUTO_INCREMENT,
  `PermissionName` varchar(250) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `ControllerName` varchar(100) DEFAULT NULL,
  `ActionName` varchar(100) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`PermissionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Tạo bảng phân quyền cho role (RolePermissions)
CREATE TABLE IF NOT EXISTS `tb_rolepermission` (
  `RolePermissionId` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` int(11) DEFAULT NULL,
  `PermissionId` int(11) DEFAULT NULL,
  PRIMARY KEY (`RolePermissionId`),
  KEY `FK_RolePermission_Role` (`RoleId`),
  KEY `FK_RolePermission_Permission` (`PermissionId`),
  CONSTRAINT `FK_RolePermission_Role` FOREIGN KEY (`RoleId`) REFERENCES `tb_role` (`RoleId`) ON DELETE CASCADE,
  CONSTRAINT `FK_RolePermission_Permission` FOREIGN KEY (`PermissionId`) REFERENCES `tb_permission` (`PermissionId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Thêm role "Người bán" nếu chưa có
INSERT INTO `tb_role` (`RoleId`, `RoleName`, `Description`) 
VALUES (4, 'Người bán', 'Quyền quản lý sản phẩm')
ON DUPLICATE KEY UPDATE `RoleName` = 'Người bán', `Description` = 'Quyền quản lý sản phẩm';

-- Thêm các quyền mặc định
INSERT INTO `tb_permission` (`PermissionName`, `Description`, `ControllerName`, `ActionName`, `IsActive`) VALUES
('Quản lý sản phẩm', 'Quyền quản lý sản phẩm', 'Products', 'Index', 1),
('Thêm sản phẩm', 'Quyền thêm sản phẩm', 'Products', 'Create', 1),
('Sửa sản phẩm', 'Quyền sửa sản phẩm', 'Products', 'Edit', 1),
('Xóa sản phẩm', 'Quyền xóa sản phẩm', 'Products', 'Delete', 1),
('Quản lý danh mục sản phẩm', 'Quyền quản lý danh mục sản phẩm', 'Productcategories', 'Index', 1),
('Quản lý danh mục bài viết', 'Quyền quản lý danh mục bài viết', 'Categories', 'Index', 1),
('Thêm danh mục bài viết', 'Quyền thêm danh mục bài viết', 'Categories', 'Create', 1),
('Sửa danh mục bài viết', 'Quyền sửa danh mục bài viết', 'Categories', 'Edit', 1),
('Quản lý bài viết', 'Quyền quản lý bài viết', 'Blogs', 'Index', 1),
('Thêm bài viết', 'Quyền thêm bài viết', 'Blogs', 'Create', 1),
('Sửa bài viết', 'Quyền sửa bài viết', 'Blogs', 'Edit', 1), 
('Xóa bài viết', 'Quyền xóa bài viết', 'Blogs', 'Delete', 1),
('Quản lý banner', 'Quyền quản lý banner', 'HomeSlider', 'Index', 1),
('Quản lý menu', 'Quyền quản lý menu', 'Menus', 'Index', 1),
('Quản lý quyền', 'Quyền quản lý phân quyền', 'Permissions', 'Index', 1),
('Phân quyền role', 'Quyền phân quyền cho role', 'RolePermissions', 'Manage', 1),
('Quản lý người dùng', 'Quyền quản lý người dùng', 'Users', 'Index', 1),
('Truy cập trang chủ', 'Quyền truy cập trang chủ', 'Home', 'Index', 1);

-- Phân quyền cho Role 2 (Quản trị viên - Toàn quyền)
INSERT INTO `tb_rolepermission` (`RoleId`, `PermissionId`)
SELECT 2, `PermissionId` FROM `tb_permission` WHERE `IsActive` = 1
ON DUPLICATE KEY UPDATE `RoleId` = `RoleId`;

-- Phân quyền cho Role 4 (Người bán - Quản lý sản phẩm và bài viết)
INSERT INTO `tb_rolepermission` (`RoleId`, `PermissionId`)
SELECT 4, `PermissionId` FROM `tb_permission` 
WHERE `ControllerName` IN ('Products', 'Productcategories', 'Blogs', 'Categories') AND `IsActive` = 1
ON DUPLICATE KEY UPDATE `RoleId` = `RoleId`;

-- Phân quyền cho Role 1 (Khách hàng - Chỉ truy cập trang chủ)
INSERT INTO `tb_rolepermission` (`RoleId`, `PermissionId`)
SELECT 1, `PermissionId` FROM `tb_permission` 
WHERE `ControllerName` = 'Home' AND `ActionName` = 'Index' AND `IsActive` = 1
ON DUPLICATE KEY UPDATE `RoleId` = `RoleId`;
