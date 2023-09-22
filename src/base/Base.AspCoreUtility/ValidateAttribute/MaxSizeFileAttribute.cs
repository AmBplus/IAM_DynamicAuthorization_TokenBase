using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Base.AspCore.ValidateAttribute;

public class MaxSizeFileAttribute : ValidationAttribute, IClientModelValidator
{
    public MaxSizeFileAttribute(int maxSize)
    {
        MaxSize = maxSize;
    }

    private int MaxSize { get; }

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-MaxFileSize", $" حداکثر فایل اپلودی باید{MaxSize / 1024}کیلو بایت باشد ");
    }

    public override bool IsValid(object? value)
    {
        if (value == null) return true;
        if (value is not IFormFile file) return true;
        if (file.Length < MaxSize) return true;
        return false;
    }
}
