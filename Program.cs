using Xabe.FFmpeg;

namespace DemoFFmpeg
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                // Đặt đường dẫn cho ffmpeg và ffprobe
                SetExecutablesPath();

                string filePath = Path.Combine(AppContext.BaseDirectory, "assets", "video_01.mp4");

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Không tìm thấy file video tại:", filePath);
                }

                var inputPaths = new List<string> { filePath };
                var imagePath = Path.Combine(AppContext.BaseDirectory, "assets", "watermark.png");
                await SetWatermark(inputPaths, imagePath, Position.Center);
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

        private static async Task SetWatermark(List<string> inputPaths, string imagePath, Position position, string? outputPath = null)
        {
            if (inputPaths == null || inputPaths.Count == 0)
            {
                throw new ArgumentException("Danh sách đường dẫn video không được rỗng.", nameof(inputPaths));
            }

            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                throw new FileNotFoundException($"Không tìm thấy file hình ảnh tại đường dẫn: {imagePath}");
            }

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                outputPath = Path.Combine(AppContext.BaseDirectory, "outputs");
            }

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var errorFiles = new List<string>();

            foreach (var inputPath in inputPaths)
            {
                if (!File.Exists(inputPath))
                {
                    errorFiles.Add(inputPath);
                    continue;
                }

                try
                {
                    var outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_watermarked.mp4";
                    var outputFilePath = Path.Combine(outputPath, outputFileName);

                    var conversion = FFmpeg.Conversions.New()
                        .AddParameter($"-i \"{inputPath}\"", ParameterPosition.PreInput)
                        .AddParameter($"-i \"{imagePath}\"", ParameterPosition.PreInput)
                        .AddParameter($"-filter_complex \"[0:v][1:v] overlay=W-w-10:H-h-10\"")
                        .AddParameter("-map 0:v")
                        .AddParameter("-map 0:a?")
                        .AddParameter("-c:v libx264")
                        .AddParameter("-c:a aac")
                        .SetOutput(outputFilePath);

                    await conversion.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi xử lý file '{inputPath}': {ex.Message}");
                    errorFiles.Add(inputPath);
                }
            }

            if (errorFiles.Count > 0)
            {
                Console.WriteLine($"Không thể xử lý các file video sau: {string.Join(", ", errorFiles.Select(x => Path.GetFileNameWithoutExtension(x)))}");
            }
        }

        #endregion

        #region Models 

        #endregion
    }
}
