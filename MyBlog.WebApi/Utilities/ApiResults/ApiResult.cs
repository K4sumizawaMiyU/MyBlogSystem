namespace MyBlog.WebApi.Utilities.ApiResults;

public class ApiResult
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public int Total { get; set; }
    public object Data { get; set; }
}