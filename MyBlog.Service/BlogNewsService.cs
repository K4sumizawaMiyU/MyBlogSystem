using MyBlog.IRepository;
using MyBlog.Model;

namespace MyBlog.Service;

public class BlogNewsService:BaseService<BlogNews>,IBlogNewsService
{
    private readonly IBlogNewsRepository _iBlogNewRep;
    public BlogNewsService(IBlogNewsRepository blogNewsRepository)
    {
        baseRepository = blogNewsRepository;
        _iBlogNewRep = blogNewsRepository;
    }
}