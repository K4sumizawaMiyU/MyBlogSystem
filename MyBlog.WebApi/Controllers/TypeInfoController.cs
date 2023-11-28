using Microsoft.AspNetCore.Mvc;
using MyBlog.Model;
using MyBlog.Service;
using MyBlog.WebApi.Utilities.ApiResults;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeInfoController : ControllerBase
    {
        private readonly ITypeInfoService _typeInfoService;
        public TypeInfoController(ITypeInfoService typeInfoService)
        {
            _typeInfoService = typeInfoService;
        }
        [HttpGet("GetType")]
        public async Task<ActionResult<ApiResult>> GetType()
        {
            var data = await _typeInfoService.QueryAsync();
            return data.Count == 0 ? ApiResultHelper.Error("没有更多了！") : ApiResultHelper.Success(data);
        }

        [HttpPost("SetTypeName")]
        public async Task<ActionResult<ApiResult>> SetTypeName(string name)
        {
            if (name == null) return ApiResultHelper.Error("参数不能为空！");
            var typeInfo = new TypeInfo()
            {
                Name = name
            };
            var data = await _typeInfoService.CreateAsync(typeInfo);
            return !data ? ApiResultHelper.Error("修改失败！服务器姐姐睡着了！") : ApiResultHelper.Success(data);
        }
        [HttpDelete("DeleteType")]
        public async Task<ActionResult<ApiResult>> DeleteType(int id)
        {
            var data = await _typeInfoService.DeleteAsync(id);
            return !data ? ApiResultHelper.Error("删除失败！请检查类型是否存在！") : ApiResultHelper.Success(data);
        }
    }
}
