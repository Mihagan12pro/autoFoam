using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AutoFoam.UI.Extensions
{
    internal static class ValidationResultsExtensions
    {
        public static IEnumerable<string> ToStrings(this List<ValidationResult> validationResults)
            => validationResults.Select(vr => vr.ErrorMessage);
    }
}
