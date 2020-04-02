using System.Text.Json;

namespace FlixOne.BookStore.StoreService.Models
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

        // other fields

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    public class SuccessResponseModel
    {

        public int Code { get; set; }
        public string Message { get; set; }

        // other fields

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}