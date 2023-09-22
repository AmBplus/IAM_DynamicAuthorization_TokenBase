using Microsoft.AspNetCore.Http;
using Myrmec;

namespace Base.AspCore.CheckContentType;

public static class CheckTypeHelper
{
    // create a sniffer instance.
    private static readonly Sniffer Sniffer = new();

    // Supported Image Type
    private static readonly List<Record> ImageSupportedFiles = new()
    {
        new("jpg,jpeg", "ff,d8,ff,E0"),
        new("jpg,jpeg", "ff,d8,ff,E1"),
        new("jpg,jpeg", "ff,d8,ff,E2"),
        new("jpg,jpeg", "ff,d8,ff,E8"),
        new("webp", "52,54,53,53"),
        new("webp", "57,45,42,50"),
        new("webp", "52,49,46,46"),
        new("png", "89,50,4e,47,0d,0a,1a,0a")
    };

    static CheckTypeHelper()
    {
        // populate with mata data.
        Sniffer.Populate(ImageSupportedFiles);
    }

    /// <summary>
    ///     Send IFormFile To Check File Is Supported Image Or Not
    /// </summary>
    /// <param name="file"></param>
    /// <returns>File is Image = true,Otherwise = False</returns>
    public static bool FileIsValidImage(this IFormFile file)
    {
        // get file head byte
        var fileHead = ReadFileHead(file);
        // Start Check Match To Supported Image Type
        List<string> results = Sniffer.Match(fileHead);
        // Count > 0 Return True
        return Convert.ToBoolean(results.Count);
    }

    /// <summary>
    ///     Read 20 Start Byte
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    private static byte[] ReadFileHead(IFormFile file)
    {
        using var fs = new BinaryReader(file.OpenReadStream());
        var bytes = new byte[20];
        fs.Read(bytes, 0, 20);
        return bytes;
    }
}
