using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaveApplication2
{

    public static class ControllerBaseExtension
    {
        public static ActionResult<CommonResponse<T>> GetCommonResponse<T>(this ControllerBase controller, int statusCode, string message, T? data = default!)
        {
            var response = new CommonResponse<T>
            {
                Status = statusCode,
                Message = message,
                Data = data

            };
            return controller.StatusCode(statusCode, response);
        }
        public static CommonResponse<T> CreateResponse<T>(
         this ControllerBase controller,
         int statusCode,
         string message,
         T? data = default!,
         Action<ResponseOptions>? configureOptions = null)
        {
            var options = new ResponseOptions();
            configureOptions?.Invoke(options); // Allow configuring options

            var response = new CommonResponse<T>
            {
                Status = statusCode,
                Message = message,
                Data = data,
                AdditionalParameters = options.AdditionalParameters
            };

            return response;
        }

        // Renamed method with a different name
        public static CommonResponse<T> CreateResponseWithParameters<T>(this ControllerBase controller,
            int statusCode,
            string message,
            T? data = default!,
            Action<ResponseOptions>? configureOptions = null)
        {
            return CreateResponse(controller, statusCode, message, data, configureOptions);
        }
    }
    
    public class ResponseOptions
    {
        public IDictionary<string, object> AdditionalParameters { get; set; }
    }

    public class CommonResponse<T>
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public object? AdditionalParameters { get; internal set; }
    }
}

