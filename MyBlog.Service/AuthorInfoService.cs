using MyBlog.IRepository;
using MyBlog.Model;

namespace MyBlog.Service;

public class AuthorInfoService:BaseService<AuthorInfo>,IAuthorInfoService
{
    private readonly IAuthorInfoRepository _iAuthorInfoRep;
    public AuthorInfoService(IAuthorInfoRepository authorInfoRepository)
    {
        baseRepository = authorInfoRepository;
        _iAuthorInfoRep = authorInfoRepository;
    }
}