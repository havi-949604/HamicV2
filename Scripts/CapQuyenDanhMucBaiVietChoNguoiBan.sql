-- =====================================================
-- CẤP QUYỀN DANH MỤC BÀI VIẾT CHO NGƯỜI BÁN (ROLE 4)
-- =====================================================
-- Mục đích: Cho phép người bán có quyền quản lý danh mục bài viết
-- Ngày tạo: 2024
-- =====================================================

-- BƯỚC 1: Tạo các quyền (Permission) cho danh mục bài viết
-- =====================================================
-- Thêm quyền "Xem danh sách danh mục bài viết"
INSERT INTO `tb_permission` (`PermissionName`, `Description`, `ControllerName`, `ActionName`, `IsActive`) 
VALUES ('Quản lý danh mục bài viết', 'Quyền quản lý danh mục bài viết', 'Categories', 'Index', 1)
ON DUPLICATE KEY UPDATE `IsActive` = 1;

-- Thêm quyền "Thêm danh mục bài viết"
INSERT INTO `tb_permission` (`PermissionName`, `Description`, `ControllerName`, `ActionName`, `IsActive`) 
VALUES ('Thêm danh mục bài viết', 'Quyền thêm danh mục bài viết', 'Categories', 'Create', 1)
ON DUPLICATE KEY UPDATE `IsActive` = 1;

-- Thêm quyền "Sửa danh mục bài viết"
INSERT INTO `tb_permission` (`PermissionName`, `Description`, `ControllerName`, `ActionName`, `IsActive`) 
VALUES ('Sửa danh mục bài viết', 'Quyền sửa danh mục bài viết', 'Categories', 'Edit', 1)
ON DUPLICATE KEY UPDATE `IsActive` = 1;


-- BƯỚC 2: Cấp quyền cho người bán (Role 4)
-- =====================================================
-- Cấp quyền "Xem danh sách danh mục bài viết"
INSERT INTO `tb_rolepermission` (`RoleId`, `PermissionId`)
SELECT 4, `PermissionId` 
FROM `tb_permission` 
WHERE `ControllerName` = 'Categories' AND `ActionName` = 'Index' AND `IsActive` = 1
ON DUPLICATE KEY UPDATE `RoleId` = `RoleId`;

-- Cấp quyền "Thêm danh mục bài viết"
INSERT INTO `tb_rolepermission` (`RoleId`, `PermissionId`)
SELECT 4, `PermissionId` 
FROM `tb_permission` 
WHERE `ControllerName` = 'Categories' AND `ActionName` = 'Create' AND `IsActive` = 1
ON DUPLICATE KEY UPDATE `RoleId` = `RoleId`;

-- Cấp quyền "Sửa danh mục bài viết"
INSERT INTO `tb_rolepermission` (`RoleId`, `PermissionId`)
SELECT 4, `PermissionId` 
FROM `tb_permission` 
WHERE `ControllerName` = 'Categories' AND `ActionName` = 'Edit' AND `IsActive` = 1
ON DUPLICATE KEY UPDATE `RoleId` = `RoleId`;


-- BƯỚC 3: Kiểm tra kết quả (Tùy chọn - có thể xóa sau)
-- =====================================================
-- Chạy câu lệnh này để xem các quyền đã được cấp cho người bán
SELECT 
    r.RoleId,
    r.RoleName AS 'Tên vai trò',
    p.PermissionName AS 'Tên quyền',
    p.ControllerName AS 'Controller',
    p.ActionName AS 'Action'
FROM tb_rolepermission rp
INNER JOIN tb_role r ON rp.RoleId = r.RoleId
INNER JOIN tb_permission p ON rp.PermissionId = p.PermissionId
WHERE r.RoleId = 4 AND p.ControllerName = 'Categories'
ORDER BY p.ActionName;

