using Microsoft.AspNetCore.Mvc;
using MyBlog.Model;
using MyBlog.Service;
using MyBlog.WebApi.Utilities.ApiResults;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _blogNewsService;
        public BlogNewsController(IBlogNewsService blogNewsService)
        {
            _blogNewsService = blogNewsService;
        }
        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            var data = await _blogNewsService.QueryAsync();
            return data == null ? ApiResultHelper.Error("没有更多了！") : ApiResultHelper.Success(data);
        }
        [HttpGet("GetBlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews(int id)
        {
            var data = await _blogNewsService.QueryAsync();
            return data == null ? ApiResultHelper.Error("文章不存在呦！") : ApiResultHelper.Success(data);
        }
        [HttpPost("CreateBlogNews")]
        public async Task<ActionResult<ApiResult>> CreateBlogNews(string? title,string? content, int typeId, int authorId)
        {
            if (title == null || content == null) return ApiResultHelper.Error("标题或内容不能为空！");
            var blogNews = new BlogNews
            {
                Title = title,
                Content = content,
                Time = DateTime.Now,
                TypeId = typeId,
                AuthorId = 1,
                Click = 0,
                Stars = 0
            };
            if (!await _blogNewsService.CreateAsync(blogNews)) return ApiResultHelper.Error("创建失败！服务器姐姐睡着了！");
            return ApiResultHelper.Success(await _blogNewsService.CreateAsync(blogNews)); 
        }

        [HttpDelete("DeleteBlogNews")]
        public async Task<ActionResult<ApiResult>> DeleteBlogNews(int id)
        {
            if(!await _blogNewsService.DeleteAsync(id)) return ApiResultHelper.Success("删除失败！服务器姐姐不让你删！");
            return ApiResultHelper.Success(await _blogNewsService.DeleteAsync(id));
        }

        [HttpPut("EditBlogNews")]
        public async Task<ActionResult<ApiResult>> EditBlogNews(int id,string title,string content,int typeid)
        {
            var blogNews = await _blogNewsService.SelectAsync(id);
            if (blogNews == null) return ApiResultHelper.Error("文章不存在呦");
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeid;
            return !await _blogNewsService.EditAsync(blogNews)
                ? ApiResultHelper.Error("修改失败，你应该问问可爱的管理员姐姐")
                : ApiResultHelper.Success(blogNews);
        }
    }
}
 