using System.Security.Cryptography;
using System.Text;

namespace MyBlog.JWT.Utilities;

public class Md5Helper
{
    public static string Md5Encrypt32(string password)
    {
        var pwd = "";
        var s = MD5.HashData(Encoding.UTF8.GetBytes(password));
        foreach (var b in s)
        {
            pwd += b.ToString("X");
        }
        return pwd;
    }
}