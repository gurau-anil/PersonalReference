using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace UtilBox
{
    public static class FileUtils
    {

        public static bool FileExists(string path) => File.Exists(path);
        public static void DeleteFile(string path)
        {
            if (FileExists(path))
                File.Delete(path);
        }

        public static string GetFileExtension(string path)
        {
            if (FileExists(path))
            {
                return Path.GetExtension(path)?.TrimStart('.') ?? String.Empty;
            }
            return String.Empty;
        }


        public static FileInfo? GetFileInfo(IHostEnvironment env, string path)
        {
            if (FileExists(path))
                return new FileInfo(path);
            return null;
        }

        public static string GetSafeFileName(string name, string replacement = "_")
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c.ToString(), replacement);
            return name;
        }

        public static string GetTempPath() => Path.GetTempPath();

        public static string CreateTempFile() => Path.GetTempFileName();


        public static string CreateTempDirectory()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(path);
            return path;
        }

        public static string GetMimeType(string extension)
        {
            return extension.ToLower() switch
            {
                "txt" => "text/plain",
                "html" => "text/html",
                "jpg" or "jpeg" => "image/jpeg",
                "png" => "image/png",
                "pdf" => "application/pdf",
                _ => "application/octet-stream"
            };
        }


        public static string GetFileSizeString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB" };
            if (byteCount == 0) return "0 B";
            int place = Convert.ToInt32(Math.Floor(Math.Log(byteCount, 1024)));
            double num = byteCount / Math.Pow(1024, place);
            return $"{num:0.##} {suf[place]}";
        }

        // Read file text (sync + async)
        public static string ReadText(string path) => File.ReadAllText(path);
        public static Task<string> ReadTextAsync(string path) => File.ReadAllTextAsync(path);

        // Write text to file (sync + async)
        public static void WriteText(string path, string content, bool append = false)
        {
            if (append)
                File.AppendAllText(path, content);
            else
                File.WriteAllText(path, content);
        }

        public static Stream ReadStream(string path) => File.OpenRead(path);

        public static void WriteStream(string path, Stream inputStream)
        {
            using (var output = File.Create(path))
            {
                inputStream.CopyTo(output);
            }
        }


        public static Task WriteTextAsync(string path, string content, bool append = false)
        {
            if (append)
                return File.AppendAllTextAsync(path, content);
            else
                return File.WriteAllTextAsync(path, content);
        }

        public static byte[] ReadBytes(string path) => File.ReadAllBytes(path);
        public static void WriteBytes(string path, byte[] data) => File.WriteAllBytes(path, data);

        public static string GetProjectRootPath(IHostEnvironment env) => env.ContentRootPath;
        public static string GetWwwRootPath(IHostEnvironment env) => Path.Combine(env.ContentRootPath, "wwwroot");
        public static IFileInfo? GetWwwRootFileInfo(IHostEnvironment env, string path) {
            if(FileExists(Path.Combine(GetWwwRootPath(env), path)))
                return env.ContentRootFileProvider.GetFileInfo(Path.Combine("wwwroot", path));
            return null;
        }




        public static bool DirectoryExists(string path) => Directory.Exists(path);

        public static void CreateDirectory(string path, string directoryName)
        {
            string dirPath = Path.Combine(path, directoryName);
            if (!DirectoryExists(dirPath))
                Directory.CreateDirectory(dirPath);
        }

        public static void DeleteDirectory(string path)
        {
            if(DirectoryExists(path))
                Directory.Delete(path, true);
        }

        public static DirectoryInfo? GetDirectoryInfo(string path)
        {
            if (DirectoryExists(path))
                return new DirectoryInfo(path);
            return null;
        }
 
    }
}
