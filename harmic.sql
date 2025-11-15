-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th9 06, 2025 lúc 05:41 PM
-- Phiên bản máy phục vụ: 10.4.32-MariaDB
-- Phiên bản PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `harmic`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_account`
--

CREATE TABLE `tb_account` (
  `AccountId` int(11) NOT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `FullName` varchar(50) DEFAULT NULL,
  `Phone` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `RoleId` int(11) DEFAULT NULL,
  `LastLogin` char(10) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_adminmenu`
--

CREATE TABLE `tb_adminmenu` (
  `MenuId` int(11) NOT NULL,
  `Title` varchar(150) DEFAULT NULL,
  `Alias` varchar(150) DEFAULT NULL,
  `Icon` varchar(100) DEFAULT NULL,
  `ParentId` int(11) DEFAULT NULL,
  `Positon` int(11) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_adminmenu`
--

INSERT INTO `tb_adminmenu` (`MenuId`, `Title`, `Alias`, `Icon`, `ParentId`, `Positon`, `Description`, `IsActive`) VALUES
(1, 'Trang chủ', 'Home', 'fas fa-tachometer-alt', 0, 1, 'Trang chủ thôi', 1),
(2, 'Quản lý sản phẩm', '#', 'fas fa-carrot', 0, 2, 'Các cài đặt liên quan đến sản phẩm', 1),
(3, 'Danh mục sản phẩm', 'ProductCategories', 'fas fa-table', 2, 1, 'Danh mục', 1),
(4, 'Sản phẩm', 'Products', 'fas fa-apple-alt ', 2, 2, 'Sản phẩm', 1),
(5, 'Đánh giá', 'ProductReviews', 'fas fa-star', 2, 3, 'Quản lý đánh giá sản phẩm từ khách hàng', 1),
(6, 'Quản lý bài viết', '#', 'fas fa-newspaper', 0, 3, 'Quản lý bài viết', 1),
(7, 'Danh mục bài viết', 'Categories', 'fas fa-table', 6, 1, 'Danh mục bài viết', 1),
(8, 'Bài viết', 'Blogs', 'fas fa-newspaper', 6, 2, 'Bài viết', 1),
(9, 'Bình luận', 'BlogComments', 'fas fa-comment', 6, 3, 'Bình luận', 1),
(10, 'Đơn đặt hàng', 'Orders', 'fas fa-money-check-alt', 0, 4, 'Đơn hàng', 0),
(11, 'Menu', 'menus', 'fas fa-bars', 0, 5, 'menu cho giao diện người dùng', 1),
(12, 'Quản lý tệp', 'FileManager', 'fas fa-folder', 0, 6, 'Quản lý tệp', 1),
(13, 'Quản lý banner', 'HomeSlider', 'fas fa-ad', 0, 7, 'Banner ở trang chủ', 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_blog`
--

CREATE TABLE `tb_blog` (
  `BlogId` int(11) NOT NULL,
  `Title` varchar(250) DEFAULT NULL,
  `Alias` varchar(250) DEFAULT NULL,
  `CategoryId` int(11) DEFAULT NULL,
  `Description` varchar(4000) DEFAULT NULL,
  `Detail` longtext DEFAULT NULL,
  `Image` varchar(500) DEFAULT NULL,
  `SeoTitle` varchar(250) DEFAULT NULL,
  `SeoDescription` varchar(500) DEFAULT NULL,
  `SeoKeywords` varchar(250) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(150) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(150) DEFAULT NULL,
  `AccountId` int(11) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_blog`
--

INSERT INTO `tb_blog` (`BlogId`, `Title`, `Alias`, `CategoryId`, `Description`, `Detail`, `Image`, `SeoTitle`, `SeoDescription`, `SeoKeywords`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy`, `AccountId`, `IsActive`) VALUES
(1, 'Khuyến mãi SỐC! Khai trương Harmic, khuyến mãi cực sốc', 'khai-truong-harrmic', 2, 'Đợt khuyến mãi đầu tiên khi Harmic được khai trương, các mặt hàng đều được giảm giá sâu', '<p>Khuyến mãi SỐC! Khai trương Harmic, khuyến mãi cực sốc!</p><p>Tất cả các mặt hàng đều được giảm giá, ít nhất 15%</p><p>Cơ hội không đến nhiều lần,&nbsp;<a href=\"http://localhost:5062/Product\">mua ngay</a>!</p><p><img src=\"data:image/jpeg;base64,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\" data-filename=\"images.jpg\" style=\"width: 300px;\"></p>', '/files/Blogs/images.jpg', NULL, NULL, NULL, '2025-07-06 02:59:25', 'Quang Lộc', NULL, NULL, NULL, 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_blogcomment`
--

CREATE TABLE `tb_blogcomment` (
  `CommentId` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `AccountId` int(11) NOT NULL,
  `Phone` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Detail` varchar(200) DEFAULT NULL,
  `BlogId` int(11) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_cart`
--

CREATE TABLE `tb_cart` (
  `IdCart` int(11) NOT NULL,
  `IdProduct` int(11) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `IdCustomer` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_category`
--

CREATE TABLE `tb_category` (
  `CategoryId` int(11) NOT NULL,
  `Title` varchar(150) DEFAULT NULL,
  `Alias` varchar(150) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `Position` int(11) DEFAULT NULL,
  `SeoTitle` varchar(250) DEFAULT NULL,
  `SeoDescription` varchar(500) DEFAULT NULL,
  `SeoKeywords` varchar(250) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(150) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(150) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_category`
--

INSERT INTO `tb_category` (`CategoryId`, `Title`, `Alias`, `Description`, `Position`, `SeoTitle`, `SeoDescription`, `SeoKeywords`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy`) VALUES
(2, 'Khuyến mãi', 'khuyen-mai', '<p>Các bài viết về khuyến mãi</p>', 1, NULL, NULL, NULL, '2025-07-05 15:31:00', 'Quang Lộc', NULL, NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_checkout`
--

CREATE TABLE `tb_checkout` (
  `ID` int(11) NOT NULL,
  `OrderID` longtext DEFAULT NULL,
  `OrderInfo` longtext DEFAULT NULL,
  `FullName` longtext DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `DatePaid` datetime DEFAULT NULL,
  `Method` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_checkout`
--

INSERT INTO `tb_checkout` (`ID`, `OrderID`, `OrderInfo`, `FullName`, `Amount`, `DatePaid`, `Method`) VALUES
(1, '638866877284140837', 'Khách hàng: . Nội dung: Thanh toán đặt hàng qua Momo tại Harmic', 'Quang Lộc', 13500, '2025-06-28 13:08:59', 'Momo'),
(2, '638866921700838006', 'Khách hàng: Quang Lộc. Nội dung: Thanh toán đặt hàng qua Momo tại Harmic', 'Quang Lộc', 13500, '2025-06-28 14:23:23', 'Momo'),
(3, '638867693617337104', 'Khách hàng: Quang Lộc. Nội dung: Thanh toán đặt hàng qua Momo tại Harmic', 'Quang Lộc', 27000, '2025-06-29 11:50:01', 'Momo'),
(4, '638917067564916760', 'Khách hàng: Quang Lộc. Nội dung: Thanh toán đặt hàng qua Momo tại Harmic', 'Quang Lộc', 22000, '2025-08-25 15:20:58', 'Momo');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_contact`
--

CREATE TABLE `tb_contact` (
  `ContactId` int(11) NOT NULL,
  `Name` varchar(150) DEFAULT NULL,
  `Phone` varchar(50) DEFAULT NULL,
  `Email` varchar(150) DEFAULT NULL,
  `Message` longtext DEFAULT NULL,
  `IsRead` int(11) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(150) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(150) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_contact`
--

INSERT INTO `tb_contact` (`ContactId`, `Name`, `Phone`, `Email`, `Message`, `IsRead`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy`) VALUES
(1, '123', '123', '123', '123', NULL, '2025-08-16 11:21:57', NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_customer`
--

CREATE TABLE `tb_customer` (
  `CustomerId` int(11) NOT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Birthday` datetime DEFAULT NULL,
  `Avatar` varchar(50) DEFAULT NULL,
  `Phone` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `LocationId` int(11) DEFAULT NULL,
  `LastLogin` datetime DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `RoleID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_customer`
--

INSERT INTO `tb_customer` (`CustomerId`, `Username`, `Password`, `Birthday`, `Avatar`, `Phone`, `Email`, `LocationId`, `LastLogin`, `IsActive`, `RoleID`) VALUES
(1, 'Quang Lộc', '6b73c20e4a962d41cbc5cf2720230988', NULL, '/files/Avatars/1.jpg', NULL, 'QuangLoc@admin.com', NULL, '2025-08-25 17:10:25', 1, 2);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_homeslider`
--

CREATE TABLE `tb_homeslider` (
  `HomeSliderId` int(11) NOT NULL,
  `Title` varchar(50) DEFAULT NULL,
  `AboveImg` varchar(500) DEFAULT NULL,
  `BelowImg` varchar(500) DEFAULT NULL,
  `MainImg` varchar(500) DEFAULT NULL,
  `Position` int(11) DEFAULT NULL,
  `SmallTxt` varchar(50) DEFAULT NULL,
  `BigTxt` varchar(200) DEFAULT NULL,
  `TextBtn` varchar(10) DEFAULT NULL,
  `UrlBtn` varchar(500) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_homeslider`
--

INSERT INTO `tb_homeslider` (`HomeSliderId`, `Title`, `AboveImg`, `BelowImg`, `MainImg`, `Position`, `SmallTxt`, `BigTxt`, `TextBtn`, `UrlBtn`, `IsActive`) VALUES
(1, 'Khai trương', '/files/HomeSlider/PNG00151-rau-cu-thap-cam-tai-png-dep.png', '/files/HomeSlider/image-removebg-preview.png', '/files/HomeSlider/250706-074531.png', 2, 'Khai trương & giảm giá', '<p>Giảm giá tất cả</p><p> sản phẩm.</p>', 'Mua ngay', '/Product', 1),
(2, 'Chính', '/files/HomeSlider/image-removebg-preview.png', '/files/HomeSlider/PNG00151-rau-cu-thap-cam-tai-png-dep.png', '/files/HomeSlider/250706-074531.png', 1, 'Harmic', '<p>Luôn cung cấp sản&nbsp; phẩm chất lượng nhất</p>', 'Mua ngay', '/product', 0);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_menu`
--

CREATE TABLE `tb_menu` (
  `MenuId` int(11) NOT NULL,
  `Title` varchar(150) DEFAULT NULL,
  `Alias` varchar(150) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `Levels` int(11) DEFAULT NULL,
  `ParentId` int(11) DEFAULT NULL,
  `Position` int(11) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(150) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(150) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_menu`
--

INSERT INTO `tb_menu` (`MenuId`, `Title`, `Alias`, `Description`, `Levels`, `ParentId`, `Position`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy`, `IsActive`) VALUES
(1, 'Trang chủ', 'Home', 'Trang chủ', 1, 0, 1, '2025-06-29 07:26:09', 'Quang Lộc', NULL, NULL, 1),
(2, 'Sản phẩm', 'Product', 'Các sản phẩm mới nhất', 1, 0, 2, '2025-06-29 08:35:29', 'Quang Lộc', NULL, NULL, 1),
(3, 'Bài viết', 'blog', 'bài viết', 1, 0, 3, '2025-08-12 22:35:14', 'Quang Lộc', NULL, NULL, 1),
(4, 'Liên lạc', 'Contact', NULL, 1, 0, 4, '2025-08-15 13:50:00', 'Quang Lộc', NULL, NULL, 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_news`
--

CREATE TABLE `tb_news` (
  `NewsId` int(11) NOT NULL,
  `Title` varchar(250) DEFAULT NULL,
  `Alias` varchar(250) DEFAULT NULL,
  `CategoryId` int(11) DEFAULT NULL,
  `Description` varchar(4000) DEFAULT NULL,
  `Detail` longtext DEFAULT NULL,
  `Image` varchar(500) DEFAULT NULL,
  `SeoTitle` varchar(250) DEFAULT NULL,
  `SeoDescription` varchar(500) DEFAULT NULL,
  `SeoKeywords` varchar(250) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(150) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(150) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_order`
--

CREATE TABLE `tb_order` (
  `OrderId` int(11) NOT NULL,
  `Code` char(10) DEFAULT NULL,
  `CustomerName` varchar(150) DEFAULT NULL,
  `Phone` varchar(15) DEFAULT NULL,
  `Address` varchar(250) DEFAULT NULL,
  `TotalAmount` int(11) DEFAULT NULL,
  `Quanlity` int(11) DEFAULT NULL,
  `OrderStatusId` int(11) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CustomerId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_orderdetail`
--

CREATE TABLE `tb_orderdetail` (
  `OrderDetailId` int(11) NOT NULL,
  `OrderId` int(11) DEFAULT NULL,
  `ProductId` int(11) DEFAULT NULL,
  `Price` decimal(18,0) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_orderstatus`
--

CREATE TABLE `tb_orderstatus` (
  `OrderStatusId` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Description` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_product`
--

CREATE TABLE `tb_product` (
  `ProductId` int(11) NOT NULL,
  `Title` varchar(250) DEFAULT NULL,
  `Alias` varchar(250) DEFAULT NULL,
  `CategoryProductId` int(11) DEFAULT NULL,
  `Description` varchar(4000) DEFAULT NULL,
  `Detail` longtext DEFAULT NULL,
  `Image` varchar(500) DEFAULT NULL,
  `Price` int(11) DEFAULT NULL,
  `PriceSale` int(11) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(150) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(150) DEFAULT NULL,
  `IsNew` tinyint(1) NOT NULL,
  `IsBestSeller` tinyint(1) NOT NULL,
  `UnitInStock` int(11) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `Star` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_product`
--

INSERT INTO `tb_product` (`ProductId`, `Title`, `Alias`, `CategoryProductId`, `Description`, `Detail`, `Image`, `Price`, `PriceSale`, `Quantity`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy`, `IsNew`, `IsBestSeller`, `UnitInStock`, `IsActive`, `Star`) VALUES
(1, 'Một túi lạc (Đậu phộng)', 'mot-tui-lac-dau-phong', 4, 'Lạc', '<p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">Đậu phộng hay còn gọi là lạc được xếp vào loại đậu cùng với các loại thực phẩm như đậu xanh, đậu nành và đậu lăng. Cây đậu phộng có nguồn gốc từ Nam Mỹ ở Brazil hoặc Peru. Đậu phộng được biết đến như nguồn cung cấp protein, chất béo, nhiều chất dinh dưỡng khác cho cơ thể đồng thời giúp giảm nguy cơ các bệnh liên quan đến tim mạch.</span></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><img src=\"/files/Products/1-1-112x124.jpg\" style=\"width: 50%;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\"><br></span></p><h2 id=\"heading-w6msqoquf\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 26px; vertical-align: baseline; font-weight: 600; position: relative; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">1. Lợi ích sức khỏe của đậu phộng</h2><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Nhiều người cho rằng đậu phộng không có giá trị dinh dưỡng cao như các loại hạt như:&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/gia-tri-dinh-duong-cua-hat-hanh-nhan-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(0, 118, 192); display: inline-block; font-weight: 600;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">Hạnh nhân</span></a>, óc chó hoặc hạt điều. Tuy nhiên, đậu phộng có nhiều lợi ích sức khỏe tương tự như các loại hạt đắt tiền khác và sử dụng đậu phộng như một loại thực phẩm bổ dưỡng.</p><h3 id=\"heading-z5pjhqswr\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 18px; vertical-align: baseline; font-weight: 600; position: relative; text-indent: 30px; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">1. Sức khỏe tim mạch</span></h3><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Người ta đã chú ý nhiều đến quả óc chó và hạnh nhân như những thực phẩm tốt cho tim mạch, do hàm lượng&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/nao-la-chat-beo-khong-bao-hoa-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(0, 118, 192); display: inline-block; font-weight: 600;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">chất béo không bão hòa</span></a>&nbsp;cao của chúng. Nhưng các nghiên cứu cho thấy rằng đậu phộng tốt cho sức khỏe tim mạch ngang với các loại hạt đắt tiền.</p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Đậu phộng giúp ngăn ngừa bệnh tim bằng cách giảm mức cholesterol. Chúng cũng có thể ngăn chặn sự hình thành các cục máu đông nhỏ và giảm nguy cơ bị đau tim hoặc&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/dot-quy-vi-sao-nguy-hiem-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(0, 118, 192); display: inline-block; font-weight: 600;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">đột quỵ</span></a>.</p><h3 id=\"heading-1kznx6lwy\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 18px; vertical-align: baseline; font-weight: 600; position: relative; text-indent: 30px; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">1.2. Giảm nguy cơ tiểu đường</span></h3><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">Ăn lạc nhiều</span>&nbsp;có tác dụng gì? Lạc thuộc nhóm thực phẩm có chỉ số đường huyết thấp, có nghĩa khi ăn lạc sẽ không làm tăng đột biến&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/carbohydrate-va-luong-duong-trong-mau-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(0, 118, 192); display: inline-block; font-weight: 600;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">lượng đường trong máu</span></a>&nbsp;của bạn. Các nghiên cứu đã chỉ ra rằng ăn đậu phộng có thể làm giảm nguy cơ mắc&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/meo-de-giam-nguy-co-mac-benh-tieu-duong-loai-2-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(0, 118, 192); display: inline-block; font-weight: 600;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">bệnh tiểu đường loại 2</span></a>&nbsp;ở phụ nữ.</p><h3 id=\"heading-rnvpjs20d\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 18px; vertical-align: baseline; font-weight: 600; position: relative; text-indent: 30px; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">1.3. Đậu phộng giúp giảm viêm</span></h3><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Lạc, nguồn cung cấp chất xơ dồi dào, giúp giảm viêm khắp cơ thể cũng như hỗ trợ hệ tiêu hóa của bạn.</p><h3 id=\"heading-l24tuu9wn\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 18px; vertical-align: baseline; font-weight: 600; position: relative; text-indent: 30px; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">1.4. Ngăn ngừa ung thư</span></h3><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Nghiên cứu đã chứng minh rằng đối với những người lớn tuổi, ăn bơ đậu phộng có thể giúp giảm nguy cơ phát triển một loại&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/ung-thu-da-day-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(0, 118, 192); display: inline-block; font-weight: 600;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">ung thư dạ dày</span></a>&nbsp;nhất định - ung thư biểu mô tuyến không tim.</p><h3 id=\"heading-hn66vsr1o\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 18px; vertical-align: baseline; font-weight: 600; position: relative; text-indent: 30px; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">1.5. Ngăn ngừa sỏi mật</span></h3><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Sử dụng đậu phộng với hàm lượng 28,35 gam mỗi tuần sẽ giúp giảm 25% nguy cơ tiến triển&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/video-dung-chu-quan-voi-benh-ly-soi-mat-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(0, 118, 192); display: inline-block; font-weight: 600;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">bệnh sỏi mật</span></a>.</p><h3 id=\"heading-61gaep22g\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 18px; vertical-align: baseline; font-weight: 600; position: relative; text-indent: 30px; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">1.6. Ngăn ngừa và phòng chống trầm cảm</span></h3><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Thành phần dinh dưỡng của đậu phộng bao gồm acid amin tryptophan có vai trò quan trọng trong quá trình sản xuất serotonin, hợp chất có lợi cho não bộ đồng thời giúp cải thiện tâm trạng cũng như giảm&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/tim-hieu-ve-benh-tram-cam-va-nhung-trieu-chung-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(0, 118, 192); display: inline-block; font-weight: 600;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; font-weight: bolder; border: 0px; vertical-align: baseline;\">chứng trầm cảm</span></a>.</p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><br></p>', '/files/Products/1-1-112x124.jpg', 15000, 13500, 1, '2025-06-19 10:47:42', 'Quang Lộc', NULL, NULL, 1, 1, 40, 1, 5),
(2, 'Đu đủ', 'du-du', 1, 'Đu đủ là một loại trái cây nhiệt đới cực kỳ tốt cho sức khỏe do trong đu đủ chứa nhiều chất chống oxy hóa có thể làm giảm viêm, chống lại bệnh tật, sáng mắt và đẹp da.', '<h2 id=\"heading-pz5pi42me\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 26px; vertical-align: baseline; font-weight: 600; position: relative; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Những lợi ích sức khỏe khi ăn đu đủ mang lại</h2><h2 id=\"heading-pz5pi42me\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 26px; vertical-align: baseline; font-weight: 600; position: relative; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"font-weight: bolder; font-size: 18px; text-indent: 30px;\">Có tác dụng chống oxy hóa mạnh</span></h2><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; display: inline-block;\">Đu đủ</span><font color=\"#333333\" face=\"Inter, roboto, Arial, Helvetica, sans-serif\"><span style=\"background-color: rgb(247, 247, 247);\">&nbsp;chứa chất chống oxy hóa lành mạnh được gọi là carotenoids. Hơn thế nữa, cơ thể bạn hấp thụ các chất chống oxy hóa có lợi này từ đu đủ tốt hơn so với các loại trái cây và rau củ khác.</span></font></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\"><span style=\"color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Các nghiên cứu cho thấy rằng đu đủ lên men có thể làm giảm căng thẳng oxy hóa ở người lớn tuổi, những</span> người bị&nbsp;<a href=\"https://www.vinmec.com/vie/bai-viet/nao-la-tien-dai-thao-duong-vi\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; display: inline-block;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\">tiền tiểu đường</span></a>,&nbsp;<a href=\"https://www.vinmec.com/vie/benh/suy-giap-3245\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; display: inline-block;\"><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\">suy giáp</span></a>&nbsp;v<font color=\"#333333\" face=\"Inter, roboto, Arial, Helvetica, sans-serif\"><span style=\"background-color: rgb(247, 247, 247);\">à bệnh gan.</span></font></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\"><span style=\"font-weight: bolder; background-color: rgb(247, 247, 247); color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; font-size: 18px; text-indent: 30px;\">Có đặc tính chống ung thư</span></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Nghiên cứu cho thấy rằng lycopene trong đu đủ có thể làm giảm nguy cơ ung thư và đu đủ cũng có thể có lợi cho những người đang điều trị ung thư.</p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"font-weight: bolder; font-size: 18px; text-indent: 30px;\">Có thể cải thiện sức khỏe tim mạch</span></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Bổ sung đu đủ vào chế độ ăn uống của bạn có thể tăng cường sức khỏe tim mạch. Các nghiên cứu cho thấy trái cây chứa nhiều lycopene và vitamin C có thể giúp ngăn ngừa bệnh tim.</p><div class=\"banner-block\" style=\"margin: 20px 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247); opacity: 1; transition: opacity 0.5s;\"><div id=\"zone_18\" class=\"banner inpage-banner\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; height: 0px; min-height: 0px; max-width: 0px;\"><iframe src=\"https://www.vinmec.com/banner/null\" title=\"Internal Banner\" loading=\"lazy\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border-width: initial; border-style: none; vertical-align: baseline; max-width: 100%; width: 0px; overflow: hidden;\"></iframe></div></div><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\"><font color=\"#333333\" style=\"font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Các chất chống oxy hóa trong đu đủ có thể bảo vệ trái tim của bạn và tăng cường tác dụng bảo vệ của&nbsp;</font><span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; display: inline-block;\">cholesterol HDL tốt</span>.</p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"font-weight: bolder; font-size: 18px; text-indent: 30px;\">Có thể chống viêm</span></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Các nghiên cứu cho thấy, các loại trái cây và rau quả giàu chất chống oxy hóa như đu đủ giúp giảm các dấu hiệu viêm.</p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><span style=\"font-weight: bolder; font-size: 18px; text-indent: 30px;\">Có thể cải thiện tiêu hóa</span></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\"><span style=\"color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Enzyme papain trong đu đủ có thể làm cho protein dễ tiêu hóa hơn. Đây là một phương thuốc chữa</span>&nbsp;<span style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; display: inline-block;\">táo bón</span>&nbsp;và các triệu chứ<font color=\"#333333\" face=\"Inter, roboto, Arial, Helvetica, sans-serif\"><span style=\"background-color: rgb(247, 247, 247);\">ng khác của hội chứng ruột kích thích (IBS).</span></font></p><figure class=\"post-image full has-zoomable\" style=\"margin-right: auto; margin-bottom: 15px; margin-left: auto; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><picture style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision;\"><source media=\"(max-width: 576px)\" srcset=\"\r\n                  /static/uploads/small_20200619_151731_778916_la_du_du_ok_yofu_max_1800x1800_jpg_047331b4eb.jpg\r\n                \" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision;\"><source media=\"(max-width: 768px)\" srcset=\"\r\n                  /static/uploads/small_20200619_151731_778916_la_du_du_ok_yofu_max_1800x1800_jpg_047331b4eb.jpg\r\n                \" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision;\"><source media=\"(min-width: 769px)\" srcset=\"\r\n                  /static/uploads/large_20200619_151731_778916_la_du_du_ok_yofu_max_1800x1800_jpg_047331b4eb.jpg\r\n                \" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision;\"><img loading=\"lazy\" alt=\"\r\nĐủ đủ chứa Enzyme papain rất tốt cho hệ tiêu hóa của người dùng\r\n\" class=\"full uploaded img-in-body\" data-cfsrc=\"/static/uploads/large_20200619_151731_778916_la_du_du_ok_yofu_max_1800x1800_jpg_047331b4eb.jpg\" data-cfstyle=\"aspect-ratio:1024/768\" src=\"https://www.vinmec.com/static/uploads/large_20200619_151731_778916_la_du_du_ok_yofu_max_1800x1800_jpg_047331b4eb.jpg\" style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; max-width: 100%; border-radius: 5px; width: 740px; aspect-ratio: 1024 / 768;\"><span style=\"font-size: 18px; font-weight: bolder; text-indent: 30px;\"><br></span></picture><picture style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision;\"><span style=\"font-size: 18px; font-weight: bolder; text-indent: 30px;\">Bảo vệ chống tổn thương da</span></picture></figure><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Ngoài việc giữ cho cơ thể khỏe mạnh, đu đủ còn có thể giúp làn da của bạn trông săn chắc và trẻ trung hơn.Vitamin C và lycopene trong đu đủ bảo vệ làn da của bạn và có thể giúp giảm các dấu hiệu lão hóa.</p><h2 id=\"heading-cxmxqorld\" style=\"margin-right: 0px; margin-bottom: 16px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; font-size: 26px; vertical-align: baseline; font-weight: 600; position: relative; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Ăn đu đủ như thế nào mới tốt</h2><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"></p><p style=\"margin-right: 0px; margin-left: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\">Dưới đây là một vài công thức mà bạn có thể thực hiện dễ dàng bằng cách sử dụng một quả đu đủ nhỏ để làm:</p><ul style=\"margin-right: 0px; margin-bottom: 15px; margin-left: 0px; padding: 15px 15px 15px 30px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline; list-style-position: initial; list-style-image: initial; color: rgb(51, 51, 51); font-family: Inter, roboto, Arial, Helvetica, sans-serif; background-color: rgb(247, 247, 247);\"><li style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\">Bữa sáng: Cắt làm đôi và đổ đầy một nửa bằng sữa chua Hy Lạp, sau đó phủ lên trên một vài quả việt quất và các loại hạt xắt nhỏ.</li><li style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\">Salad: Cắt nhỏ đu đủ, cà chua, hành tây và ngò, sau đó thêm nước cốt chanh và trộn đều.</li><li style=\"margin: 0px; padding: 0px; text-rendering: geometricprecision; border: 0px; vertical-align: baseline;\">Tráng miệng: Kết hợp trái cây xắt nhỏ với 2 muỗng canh (28 gram) hạt chia, 1 cốc (240 ml) sữa hạnh nhân và 1/4 muỗng cà phê vani. Trộn đều và để tủ lạnh trước khi ăn.</li></ul>', '/files/Products/1-1-270x300.jpg', 30000, 22000, 1, '2025-07-06 03:15:52', 'Quang Lộc', '2025-07-06 03:17:26', 'Quang Lộc', 1, 0, 100, 1, 4);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_productcategory`
--

CREATE TABLE `tb_productcategory` (
  `CategoryProductId` int(11) NOT NULL,
  `Title` varchar(150) DEFAULT NULL,
  `Alias` varchar(150) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `Icon` varchar(500) DEFAULT NULL,
  `Position` int(11) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(150) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(150) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_productcategory`
--

INSERT INTO `tb_productcategory` (`CategoryProductId`, `Title`, `Alias`, `Description`, `Icon`, `Position`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy`, `IsActive`) VALUES
(1, 'Trái cây', 'TraiCay', 'Các loại hoa quả', '/files/CategoryProduct/2.png', 1, '2025-06-17 06:08:27', 'Quang Lộc', '2025-06-29 11:44:02', 'Quang Lộc', 1),
(2, 'Rau', 'Rau', 'Cấc loại rau củ', '/files/CategoryProduct/3.png', 4, '2025-06-17 06:08:27', 'Quang Lộc', NULL, NULL, 1),
(3, 'Thịt', 'thit', 'Các loại thịt từ đóng hộp cho đến thịt tươi, hay các thực phẩm từ thịt', '/files/CategoryProduct/4.png', 6, '2025-06-19 10:12:10', 'Quang Lộc', NULL, NULL, 1),
(4, 'Lương thực', 'luongthuc', 'Các loại lương thực như lúa, gạo, khoai, sắn, ....', '/files/CategoryProduct/5.png', 2, '2025-06-19 10:12:54', 'Quang Lộc', '2025-06-28 16:10:46', 'Quang Lộc', 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_productreview`
--

CREATE TABLE `tb_productreview` (
  `ProductReviewId` int(11) NOT NULL,
  `CustomerId` int(11) NOT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Detail` varchar(200) DEFAULT NULL,
  `Star` int(11) DEFAULT NULL,
  `ProductId` int(11) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_productreview`
--

INSERT INTO `tb_productreview` (`ProductReviewId`, `CustomerId`, `CreatedDate`, `Detail`, `Star`, `ProductId`, `IsActive`) VALUES
(3, 1, '2025-08-18 21:23:30', 'Được đó', 4, 1, 1),
(4, 1, '2025-08-19 06:28:47', '123', 4, 2, 0);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_role`
--

CREATE TABLE `tb_role` (
  `RoleId` int(11) NOT NULL,
  `RoleName` varchar(50) DEFAULT NULL,
  `Description` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_role`
--

INSERT INTO `tb_role` (`RoleId`, `RoleName`, `Description`) VALUES
(1, 'Người dùng', 'Người dùng bình thường'),
(2, 'Quản trị viên', 'Toàn quyền'),
(3, 'Quản trị bài viết', 'Chỉnh sửa, thêm sửa xóa bài viết');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_wishlish`
--

CREATE TABLE `tb_wishlish` (
  `WishlishID` int(11) NOT NULL,
  `AccountID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_wishlish`
--

INSERT INTO `tb_wishlish` (`WishlishID`, `AccountID`, `ProductID`) VALUES
(13, 1, 2),
(14, 1, 1);

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `tb_account`
--
ALTER TABLE `tb_account`
  ADD PRIMARY KEY (`AccountId`),
  ADD KEY `FK_tb_Account_tb_Role` (`RoleId`);

--
-- Chỉ mục cho bảng `tb_adminmenu`
--
ALTER TABLE `tb_adminmenu`
  ADD PRIMARY KEY (`MenuId`);

--
-- Chỉ mục cho bảng `tb_blog`
--
ALTER TABLE `tb_blog`
  ADD PRIMARY KEY (`BlogId`),
  ADD KEY `FK_tb_Blog_tb_Account` (`AccountId`),
  ADD KEY `FK_tb_Blog_tb_Category` (`CategoryId`);

--
-- Chỉ mục cho bảng `tb_blogcomment`
--
ALTER TABLE `tb_blogcomment`
  ADD PRIMARY KEY (`CommentId`),
  ADD KEY `FK_tb_BlogComment_tb_Blog` (`BlogId`),
  ADD KEY `UserId` (`AccountId`);

--
-- Chỉ mục cho bảng `tb_cart`
--
ALTER TABLE `tb_cart`
  ADD PRIMARY KEY (`IdCart`),
  ADD KEY `FK_tb_Cart_tb_Customer` (`IdCustomer`),
  ADD KEY `FK_tb_Cart_tb_Product` (`IdProduct`);

--
-- Chỉ mục cho bảng `tb_category`
--
ALTER TABLE `tb_category`
  ADD PRIMARY KEY (`CategoryId`);

--
-- Chỉ mục cho bảng `tb_checkout`
--
ALTER TABLE `tb_checkout`
  ADD PRIMARY KEY (`ID`);

--
-- Chỉ mục cho bảng `tb_contact`
--
ALTER TABLE `tb_contact`
  ADD PRIMARY KEY (`ContactId`);

--
-- Chỉ mục cho bảng `tb_customer`
--
ALTER TABLE `tb_customer`
  ADD PRIMARY KEY (`CustomerId`),
  ADD KEY `RoleID` (`RoleID`);

--
-- Chỉ mục cho bảng `tb_homeslider`
--
ALTER TABLE `tb_homeslider`
  ADD PRIMARY KEY (`HomeSliderId`);

--
-- Chỉ mục cho bảng `tb_menu`
--
ALTER TABLE `tb_menu`
  ADD PRIMARY KEY (`MenuId`);

--
-- Chỉ mục cho bảng `tb_news`
--
ALTER TABLE `tb_news`
  ADD PRIMARY KEY (`NewsId`),
  ADD KEY `FK_tb_News_tb_Category` (`CategoryId`);

--
-- Chỉ mục cho bảng `tb_order`
--
ALTER TABLE `tb_order`
  ADD PRIMARY KEY (`OrderId`),
  ADD KEY `FK_tb_Order_tb_Customer` (`CustomerId`),
  ADD KEY `FK_tb_Order_tb_OrderStatus` (`OrderStatusId`);

--
-- Chỉ mục cho bảng `tb_orderdetail`
--
ALTER TABLE `tb_orderdetail`
  ADD PRIMARY KEY (`OrderDetailId`),
  ADD KEY `FK_tb_OrderDetail_tb_Order` (`OrderId`);

--
-- Chỉ mục cho bảng `tb_orderstatus`
--
ALTER TABLE `tb_orderstatus`
  ADD PRIMARY KEY (`OrderStatusId`);

--
-- Chỉ mục cho bảng `tb_product`
--
ALTER TABLE `tb_product`
  ADD PRIMARY KEY (`ProductId`),
  ADD KEY `FK_tb_Product_tb_ProductCategory` (`CategoryProductId`);

--
-- Chỉ mục cho bảng `tb_productcategory`
--
ALTER TABLE `tb_productcategory`
  ADD PRIMARY KEY (`CategoryProductId`);

--
-- Chỉ mục cho bảng `tb_productreview`
--
ALTER TABLE `tb_productreview`
  ADD PRIMARY KEY (`ProductReviewId`),
  ADD KEY `FK_tb_ProductReview_tb_Product` (`ProductId`),
  ADD KEY `CustomerId` (`CustomerId`);

--
-- Chỉ mục cho bảng `tb_role`
--
ALTER TABLE `tb_role`
  ADD PRIMARY KEY (`RoleId`);

--
-- Chỉ mục cho bảng `tb_wishlish`
--
ALTER TABLE `tb_wishlish`
  ADD PRIMARY KEY (`WishlishID`),
  ADD KEY `AccountID` (`AccountID`),
  ADD KEY `ProductID` (`ProductID`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `tb_account`
--
ALTER TABLE `tb_account`
  MODIFY `AccountId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `tb_adminmenu`
--
ALTER TABLE `tb_adminmenu`
  MODIFY `MenuId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT cho bảng `tb_blog`
--
ALTER TABLE `tb_blog`
  MODIFY `BlogId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT cho bảng `tb_blogcomment`
--
ALTER TABLE `tb_blogcomment`
  MODIFY `CommentId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `tb_cart`
--
ALTER TABLE `tb_cart`
  MODIFY `IdCart` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT cho bảng `tb_category`
--
ALTER TABLE `tb_category`
  MODIFY `CategoryId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT cho bảng `tb_checkout`
--
ALTER TABLE `tb_checkout`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `tb_contact`
--
ALTER TABLE `tb_contact`
  MODIFY `ContactId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT cho bảng `tb_customer`
--
ALTER TABLE `tb_customer`
  MODIFY `CustomerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT cho bảng `tb_homeslider`
--
ALTER TABLE `tb_homeslider`
  MODIFY `HomeSliderId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT cho bảng `tb_menu`
--
ALTER TABLE `tb_menu`
  MODIFY `MenuId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `tb_news`
--
ALTER TABLE `tb_news`
  MODIFY `NewsId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `tb_order`
--
ALTER TABLE `tb_order`
  MODIFY `OrderId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `tb_orderdetail`
--
ALTER TABLE `tb_orderdetail`
  MODIFY `OrderDetailId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `tb_orderstatus`
--
ALTER TABLE `tb_orderstatus`
  MODIFY `OrderStatusId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `tb_product`
--
ALTER TABLE `tb_product`
  MODIFY `ProductId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT cho bảng `tb_productcategory`
--
ALTER TABLE `tb_productcategory`
  MODIFY `CategoryProductId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `tb_productreview`
--
ALTER TABLE `tb_productreview`
  MODIFY `ProductReviewId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `tb_role`
--
ALTER TABLE `tb_role`
  MODIFY `RoleId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `tb_wishlish`
--
ALTER TABLE `tb_wishlish`
  MODIFY `WishlishID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `tb_account`
--
ALTER TABLE `tb_account`
  ADD CONSTRAINT `FK_tb_Account_tb_Role` FOREIGN KEY (`RoleId`) REFERENCES `tb_role` (`RoleId`);

--
-- Các ràng buộc cho bảng `tb_blog`
--
ALTER TABLE `tb_blog`
  ADD CONSTRAINT `FK_tb_Blog_tb_Account` FOREIGN KEY (`AccountId`) REFERENCES `tb_account` (`AccountId`),
  ADD CONSTRAINT `FK_tb_Blog_tb_Category` FOREIGN KEY (`CategoryId`) REFERENCES `tb_category` (`CategoryId`);

--
-- Các ràng buộc cho bảng `tb_blogcomment`
--
ALTER TABLE `tb_blogcomment`
  ADD CONSTRAINT `FK_tb_BlogComment_tb_Blog` FOREIGN KEY (`BlogId`) REFERENCES `tb_blog` (`BlogId`),
  ADD CONSTRAINT `tb_blogcomment_ibfk_1` FOREIGN KEY (`AccountId`) REFERENCES `tb_account` (`AccountId`);

--
-- Các ràng buộc cho bảng `tb_cart`
--
ALTER TABLE `tb_cart`
  ADD CONSTRAINT `FK_tb_Cart_tb_Customer` FOREIGN KEY (`IdCustomer`) REFERENCES `tb_customer` (`CustomerId`),
  ADD CONSTRAINT `FK_tb_Cart_tb_Product` FOREIGN KEY (`IdProduct`) REFERENCES `tb_product` (`ProductId`);

--
-- Các ràng buộc cho bảng `tb_customer`
--
ALTER TABLE `tb_customer`
  ADD CONSTRAINT `tb_customer_ibfk_1` FOREIGN KEY (`RoleID`) REFERENCES `tb_role` (`RoleId`);

--
-- Các ràng buộc cho bảng `tb_news`
--
ALTER TABLE `tb_news`
  ADD CONSTRAINT `FK_tb_News_tb_Category` FOREIGN KEY (`CategoryId`) REFERENCES `tb_category` (`CategoryId`);

--
-- Các ràng buộc cho bảng `tb_order`
--
ALTER TABLE `tb_order`
  ADD CONSTRAINT `FK_tb_Order_tb_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `tb_customer` (`CustomerId`),
  ADD CONSTRAINT `FK_tb_Order_tb_OrderStatus` FOREIGN KEY (`OrderStatusId`) REFERENCES `tb_orderstatus` (`OrderStatusId`);

--
-- Các ràng buộc cho bảng `tb_orderdetail`
--
ALTER TABLE `tb_orderdetail`
  ADD CONSTRAINT `FK_tb_OrderDetail_tb_Order` FOREIGN KEY (`OrderId`) REFERENCES `tb_order` (`OrderId`);

--
-- Các ràng buộc cho bảng `tb_product`
--
ALTER TABLE `tb_product`
  ADD CONSTRAINT `FK_tb_Product_tb_ProductCategory` FOREIGN KEY (`CategoryProductId`) REFERENCES `tb_productcategory` (`CategoryProductId`);

--
-- Các ràng buộc cho bảng `tb_productreview`
--
ALTER TABLE `tb_productreview`
  ADD CONSTRAINT `FK_tb_ProductReview_tb_Product` FOREIGN KEY (`ProductId`) REFERENCES `tb_product` (`ProductId`),
  ADD CONSTRAINT `tb_productreview_ibfk_1` FOREIGN KEY (`CustomerId`) REFERENCES `tb_customer` (`CustomerId`);

--
-- Các ràng buộc cho bảng `tb_wishlish`
--
ALTER TABLE `tb_wishlish`
  ADD CONSTRAINT `tb_wishlish_ibfk_1` FOREIGN KEY (`AccountID`) REFERENCES `tb_customer` (`CustomerId`),
  ADD CONSTRAINT `tb_wishlish_ibfk_2` FOREIGN KEY (`ProductID`) REFERENCES `tb_product` (`ProductId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
