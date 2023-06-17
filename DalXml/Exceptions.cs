
namespace Dal;
internal class Exceptions
{
    public class DataError : Exception
    {
        public DataError(Exception inner) : base("", inner) { }
        public override string Message => InnerException.Message;
    }
}
