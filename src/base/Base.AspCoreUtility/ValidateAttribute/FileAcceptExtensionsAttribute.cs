using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Base.AspCore.ValidateAttribute;

public class FileAcceptExtensionsAttribute : ValidationAttribute, IClientModelValidator
{
    public FileAcceptExtensionsAttribute(string[] validExtensions)
    {
        ValidExtensions = validExtensions;
    }

    private string[] ValidExtensions { get; }

    public void AddValidation(ClientModelValidationContext context)
    {
        var validEx = string.Empty;
        for (var i = 0; i < ValidExtensions.Length; i++)
            if (i == ValidExtensions.Length - 1)
                validEx += ValidExtensions[i];
            else
                validEx += $"{ValidExtensions[i]},";
        if (ErrorMessageResourceName != null)
            context.Attributes.Add("data-val-ValidFileExtension", ErrorMessageResourceName);
        else
            context.Attributes.Add("data-val-ValidFileExtension", $" فرمت های صحیح فقط {validEx}");
        context.Attributes.Add("accept", validEx);
    }

    public override bool IsValid(object? value)
    {
        if (value == null) return true;
        if (value is IFormFile file)
            return IsValidFile(file);
        if (value is ICollection<IFormFile> files) return IsValidFiles(files);
        return false;
    }

    public bool IsValidFiles(ICollection<IFormFile> formFiles)
    {
        foreach (var file in formFiles)
            if (!IsValidFile(file))
                return false;
        return true;
    }

    public bool IsValidFile(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        if (ValidExtensions.Contains(fileExtension)) return true;
        return false;
    }
}
