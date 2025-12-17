namespace Backend.Common
{
    public enum ErrorCode
    {
        Success = 201,
        InvalidInput = 400,
        UserNotFound = 404,
        Unauthorized = 401,
        InternalServerError = 500,
        Unknown = 505
    }
}
