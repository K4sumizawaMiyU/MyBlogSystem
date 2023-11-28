using SqlSugar;

namespace MyBlog.JWT.Utilities.ApiResults;

public static class ApiResultHelper
{
    public static ApiResult Success(object data, string msg = "success")
    {
        return new ApiResult
        {
            Code = 200,
            Msg = msg,
            Data = data
        };
    }
    public static ApiResult Success(object data,RefAsync<int> total, string msg = "success")
    {
        return new ApiResult
        {
            Code = 200,
            Msg = msg,
            Data = data,
            Total = total
        };
    }

    public static ApiResult Error(string msg = "error")
    {
        return new ApiResult
        {
            Code = 500,
            Msg = msg,
            Data = null,
            Total = 0
        };
    }
    public static ApiResult Error(object data,RefAsync<int> total, string msg = "error")
    {
        return new ApiResult
        {
            Code = 500,
            Msg = msg,
            Data = null,
            Total = 0
        };
    }
}