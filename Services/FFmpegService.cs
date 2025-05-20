using DemoFFmpeg.Extentions;
using DemoFFmpeg.Models;

namespace DemoFFmpeg.Services
{
    public static class FFmpegService
    {
        public static async Task ConcatVideosAsync(List<string> inputPaths, ConcatVideoFormatOptionModel? formatOptions, string? outputDircectory)
        {
            if (inputPaths == null || inputPaths.Count < 2)
            {
                throw new ArgumentException("Cần ít nhất 2 video để nối.");
            }

            var filesNotFound = inputPaths.CheckFilesExist();
            if (filesNotFound.Any())
            {
                Console.WriteLine("Không tìm thấy các file:");
                filesNotFound.ForEach(Console.WriteLine);
                return;
            }

            if(formatOptions == null)
            {

            }
        }
    }
}
