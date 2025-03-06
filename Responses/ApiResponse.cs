
using System.Net;

namespace TodoApi.Responses;
    public class ApiResponse
    {
        public int? Status { get; set; }

        public string? Message { get; set; }

        public object? Data { get; set; }
        

           public static ApiResponse Created(string? message = "Success", object? data = null)
        {
        return new ApiResponse
        {
            Message = message,
            Data = data,
            Status = HttpStatusCode.Created.GetHashCode()
        };
        }
        

         public static ApiResponse Ok(string? message = "Success", object? data = null)
    {
        return new ApiResponse
        {
            Message = message,
            Data = data,
            Status = HttpStatusCode.OK.GetHashCode()
        };
    }

        public static ApiResponse Notfound(string? message = "Not found")
    {
        return new ApiResponse
        {
            Message = message,
            Data = null,
            Status = HttpStatusCode.NotFound.GetHashCode()
        };
    }
    }