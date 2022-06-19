using Microsoft.AspNetCore.Mvc;
using Sofomo.Network;

namespace Sofomo.Api
{
    public static class ControllerExtension
    {
        public static JsonResult JsonApi(this ControllerBase controller)
        {
            ApiResponse response = new ApiResponse
            {
                Success = true
            };

            JsonResult result = new JsonResult(response);

            return result;
        }

        public static JsonResult JsonApi(this ControllerBase controller, object data)
        {
            ApiResponse response = new ApiResponse
            {
                Data = data,
                Success = true
            };

            JsonResult result = new JsonResult(response);

            return result;
        }

        public static JsonResult JsonApi(this ControllerBase controller, Exception exception)
        {
            ApiResponse response = new ApiResponse
            {
                Success = false,
                Errors = new[] {exception.Message}
            };

            JsonResult result = new JsonResult(response);

            return result;
        }
    }
}