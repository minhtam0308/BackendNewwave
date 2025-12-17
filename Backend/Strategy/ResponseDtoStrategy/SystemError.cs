using BeNewNewave.DTOs;
using BeNewNewave.Interface.Strategy;

namespace BeNewNewave.Strategy.ResponseDtoStrategy
{
    public class SystemError : IResponseDtoStrategy
    {
        private string _messageServerError = "Server error contact to admin";
        public SystemError() { }
        public SystemError(string messageServerError) 
        {
            _messageServerError = messageServerError;
        }
        public ResponseDto GetResponse()
        {
            return new ResponseDto()
            {
                errorCode = 1,
                errorMessage = _messageServerError,
            };
        }
    }
}
