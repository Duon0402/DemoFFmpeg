using Xabe.FFmpeg;

namespace DemoFFmpeg
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                SetExecutablesPath();

                var mediaInfo = await FFmpeg.GetMediaInfo("video_02.mp4");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã có lỗi xảy ra: \n {ex}");
            }
        }

        #region Private Methods
        private static void SetExecutablesPath()
        {
            string appDirectory = AppContext.BaseDirectory;

            if (string.IsNullOrWhiteSpace(appDirectory))
            {
                throw new InvalidOperationException("Không thể xác định thư mục chứa ứng dụng.");
            }

            string ffmpegPath = Path.Combine(appDirectory, "ffmpeg");

            string ffmpegExe = Path.Combine(ffmpegPath, "ffmpeg.exe");
            string ffprobeExe = Path.Combine(ffmpegPath, "ffprobe.exe");

            if (!File.Exists(ffmpegExe))
            {
                throw new FileNotFoundException("Không tìm thấy ffmpeg.exe tại đường dẫn:", ffmpegExe);
            }

            if (!File.Exists(ffprobeExe))
            {
                throw new FileNotFoundException("Không tìm thấy ffprobe.exe tại đường dẫn:", ffprobeExe);
            }

            FFmpeg.SetExecutablesPath(ffmpegPath);
        }

        
        #endregion
    }
}
