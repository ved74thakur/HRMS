namespace leaveApplication2.Other
{
    public class CustomLeaveException : Exception
    {
        public int StatusCode { get; set; }

        public CustomLeaveException(string message) : base(message)
        {
        }

    
        public CustomLeaveException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
