using System;
using NewSamDU.Application.DTOs.FileDTO;

namespace NewSamDU.Api.Helpers;

public class FileHelper
{
    public FileHelper(FolderHelper fHelper, IWebHostEnvironment env)
    {
        wwwRootPath = Path.Combine(env.WebRootPath, fHelper.GetMainFoldername());
        mainFolderName = fHelper.GetMainFoldername();
        folderHelper = fHelper;

        if (!Directory.Exists(wwwRootPath))
            Directory.CreateDirectory(wwwRootPath);
    }

    protected string wwwRootPath;
    protected string mainFolderName;

    protected FolderHelper folderHelper;

    public List<GetFileDTO> GetFiles(string path = "/")
    {
        if (path == "/")
        {
            var files = Directory
                .GetFiles(wwwRootPath)
                .Select(path => new
                {
                    Name = Path.GetFileName(path),
                    Size = new FileInfo(path).Length,
                })
                .ToList();
            return Directory
                .GetFiles(wwwRootPath)
                .Select(f => new GetFileDTO
                {
                    Name = Path.GetFileName(f),
                    Size = new FileInfo(f).Length / 1024,
                })
                .ToList()!;
        }
        path = folderHelper.GetFolderPath(path);

        return Directory
            .GetFiles(path)
            .Select(f => new GetFileDTO
            {
                Name = Path.GetFileName(f),
                Size = new FileInfo(f).Length,
            })
            .ToList()!;
    }

    public async Task<string> UploadFile(IFormFile file, string folderPath)
    {
        folderPath = folderHelper.GetFolderPath(folderPath);

        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        string safeName = $"{DateTime.Now:yyyyMMdd_HHmmssfff}_{fileName}{extension}";
        string path = Path.Combine(folderPath, safeName);

#warning bu yerda pathni to'liq qilish kerak faqat vaqt bo'lib qolayapti

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return path;
    }

    public bool DeleteFile(string filePath)
    {
        string path = ResolveFileOrThrow(CleanName(filePath));

        File.Delete(path);
        return true;
    }

    public string ResolveFileOrThrow(string filePath)
    {
        string combinedPath = Path.GetFullPath(Path.Combine(wwwRootPath, filePath));

        if (!combinedPath.StartsWith(wwwRootPath, StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedAccessException("Access denied: outside of root.");
        if (!File.Exists(combinedPath))
            throw new FileNotFoundException($"File not found: {combinedPath}");

        return combinedPath;
    }

    private string CleanName(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty.");

        path = path.Trim(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        var segments = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        foreach (var segment in segments)
        {
            if (segment.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException($"Invalid characters in path segment: {segment}");
        }

        return path;
    }
}
