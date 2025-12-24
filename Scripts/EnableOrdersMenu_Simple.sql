-- Kích hoạt menu "Quản lý đơn hàng"
-- Script này sẽ kích hoạt menu đã có (MenuId = 10) hoặc tạo mới nếu chưa có

-- Bước 1: Kích hoạt menu hiện có nếu đã tồn tại
UPDATE `tb_adminmenu` 
SET `IsActive` = 1, 
    `Title` = 'Quản lý đơn hàng',
    `Icon` = 'fas fa-shopping-cart',
    `Description` = 'Quản lý đơn hàng của khách hàng',
    `Positon` = 4
WHERE `MenuId` = 10;

-- Bước 2: Nếu không có menu nào được update (menu chưa tồn tại), tạo mới
INSERT INTO `tb_adminmenu` (`MenuId`, `Title`, `Alias`, `Icon`, `ParentId`, `Positon`, `Description`, `IsActive`) 
SELECT 10, 'Quản lý đơn hàng', 'Orders', 'fas fa-shopping-cart', 0, 4, 'Quản lý đơn hàng của khách hàng', 1
WHERE NOT EXISTS (SELECT 1 FROM tb_adminmenu WHERE MenuId = 10);

