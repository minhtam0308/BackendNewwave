using Backend.Common;
using Backend.Strategy.ResponseDtoStrategy;
using BeNewNewave.Interface.Strategy;
using BeNewNewave.Strategy.ResponseDtoStrategy;

namespace BeNewNewave.DTOs
{
    public class ResponseDto
    {
        private IResponseDtoStrategy _responseDto = new SystemError();
        public ErrorCode errorCode { get; set; } = ErrorCode.Success;
        public string errorMessage { get; set; } = "success";
        public object data { get; set; } = new();



        public IResponseDtoStrategy GetResponseDtoStrategy()
        {
            return _responseDto; 
        }

        public void SetResponseDtoStrategy(IResponseDtoStrategy value)
        {
            _responseDto = value;
        }

        public ResponseDto GetResponseDto() 
        {
            return _responseDto.GetResponse();
        }

        public ResponseDto GenerateStrategyResponseDto(ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.InvalidInput:
                    SetResponseDtoStrategy(new UserError());
                    return GetResponseDto();
                case ErrorCode.InternalServerError:
                    SetResponseDtoStrategy(new SystemError());
                    return GetResponseDto();
                case ErrorCode.Success:
                    SetResponseDtoStrategy(new Success());
                    return GetResponseDto();
                default:
                    SetResponseDtoStrategy(new Unknow());
                    return GetResponseDto();
            }
        }

    }
}
