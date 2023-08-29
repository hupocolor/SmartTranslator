namespace SmartTranslator.Utils
{
    public class ResponseResult<T>
    {
        public int code { get; private set; }
        public string message { get; set; }
        public T? data { get; set; }

        private ResponseResult(int code, string message, T data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
        private ResponseResult(int code, string message) {
            this.code = code;
            this.message = message;
        }

        public static ResponseResult<T> Ok()
        {
            return new ResponseResult<T>(200, "操作成功");
        }
        public static ResponseResult<T> Error(string message)
        {
            return new ResponseResult<T>(400, message);
        }
        public static ResponseResult<T> Ok(T data)
        {
            return new ResponseResult<T>(200, "操作成功", data);
        }
    }
}
