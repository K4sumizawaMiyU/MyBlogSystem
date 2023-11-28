using System.Linq.Expressions;
using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;

namespace MyBlog.Repository;

public class BlogNewsRepository:BaseRepository<BlogNews>,IBlogNewsRepository
{
    public async override Task<List<BlogNews>> QueryAsync()
    {
        return await Context.Queryable<BlogNews>()
            .Mapper(c => c.TypeId, c=> c.TypeInfo.Id)
            .Mapper(c => c.AuthorInfo, c=> c.AuthorId,c =>c.AuthorInfo.Id )
            .ToListAsync();
    }

    public async Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func)
    {
        return await Context.Queryable<BlogNews>()
            .Where(func)
            .Mapper(c => c.TypeInfo,c => c.TypeId, c => c.TypeInfo.Id)
            .Mapper(c => c.AuthorInfo, c => c.AuthorId, c => c.AuthorInfo.Id)
            .ToListAsync();
    }

    public override async Task<List<BlogNews>> QueryAsync(int page, int size, RefAsync<int> total)
    {
        return await Context.Queryable<BlogNews>()
            .Mapper(c => c.AuthorInfo, c => c.AuthorId, c => c.AuthorInfo.Id)
            .Mapper(c => c.TypeInfo,c => c.TypeId, c => c.TypeInfo.Id)
            .ToPageListAsync(page, size, total);
    }

    public async override Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func,int page, int size, RefAsync<int> total)
    {
        return await Context.Queryable<BlogNews>()
            .Where(func)
            .Mapper(c => c.AuthorInfo, c => c.AuthorId, c => c.AuthorInfo.Id)
            .Mapper(c => c.TypeInfo,c => c.TypeId, c => c.TypeInfo.Id)
            .ToPageListAsync(page, size, total);
    }
}