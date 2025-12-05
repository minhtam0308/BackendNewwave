namespace Backend.Exceptions
{
    public class FEException : Exception
    {
        public int ErrorCode { get; }

        public FEException(string message, int errorCode = 2)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
