using Microsoft.AspNetCore.Mvc;

namespace SmartTranslator.Utils
{
    public static class ResponseResults<T>
    {
        public static ActionResult<ResponseResult<T>> OkRes(T data)
        {
            return new OkObjectResult(ResponseResult<T>.Ok(data));
        }
        public static ActionResult<ResponseResult<string>> OkRes()
        {
            return new OkObjectResult(ResponseResult<T>.Ok());
        }
        public static ActionResult<ResponseResult<string>> ErrorRes(string msg)
        {
            return new BadRequestObjectResult(ResponseResult<string>.Error(msg));
        }
        public static ActionResult<ResponseResult<string>> ErrorRes()
        {
            return new BadRequestObjectResult(ResponseResult<string>.Error("服务器错误"));
        }
    }
}
