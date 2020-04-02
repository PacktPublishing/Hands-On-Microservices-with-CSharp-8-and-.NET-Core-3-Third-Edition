using FlixOne.BookStore.ReviewService.Extensions;
using FlixOne.BooStore.ReviewService.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlixOne.BookStore.StoreService.Extensions
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
