namespace MyBlog.Model.DTO;

public class BlogNewsDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Time { get; set; }
    public int Click { get; set; }
    public int Stars { get; set; }
    public int TypeId { get; set; }
    public int AuthorId { get; set; }
    public string TypeName { get; set; }
    public string AuthorName { get; set; }
}