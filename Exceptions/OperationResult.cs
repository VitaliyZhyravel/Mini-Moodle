namespace Mini_Moodle.Exceptions
{
    public class OperationResult<TResultData>
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public TResultData? Data { get; set; }

        public static OperationResult<TResultData> Success(TResultData data) => new OperationResult<TResultData> { IsSuccess = true, Data = data };
        public static OperationResult<TResultData> Failure(string errorMessage) => new OperationResult<TResultData> { IsSuccess = false, ErrorMessage = errorMessage };
    }
}
