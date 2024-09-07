namespace BlazeMyFinance.Shared.Common
{
    public class ApiResult<T>
    {
        public ApiResult()
        {
            Messages = new List<string>();
        }
        public ApiResult(bool success)
        {
            Success = success;
        }

        public ApiResult(bool success, T value)
        {
            Success = success;
            Value = value;
        }

        public ApiResult(bool success, T value, List<string> messsages)
        {
            Success = success;
            Value = value;
            Messages = messsages;
        }

        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public T Value { get; set; }
    }
}
