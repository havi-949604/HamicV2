-- Script xóa các quyền trùng lặp trong database
-- Giữ lại permission có PermissionId nhỏ nhất, xóa các permission trùng lặp

-- Bước 1: Xóa các RolePermission liên quan đến permission trùng lặp (trừ permission đầu tiên)
DELETE rp1 FROM tb_rolepermission rp1
INNER JOIN tb_permission p1 ON rp1.PermissionId = p1.PermissionId
INNER JOIN tb_permission p2 ON p1.ControllerName = p2.ControllerName 
    AND p1.ActionName = p2.ActionName 
    AND p1.PermissionId > p2.PermissionId
WHERE p1.IsActive = 1 AND p2.IsActive = 1;

-- Bước 2: Xóa các permission trùng lặp (giữ lại permission có ID nhỏ nhất)
DELETE p1 FROM tb_permission p1
INNER JOIN tb_permission p2 ON p1.ControllerName = p2.ControllerName 
    AND p1.ActionName = p2.ActionName 
    AND p1.PermissionId > p2.PermissionId
WHERE p1.IsActive = 1 AND p2.IsActive = 1;

