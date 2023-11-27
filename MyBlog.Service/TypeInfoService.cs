using MyBlog.IRepository;
using MyBlog.Model;

namespace MyBlog.Service;

public class TypeInfoService:BaseService<TypeInfo>,ITypeInfoService
{
    private readonly ITypeInfoRepository _iTypeInfoRep;
    public TypeInfoService(ITypeInfoRepository typeInfoRepository)
    {
        baseRepository = typeInfoRepository;
        _iTypeInfoRep = typeInfoRepository;
    }
}