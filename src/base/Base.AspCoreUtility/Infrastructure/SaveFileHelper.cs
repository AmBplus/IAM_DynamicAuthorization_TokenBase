using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Base.AspCore.CheckContentType;
using Base.AspCore.Infrastructure;

namespace Base.AspCore.Infrastructure;

public static class SaveFileHelper
{
    public static async Task<string> SaveFormFile(this IFormFile file, IWebHostEnvironment hostEnvironment,
        string path)
    {
        if (!(file != null && file.Length > 0)) return null;
        if (!file.FileIsValidImage()) return null;
        var fileExtension = Path.GetExtension(file.FileName);
        var relativePath = Path.Combine(path,
            Guid.NewGuid().ToString()) + fileExtension;
        var absolutePath = hostEnvironment.WebRootPath + Path.DirectorySeparatorChar + relativePath;
        var directoryPath = Path.GetDirectoryName(absolutePath);
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        using (var stream = File.Create(absolutePath))
        {
            await file.CopyToAsync(stream);
        }

        return relativePath;
    }

    public static async Task<IList<string>> SaveRangeFormFile(this IList<IFormFile> files,
        IWebHostEnvironment hostEnvironment,
        string relativePath)
    {
        IList<string> imagePath = new List<string>();
        foreach (var image in files)
        {
            if (!image.FileIsValidImage()) continue;
            var fileName = await image.SaveFormFile(hostEnvironment, relativePath);
            if (!string.IsNullOrWhiteSpace(fileName)) imagePath.Add(fileName);
        }

        return imagePath;
    }
}
