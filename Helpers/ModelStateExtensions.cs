using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HanuMediSoftCore.Helpers
{
    public static class ModelStateExtensions
    {
        public static string? GetValidationMessage(this ModelStateDictionary modelState, string key)
        {
            if (!modelState.ContainsKey(key))
                return null;

            var entry = modelState[key];
            if (entry?.Errors.Count > 0)
                return entry.Errors[0].ErrorMessage;

            return null;
        }
    }
}

