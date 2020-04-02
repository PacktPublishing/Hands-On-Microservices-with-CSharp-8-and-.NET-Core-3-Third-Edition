using FlixOne.BookStore.StoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlixOne.BookStore.StoreService.Extensions
{
    public static class ResponseExtension
    {
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
