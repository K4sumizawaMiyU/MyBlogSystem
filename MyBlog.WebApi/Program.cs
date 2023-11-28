using MyBlog.IRepository;
using MyBlog.Repository;
using MyBlog.Service;
using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSqlSugar(new IocConfig()
{
    ConnectionString = "server=localhost;Database=TestDb;Uid=root;Pwd=Miyusuki",
    DbType = IocDbType.MySql,
    IsAutoCloseConnection = true
});

builder.Services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
builder.Services.AddScoped<IBlogNewsService, BlogNewsService>();
builder.Services.AddScoped<IAuthorInfoRepository, AuthorInfoRepository>();
builder.Services.AddScoped<IAuthorService, AuthorInfoService>();
builder.Services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
builder.Services.AddScoped<ITypeInfoService, TypeInfoService>();

//Create Database

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
