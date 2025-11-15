using System.Security.Cryptography;
using System.Text;
using NuGet.Protocol;
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
    }
}
