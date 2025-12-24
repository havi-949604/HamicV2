-- Thêm menu quản lý đơn hàng vào admin menu
-- Lấy MenuId lớn nhất hiện có
SET @maxMenuId = (SELECT COALESCE(MAX(MenuId), 0) FROM tb_adminmenu);

-- Lấy Position lớn nhất hiện có
SET @maxPosition = (SELECT COALESCE(MAX(Positon), 0) FROM tb_adminmenu WHERE ParentId = 0);

-- Thêm menu "Quản lý đơn hàng"
INSERT INTO `tb_adminmenu` (`MenuId`, `Title`, `Alias`, `Icon`, `ParentId`, `Positon`, `Description`, `IsActive`) 
VALUES 
(@maxMenuId + 1, 'Quản lý đơn hàng', 'Orders', 'fas fa-shopping-cart', 0, @maxPosition + 1, 'Quản lý đơn hàng của khách hàng', 1);

