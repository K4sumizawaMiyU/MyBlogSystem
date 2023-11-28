using AutoMapper;
using MyBlog.Model;
using MyBlog.Model.DTO;

namespace MyBlog.WebApi.Utilities.AutoMapper;

public class CustomAutoMapperProfile : Profile
{
    public CustomAutoMapperProfile()
    {
        base.CreateMap<AuthorInfo,AuthorDTO>();
        base.CreateMap<BlogNews, BlogNewsDTO>().ForMember(dest => dest.TypeName,
            source => source
                .MapFrom(src => src.TypeInfo));
    }
}