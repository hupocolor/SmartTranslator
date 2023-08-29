namespace SmartTranslator.Utils
{
    public class MyException:Exception
    {
        public ExceptionType Type { get; private set; }
        public string msg { get; private set; }

        private MyException(ExceptionType type)
        {
            Type = type;
            msg = "操作失败";
        }
        private MyException(string msg) {
            Type = ExceptionType.BAD_REQUEST;
            this.msg = msg;
        }
        public static MyException create(ExceptionType type)
        {
            return new MyException(type);
        }
        public static MyException create(string msg)
        {
            return new MyException(msg);
        }
    }
}
