-- Cấp quyền Categories (Danh mục bài viết) cho Role 4 (Người bán)
-- Script này sẽ thêm quyền quản lý danh mục bài viết cho role "Người bán"
-- Chạy script này để cấp quyền danh mục bài viết cho người bán nếu chưa có

-- Bước 1: Thêm các permission cho Categories nếu chưa có
INSERT INTO `tb_permission` (`PermissionName`, `Description`, `ControllerName`, `ActionName`, `IsActive`) VALUES
('Quản lý danh mục bài viết', 'Quyền quản lý danh mục bài viết', 'Categories', 'Index', 1),
('Thêm danh mục bài viết', 'Quyền thêm danh mục bài viết', 'Categories', 'Create', 1),
('Sửa danh mục bài viết', 'Quyền sửa danh mục bài viết', 'Categories', 'Edit', 1)
ON DUPLICATE KEY UPDATE `IsActive` = 1;

-- Bước 2: Cấp tất cả quyền Categories cho Role 4 (Người bán)
INSERT INTO `tb_rolepermission` (`RoleId`, `PermissionId`)
SELECT 4, `PermissionId` FROM `tb_permission` 
WHERE `ControllerName` = 'Categories' AND `IsActive` = 1
ON DUPLICATE KEY UPDATE `RoleId` = `RoleId`;

-- Kiểm tra kết quả (có thể comment lại sau khi chạy thành công)
-- SELECT rp.RolePermissionId, r.RoleName, p.PermissionName, p.ControllerName, p.ActionName
-- FROM tb_rolepermission rp
-- INNER JOIN tb_role r ON rp.RoleId = r.RoleId
-- INNER JOIN tb_permission p ON rp.PermissionId = p.PermissionId
-- WHERE r.RoleId = 4 AND p.ControllerName = 'Categories';

