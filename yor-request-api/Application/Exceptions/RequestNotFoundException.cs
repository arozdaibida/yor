namespace yor_request_api.Application.Exceptions
{
    public class RequestNotFoundException: Exception
    {
        public RequestNotFoundException(string message)
            : base(message) { }
    }
}
