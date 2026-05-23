using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

       
        public static ApiResponse<T> Success(T data, string message = "Operation completed successfully.")
        {
            return new ApiResponse<T> { IsSuccess = true, Data = data, Message = message };
        }

   
        public static ApiResponse<T> Failure(List<string> errors, string message = "Operation failed.")
        {
            return new ApiResponse<T> { IsSuccess = false, Errors = errors, Message = message };
        }
    }
}
