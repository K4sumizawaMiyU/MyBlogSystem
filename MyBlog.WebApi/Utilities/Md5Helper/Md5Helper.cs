using System.Security.Cryptography;
using System.Text;

namespace MyBlog.WebApi.Utilities;

public class Md5Helper
{
    public static string Md5Encrypt32(string password)
    {
        var pwd = "";
        var md5 = MD5.Create();
        var s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        foreach (var b in s)
        {
            pwd += b.ToString("X");
        }
        return pwd;
    }
}