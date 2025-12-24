-- Kích hoạt menu "Quản lý đơn hàng" đã có trong database
-- Menu này đã tồn tại với MenuId = 10 nhưng IsActive = 0

-- Cách 1: Kích hoạt menu hiện có (nếu MenuId = 10 tồn tại)
UPDATE `tb_adminmenu` 
SET `IsActive` = 1, 
    `Title` = 'Quản lý đơn hàng',
    `Icon` = 'fas fa-shopping-cart',
    `Description` = 'Quản lý đơn hàng của khách hàng'
WHERE `MenuId` = 10 AND `Alias` = 'Orders';

-- Cách 2: Nếu menu không tồn tại, tạo mới
-- Lấy MenuId lớn nhất hiện có
SET @maxMenuId = (SELECT COALESCE(MAX(MenuId), 0) FROM tb_adminmenu);

-- Lấy Position lớn nhất hiện có cho menu cha (ParentId = 0)
SET @maxPosition = (SELECT COALESCE(MAX(Positon), 0) FROM tb_adminmenu WHERE ParentId = 0);

-- Chỉ tạo mới nếu chưa có menu với Alias = 'Orders'
INSERT INTO `tb_adminmenu` (`MenuId`, `Title`, `Alias`, `Icon`, `ParentId`, `Positon`, `Description`, `IsActive`) 
SELECT @maxMenuId + 1, 'Quản lý đơn hàng', 'Orders', 'fas fa-shopping-cart', 0, @maxPosition + 1, 'Quản lý đơn hàng của khách hàng', 1
WHERE NOT EXISTS (SELECT 1 FROM tb_adminmenu WHERE Alias = 'Orders');

