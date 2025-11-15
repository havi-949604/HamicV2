using System.Security.Cryptography;
using System.Text;
using NuGet.Protocol;
using Microsoft.EntityFrameworkCore;
namespace Harmic.Utilities
{
    public class Function
    {
        public static int _AccountId = 0;
        public static string _Email = string.Empty;
        public static string _Message = string.Empty;
        public static string _FullName = string.Empty;
        public static int _RoleId = 0;

        public static string _ReturnUrl = string.Empty;

        public static string TitleSlugGenerationAlias(string title)
        {
            return SlugGenerator.SlugGenerator.GenerateSlug(title);
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public static string md5password(string text)
        {
            string str = MD5Hash(text);

            for (int i = 0; i <= 5; i++)
            {
                str = MD5Hash(str + "_" + str);
            }
            return str;
        }

        public static bool isLogin()
        {
            if (string.IsNullOrEmpty(_Email) || string.IsNullOrEmpty(_FullName) || _AccountId == 0 || _RoleId == 0) return false;
            return true;
        }

        public static bool isAdmin()
        {
            if (isLogin() && _RoleId == 2) return true;
            return false;
        }

        // Kiểm tra có phải người bán không (Role 4)
        public static bool isSeller()
        {
            if (isLogin() && _RoleId == 4) return true;
            return false;
        }

        // Kiểm tra có phải khách hàng không (Role 1)
        public static bool isCustomer()
        {
            if (isLogin() && _RoleId == 1) return true;
            return false;
        }

        // Kiểm tra có quyền truy cập admin không (Admin hoặc Người bán)
        public static bool canAccessAdmin()
        {
            if (isLogin() && (_RoleId == 2 || _RoleId == 4)) return true;
            return false;
        }

        // Kiểm tra sản phẩm có đang trong thời gian khuyến mãi không
        public static bool IsProductOnSale(Models.TbProduct product)
        {
            if (product == null) return false;
            
            // Nếu không có giá khuyến mãi thì không có khuyến mãi
            if (product.PriceSale == null || product.PriceSale == 0) return false;
            
            DateTime now = DateTime.Now;
            
            // Nếu không có ngày bắt đầu và kết thúc, coi như luôn khuyến mãi (tương thích với dữ liệu cũ)
            if (product.SaleStartDate == null && product.SaleEndDate == null)
            {
                return product.PriceSale > 0 && product.Price > product.PriceSale;
            }
            
            // Kiểm tra ngày hiện tại có trong khoảng khuyến mãi không
            bool afterStart = product.SaleStartDate == null || now >= product.SaleStartDate.Value;
            bool beforeEnd = product.SaleEndDate == null || now <= product.SaleEndDate.Value;
            
            return afterStart && beforeEnd && product.PriceSale > 0 && product.Price > product.PriceSale;
        }

        // Lấy giá hiện tại của sản phẩm (tự động chọn giá khuyến mãi nếu đang trong thời gian khuyến mãi)
        public static decimal GetCurrentPrice(Models.TbProduct product)
        {
            if (product == null) return 0;
            
            if (IsProductOnSale(product))
            {
                return (decimal)(product.PriceSale ?? 0);
            }
            
            return (decimal)(product.Price ?? 0);
        }

        // Lấy giá gốc (không khuyến mãi)
        public static decimal GetOriginalPrice(Models.TbProduct product)
        {
            if (product == null) return 0;
            return (decimal)(product.Price ?? 0);
        }

        // Kiểm tra quyền theo tên quyền
        public static bool HasPermission(Models.HarmicContext context, string permissionName)
        {
            // Chỉ Admin và Người bán mới có thể có quyền
            if (!canAccessAdmin()) return false;
            if (context == null) return false;

            var permission = context.TbPermissions
                .FirstOrDefault(p => p.PermissionName == permissionName && p.IsActive);

            if (permission == null) return false;

            var hasPermission = context.TbRolePermissions
                .Any(rp => rp.RoleId == _RoleId && rp.PermissionId == permission.PermissionId);

            return hasPermission;
        }

        // Kiểm tra quyền theo controller và action
        public static bool HasPermission(Models.HarmicContext context, string controllerName, string actionName)
        {
            // Chỉ Admin và Người bán mới có thể có quyền
            if (!canAccessAdmin()) return false;
            if (context == null) return false;

            var permission = context.TbPermissions
                .FirstOrDefault(p => 
                    p.ControllerName == controllerName && 
                    p.ActionName == actionName && 
                    p.IsActive);

            if (permission == null) return false;

            var hasPermission = context.TbRolePermissions
                .Any(rp => rp.RoleId == _RoleId && rp.PermissionId == permission.PermissionId);

            return hasPermission;
        }

        // Lấy danh sách quyền của role hiện tại
        public static List<Models.TbPermission> GetUserPermissions(Models.HarmicContext context)
        {
            // Chỉ Admin và Người bán mới có thể có quyền
            if (!canAccessAdmin() || context == null) return new List<Models.TbPermission>();

            return context.TbRolePermissions
                .Where(rp => rp.RoleId == _RoleId)
                .Include(rp => rp.Permission)
                .Select(rp => rp.Permission)
                .Where(p => p != null && p.IsActive)
                .ToList()!;
        }
    }
}
