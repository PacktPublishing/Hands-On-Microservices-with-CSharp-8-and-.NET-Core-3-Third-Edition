using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace FlixOne.BookStore.StoreService.Extensions
{
    public static class ModelStateExtension
    {
        /// <summary>
        /// Get comma separated error messages
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns>Error message</returns>
        public static string ToErrorMessage(this ModelStateDictionary modelState)
        {
            return string.Join(",",
                    modelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());
        }

        /// <summary>
        /// Add errors to ModelState
        /// </summary>
        /// <param name="identityResult"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static ModelStateDictionary AddErrorsToModelState(this ModelStateDictionary modelState, IdentityResult identityResult)
        {
            foreach (var e in identityResult.Errors)
            {
                modelState.TryAddModelError(e.Code, e.Description);
            }

            return modelState;
        }

        /// <summary>
        /// Add error to ModelState
        /// </summary>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static ModelStateDictionary AddErrorToModelState(this ModelStateDictionary modelState, string code, string description)
        {
            modelState.TryAddModelError(code, description);
            return modelState;
        }
    }
}
