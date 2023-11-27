using SqlSugar;

namespace MyBlog.Model;

public class TypeInfo : BaseId
{
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string Name { get; set; }
}