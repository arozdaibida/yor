namespace yor_request_api.Application.Exceptions
{
    public class ConcurrenceNotFoundException: Exception
    {
        public ConcurrenceNotFoundException(string message)
            : base(message) { }
    }
}
