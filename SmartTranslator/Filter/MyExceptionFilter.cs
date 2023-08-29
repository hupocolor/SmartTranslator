using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SmartTranslator.Utils;

namespace SmartTranslator.Filter
{
    public class MyExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //请你在这个位置添加代码使得所有MyException类型的异常都在此处处理
            if (context.Exception is not MyException) return;
            var resResult = ResponseResult<string>.Error(((MyException)context.Exception).msg);
            var result = new ContentResult();
            ////内容为自定义返回类型的json格式
            result.Content = JsonConvert.SerializeObject(resResult);
            result.StatusCode = 400;
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
