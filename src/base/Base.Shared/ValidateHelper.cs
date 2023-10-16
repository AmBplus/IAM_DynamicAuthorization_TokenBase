
using Base.Shared.ResultUtility;
using System.ComponentModel.DataAnnotations;

namespace Base.Shared;

public static class ValidationHelper
{
    static ValidationHelper()
    {
    }

    public static ResultOperation GetFailedResultWithError_s(this IList<ValidationResult> validationResults)
    {
        var errors = validationResults.Select(x => x.ErrorMessage);
        return ResultOperation.ToFailedResult(errors!);
    }

    public static bool IsValid<T>(this T entity) where T : class
    {
        var validationContext =
            new ValidationContext(instance: entity);

        var validationResults =
            new List<ValidationResult>();

        var isValid =
            Validator
                .TryValidateObject(instance: entity, validationContext: validationContext,
                    validationResults: validationResults, validateAllProperties: true);
        
        return isValid;
    }


    public static ResultOperation
        GetValidationResults<T>(this T entity) where T : class
    {
        var validationContext =
            new ValidationContext(instance: entity);

        var validationResults =
            new List<ValidationResult>();

        //var isValid =
        Validator
            .TryValidateObject(instance: entity, validationContext: validationContext,
                validationResults: validationResults, validateAllProperties: true);
       
        if (validationResults.Count > 0)
        {
            var resultError = validationResults.GetFailedResultWithError_s();
            return ResultOperation.ToFailedResult(resultError.message);
        }
       
        return ResultOperation.ToSuccessResult();
    }

}
