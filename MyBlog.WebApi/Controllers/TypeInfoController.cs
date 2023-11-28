using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Model;
using MyBlog.Service;
using MyBlog.WebApi.Utilities.ApiResults;

namespace MyBlog.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TypeInfoController : ControllerBase
{
    private readonly ITypeInfoService _typeInfoService;

    public TypeInfoController(ITypeInfoService typeInfoService)
    {
        _typeInfoService = typeInfoService;
    }

    [HttpGet("GetTypeNames")]
    public async Task<ActionResult<ApiResult>> GetTypeNames()
    {
        var types = await _typeInfoService.QueryAsync();
        return types.Count == 0
            ? ApiResultHelper.Error("没有更多了！")
            : ApiResultHelper.Success(types, types.Count, "查询成功");
    }

    [HttpPost("SetType")]
    public async Task<ActionResult<ApiResult>> SetType(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("参数不能为空！");
        var typeInfo = new TypeInfo
        {
            Name = name
        };
        var type = await _typeInfoService.CreateAsync(typeInfo);
        return !type
            ? ApiResultHelper.Error("修改失败！服务器姐姐睡着了！")
            : ApiResultHelper.Success(type);
    }

    [HttpDelete("DeleteType")]
    public async Task<ActionResult<ApiResult>> DeleteType(int id)
    {
        var type = await _typeInfoService.DeleteAsync(id);
        return !type
            ? ApiResultHelper.Error("删除失败！请检查类型是否存在！")
            : ApiResultHelper.Success(type);
    }

    [HttpPut("EditType")]
    public async Task<ActionResult<ApiResult>> EditType(int id, string name)
    {
        var type = await _typeInfoService.SelectAsync(id);
        type.Name = name;
        var editBool = await _typeInfoService.EditAsync(type);
        return !editBool
            ? ApiResultHelper.Error("修改失败，请联系管理员")
            : ApiResultHelper.Success("修改成功");
    }
}