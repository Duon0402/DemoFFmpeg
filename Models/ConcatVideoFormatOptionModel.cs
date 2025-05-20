using Xabe.FFmpeg;

namespace DemoFFmpeg.Models
{
    public class ConcatVideoFormatOptionModel
    {
        public VideoCodec VideoCodec { get; set; }
        public AudioCodec AudioCodec { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Framerate { get; set; }
    }
}
