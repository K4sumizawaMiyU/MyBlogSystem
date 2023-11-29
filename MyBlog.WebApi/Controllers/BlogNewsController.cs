using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Service;
using MyBlog.WebApi.Utilities.ApiResults;
using SqlSugar;

namespace MyBlog.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("any")]
public class BlogNewsController : ControllerBase
{
    private readonly IBlogNewsService _blogNewsService;

    public BlogNewsController(IBlogNewsService blogNewsService)
    {
        _blogNewsService = blogNewsService;
    }
    [HttpGet("BlogNewsPage")]
    public async Task<ActionResult<ApiResult>> GetBlogNewsPage([FromServices]IMapper mapper,int page,int size)
    {
        RefAsync<int> total = 10;
        var blogNews = await _blogNewsService.QueryAsync(page, size, total);
        try
        {
            var blogNewsDtoList = mapper.Map<List<BlogNewsDTO>>(blogNews);
            return ApiResultHelper.Success(blogNewsDtoList,total);
        }
        catch
        {
            return ApiResultHelper.Error("Mapper映射错误");
        }
    }
    [HttpGet("GetBlogNews")]
    public async Task<ActionResult<ApiResult>> GetBlogNews(int typeId)
    {
        var data = await _blogNewsService.QueryAsync(c => c.TypeId == typeId);
        return data == null
            ? ApiResultHelper.Error("文章不存在呦！")
            : ApiResultHelper.Success(data,data.Count);
    }
    [Authorize]
    [HttpGet("BlogNews")]
    public async Task<ActionResult<ApiResult>> GetBlogNews()
    {
        var id = Convert.ToInt32(this.User.FindFirst("Id")!.Value);
        var data = await _blogNewsService.QueryAsync(c => c.AuthorId == id);
        return data.Count == 0
            ? ApiResultHelper.Error("没有更多了！")
            : ApiResultHelper.Success(data, data.Count, "查询成功");
    }
    [Authorize]
    [HttpPost("CreateBlogNews")]
    public async Task<ActionResult<ApiResult>> CreateBlogNews(string? title, 
        string? content, int typeId)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            return ApiResultHelper.Error("标题或内容不能为空！");
        var blogNews = new BlogNews
        {
            Title = title,
            Content = content,
            Time = DateTime.Now,
            TypeId = typeId,
            AuthorId = Convert.ToInt32(this.User.FindFirst("Id")!.Value),
            Click = 0,
            Stars = 0
        };
        var data = await _blogNewsService.CreateAsync(blogNews);
        return !data
            ? ApiResultHelper.Error("创建失败！服务器姐姐睡着了！")
            : ApiResultHelper.Success(await _blogNewsService.CreateAsync(blogNews));
    }
    [Authorize]
    [HttpDelete("DeleteBlogNews")]
    public async Task<ActionResult<ApiResult>> DeleteBlogNews(int id)
    {
        var data = await _blogNewsService.DeleteAsync(id);
        return !data
            ? ApiResultHelper.Error("删除失败！服务器姐姐不让你删！")
            : ApiResultHelper.Success(data);
    }
    [Authorize]
    [HttpPut("EditBlogNews")]
    public async Task<ActionResult<ApiResult>> EditBlogNews(int id, string title, string content, int typeid)
    {
        var blogNews = await _blogNewsService.SelectAsync(id);
        if (blogNews == null) return ApiResultHelper.Error("文章不存在呦");
        blogNews.Title = title;
        blogNews.Content = content;
        blogNews.TypeId = typeid;
        var dataBool = await _blogNewsService.EditAsync(blogNews);
        return !dataBool
            ? ApiResultHelper.Error("修改失败，你应该问问可爱的管理员姐姐")
            : ApiResultHelper.Success(blogNews);
    }
}