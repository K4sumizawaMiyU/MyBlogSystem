using System.Text;
using MyBlog.IRepository;
using MyBlog.Repository;
using MyBlog.Service;
using SqlSugar.IOC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyBlog.WebApi.Utilities.AutoMapper;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "ֱ输入Bearer {token}",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("em2TPkYYc7FNbAS7mmjUAm4oQ7dczVPgrhoWHjryrUf5aUVI3zZnnZdrfNlBcMXykL1hacUXcczeTzP7jx23iw==")),
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:6060;https://localhost:6066",
            ValidateAudience = true,
            ValidAudience = "http://localhost:5000;https://localhost:5100",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromHours(1)
        };
    });   
builder.Services.AddSqlSugar(new IocConfig()
{
    ConnectionString = "server=localhost;Database=TestDb;Uid=root;Pwd=Miyusuki",
    DbType = IocDbType.MySql,
    IsAutoCloseConnection = true
});
builder.Services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
builder.Services.AddScoped<IBlogNewsService, BlogNewsService>();
builder.Services.AddScoped<IAuthorInfoRepository, AuthorInfoRepository>();
builder.Services.AddScoped<IAuthorInfoService, AuthorInfoService>();
builder.Services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
builder.Services.AddScoped<ITypeInfoService, TypeInfoService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
