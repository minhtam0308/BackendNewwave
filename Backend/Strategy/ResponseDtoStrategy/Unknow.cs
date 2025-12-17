using Backend.Common;
using BeNewNewave.DTOs;
using BeNewNewave.Interface.Strategy;

namespace Backend.Strategy.ResponseDtoStrategy
{
    public class Unknow : IResponseDtoStrategy
    {
        private string _messageServerError = "Unknown error";
        public Unknow() { }
        public Unknow(string messageServerError)
        {
            _messageServerError = messageServerError;
        }
        public ResponseDto GetResponse()
        {
            return new ResponseDto()
            {
                errorCode = ErrorCode.Unknown,
                errorMessage = _messageServerError,
            };
        }
    }
}
