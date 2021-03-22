using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace WCT.Infrastructure.Extensions
{
    public static class ModelState
    {
        public static void AddErrors(this ModelStateDictionary modelState,
           IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                if (errors != null && !modelState.Any(e => e.Key == error.Code))
                {
                    modelState.AddModelError(error.Code, error.Description);
                }
            }
        }
    }
}