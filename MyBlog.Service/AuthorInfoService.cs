using MyBlog.IRepository;
using MyBlog.Model;

namespace MyBlog.Service;

public class AuthorInfoService:BaseService<AuthorInfo>,IAuthorService
{
    private readonly IAuthorInfoRepository _iAuthorInfoRep;
    public AuthorInfoService(IAuthorInfoRepository authorInfoRepository)
    {
        baseRepository = authorInfoRepository;
        _iAuthorInfoRep = authorInfoRepository;
    }
}