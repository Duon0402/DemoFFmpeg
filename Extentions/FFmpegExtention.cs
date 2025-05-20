using Xabe.FFmpeg;

namespace DemoFFmpeg.Extentions
{
    public static class FFmpegExtention
    {
        public static async Task<bool> IsSimilarFormat(IEnumerable<string> inputPaths, CheckSimilarOptions checkOptions = CheckSimilarOptions.All)
        {
            var notFoundFiles = inputPaths.CheckFilesExist();

            if (notFoundFiles.Any())
            {
                Console.WriteLine("Không tìm thấy các file:");
                notFoundFiles.ForEach(Console.WriteLine);
                return false;
            }

            var mediaInfos = new List<IMediaInfo>();
            foreach (var path in inputPaths)
            {
                var info = await FFmpeg.GetMediaInfo(path);
                mediaInfos.Add(info);
            }

            switch (checkOptions)
            {

                case CheckSimilarOptions.All:
                    // Kiểm tra tất cả các file
                    break;
                case CheckSimilarOptions.Video:
                    // Kiểm tra định dạng video
                    break;
                case CheckSimilarOptions.Audio:
                    // Kiểm tra định dạng âm thanh
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(checkOptions), checkOptions, null);
            }

            return true;
        }

        public enum CheckSimilarOptions
        {
            All,
            Video,
            Audio
        }
    }
}