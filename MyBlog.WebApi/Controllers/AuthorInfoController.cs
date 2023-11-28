using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Service;
using MyBlog.WebApi.Utilities;
using MyBlog.WebApi.Utilities.ApiResults;

namespace MyBlog.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthorInfoController : ControllerBase
{
    private readonly IAuthorInfoService _authorInfoService;

    public AuthorInfoController(IAuthorInfoService authorInfoService)
    {
        _authorInfoService = authorInfoService;
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult<ApiResult>> CreateUser(string name, string username, string pwd)
    {
        var user = new AuthorInfo
        {
            Name = name,
            UserName = username,
            UserPwd = Md5Helper.Md5Encrypt32(pwd)
        };
        var userInfo = await _authorInfoService.SelectAsync(c => c.UserName == username);
        if (userInfo != null) return ApiResultHelper.Error("已存在用户！");
        await _authorInfoService.CreateAsync(user);
        return ApiResultHelper.Success("创建成功！");
    }

    [HttpPut("ChangeName")]
    public async Task<ActionResult<ApiResult>> ChangeUserName(string name, string username)
    {
        var id = Convert.ToInt32(User.FindFirst("Id")!.Value);
        var user = await _authorInfoService.SelectAsync(id); 
        user.Name = name;
        var changeBool = await _authorInfoService.EditAsync(user);
        return !changeBool
            ? ApiResultHelper.Error("修改失败，请联系管理员获取更多信息。")
            : ApiResultHelper.Success("修改成功！");
    }

    [HttpPut("ChangeUserPwd")]
    public async Task<ActionResult<ApiResult>> ChangeUserPassword(string newPwd)
    {
        var id = Convert.ToInt32(User.FindFirst("Id")!.Value);
        var user = await _authorInfoService.SelectAsync(id);
        user.UserPwd = Md5Helper.Md5Encrypt32(newPwd);
        var changeBool = await _authorInfoService.EditAsync(user);
        return !changeBool
            ? ApiResultHelper.Error("修改失败，请联系管理员获取更多信息。")
            : ApiResultHelper.Success("修改成功！");
    }

    [HttpDelete("DeleteUserAccount")]
    public async Task<ActionResult<ApiResult>> DeleteUserAccount()
    {
        var id = Convert.ToInt32(User.FindFirst("Id")!.Value);
        var user = await _authorInfoService.SelectAsync(id);
        if (user == null) return ApiResultHelper.Error("用户不存在！");
        var deleteBool = await _authorInfoService.DeleteAsync(user.Id);
        return !deleteBool
            ? ApiResultHelper.Error("删除失败，请联系管理员！")
            : ApiResultHelper.Success("删除成功！");
    }
    [AllowAnonymous]
    [HttpGet("FindUser")]
    public async Task<ActionResult<ApiResult>> FindUser([FromServices]IMapper iMapper,string Name)
    {
        var users = await _authorInfoService.QueryAsync(c => c.Name == Name);
        var userMapperList = users.Select(user => iMapper.Map<AuthorDTO>(user)).ToList();
        return userMapperList.Count == 0 
            ? ApiResultHelper.Error("没有找到这样的用户哦")
            : ApiResultHelper.Success(userMapperList);
    }
}