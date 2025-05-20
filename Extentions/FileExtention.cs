namespace DemoFFmpeg.Extentions
{
    public static class FileExtention
    {
        public static List<string> CheckFilesExist(this IEnumerable<string> filePaths)
        {
            if (filePaths == null)
                return new List<string>();

            return filePaths
                .Where(path => !string.IsNullOrWhiteSpace(path))
                .Select(path => path.Trim())
                .Where(path => !File.Exists(path))
                .Distinct()
                .ToList();
        }
    }
}
