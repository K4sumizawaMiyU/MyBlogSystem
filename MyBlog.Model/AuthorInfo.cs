using SqlSugar;

namespace MyBlog.Model;

public class AuthorInfo : BaseId
{
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string Name { get; set; }
    [SugarColumn(ColumnDataType = "nvarchar(16)")]
    public string UserName { get; set; }
    [SugarColumn(ColumnDataType = "nvarchar(64)")]
    public string UserPwd { get; set; }
}