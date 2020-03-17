using System;
using System.Text.Json;

namespace FlixOne.BookStore.UserService.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class ErrorResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
    public class SuccessResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
