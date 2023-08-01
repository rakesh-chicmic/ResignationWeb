using System.Net;

namespace ResignationWeb.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
