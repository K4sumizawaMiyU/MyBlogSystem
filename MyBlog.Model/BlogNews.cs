using SqlSugar;

namespace MyBlog.Model;

public class BlogNews : BaseId
{
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string Title { get; set; }
    [SugarColumn(ColumnDataType = "text")]
    public string Content { get; set; }
    public DateTime Time { get; set; }
    
    public int Click { get; set; }
    public int Stars { get; set; }
    
    public int TypeId { get; set; }
    public int AuthorId { get; set; }
    
    [SugarColumn(IsIgnore = true)]
    public TypeInfo TypeInfo { get; set; }
    [SugarColumn(IsIgnore = true)]
    public AuthorInfo AuthorInfo { get; set; }
}