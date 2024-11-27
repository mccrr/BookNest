namespace BookNest.Utils
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Status {  get; set; }

        public BaseResponse() { }
        public BaseResponse(bool success, string message, int status) { Success = success; Message = message; }
        public static BaseResponse SuccessResponse(string message)
        {
            return new BaseResponse(true, message, 200);
        }
        public static BaseResponse Error(string message, int status)
        {
            return new BaseResponse(false, message, status);
        } 
    }
}
