namespace WebGrilla.Utils
{
    public class Result<T>
    {
        public T value { get; }
        public string message { get; }
        public bool isSuccess { get; }

        private Result(T item)
        {
            value = item;
            message = "";
            isSuccess = true;
        }

        private Result(string _message) 
        { 
            message = _message;
            isSuccess = false;
        }

        public static Result<T> Success(T item) => new Result<T>(item);
        public static Result<T> Failure(string message) => new Result<T>(message);
    }
}
