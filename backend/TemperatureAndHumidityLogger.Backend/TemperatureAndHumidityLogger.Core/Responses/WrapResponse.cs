namespace TemperatureAndHumidityLogger.Core.Responses
{
    public class WrapResponse<T>
    {
        public bool Status { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }

        public WrapResponse()
        {
        }

        public WrapResponse(bool status, T result, string message = "")
        {
            Status = status;
            Result = result;
            Message = message;
        }

        public static WrapResponse<T> Success(T result, string message = "")
        {
            return new WrapResponse<T>(true, result, message);
        }

        public static WrapResponse<T> Failure(string message)
        {
            return new WrapResponse<T>(false, default, message);
        }
    }

}
