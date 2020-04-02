using FlixOne.BookStore.OrderService.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlixOne.BookStore.OrderService.Extensions
{
    /// <summary>
    /// Respons eextension
    /// </summary>
    public static class ResponseExtension
    {
        /// <summary>
        /// Bad request
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static ErrorResponseModel BadRequestDueToModelState(this ModelStateDictionary modelState)
        {

            return new ErrorResponseModel
            {
                Message = modelState.ToErrorMessage(),
                Code = 400,
                Exception = modelState.GetType().Name
            };
        }
    }
}
