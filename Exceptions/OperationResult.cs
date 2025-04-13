namespace Mini_Moodle.Exceptions
{
    public class OperationResult<TData>
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public TData? Data { get; set; }

        public static OperationResult<TData> Success(TData data) => new OperationResult<TData> { IsSuccess = true, Data = data };
        public static OperationResult<TData> Failure(string errorMessage) => new OperationResult<TData> { IsSuccess = false, ErrorMessage = errorMessage };
    }
}
