using Microsoft.AspNetCore.Hosting;

namespace Base.AspCore.Infrastructure;

//public static class RemoveFileHelper
//{
//    public static async Task RemoveFile(IWebHostEnvironment hostEnvironment, string path, ILoggerManger logger)
//    {
//        if (string.IsNullOrWhiteSpace(path))
//        {
//            var absolutePath = hostEnvironment.WebRootPath + path;
//            if (File.Exists(absolutePath))
//                try
//                {
//                    File.Delete(absolutePath);
//                }
//                catch (Exception e)
//                {
//                    logger.LogError(e, e.Message);
//                }
//        }
//    }

//    public static async Task RemoveRangeFiles(IWebHostEnvironment hostEnvironment, IList<string> pathFiles,
//        ILoggerManger logger)
//    {
//        foreach (var pathFile in pathFiles) await RemoveFile(hostEnvironment, pathFile, logger);
//    }
//}
