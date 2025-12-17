using Backend.Common;
using BeNewNewave.DTOs;
using BeNewNewave.Interface.Strategy;
using Microsoft.AspNetCore.Diagnostics;

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
                errorCode = ErrorCode.InternalServerError,
                errorMessage = _messageServerError,
            };
        }
    }
}
