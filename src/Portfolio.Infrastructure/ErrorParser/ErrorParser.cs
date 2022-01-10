using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.Application.ErrorParser
{
    public static class ErrorParser
    {
        public static List<string> Parse(ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(m => m.Errors)
                             .Select(e => e.ErrorMessage)
                             .ToList();
        }
    }
}
