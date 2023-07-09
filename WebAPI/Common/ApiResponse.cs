namespace WebAPI.Common
{
    public class ApiResponse<T>
    {
        private int _code;

        public string Status { get; set; }
        public int Code
        {
            get => _code;
            set
            {
                _code = value;

                SetStatusAndMessage(value);
            }
        }
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;

        private void SetStatusAndMessage(int statusCode)
        {


            if (statusCode >= 200 && statusCode < 300)
            {
                Status = ApiResponseStatus.Success.ToString();
                Message = ApiResponseMessage.RequestSuccessful.ToString();
            }
            else if (statusCode >= 400 && statusCode < 500)
            {
                Status = ApiResponseStatus.Error.ToString();
                Message = ApiResponseMessage.InvalidInputProvided.ToString();
            }
            else if (statusCode >= 500 && statusCode < 600)
            {
                Status = ApiResponseStatus.Error.ToString();
                Message = ApiResponseMessage.InternalServerError.ToString();
            }
            else
            {
                Status = ApiResponseStatus.Error.ToString();
                Message = ApiResponseMessage.UnexpectedErrorOccurred.ToString();
            }
        }
    }

    public enum ApiResponseStatus
    {
        Success,
        Error
    }
    public enum ApiResponseMessage
    {
        RequestSuccessful,
        DataRetrievedSuccessfully,
        InvalidInputProvided,
        AuthenticationFailed,
        RecordNotFound,
        ResourceNotFound,
        AccessDenied,
        InternalServerError,
        UnableToProcessRequest,
        MissingRequiredParameters,
        InvalidRequestFormat,
        InvalidApiKey,
        RateLimitExceeded,
        DatabaseConnectionFailed,
        ExternalServiceUnavailable,
        InsufficientPermissions,
        DuplicateEntryDetected,
        UnexpectedErrorOccurred,
        InvalidFileFormat,
        ValidationError,
        PaymentRequired,
        ResourceAlreadyExists,
        DatabaseConnectionError
    }

}
