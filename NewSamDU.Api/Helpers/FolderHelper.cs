namespace NewSamDU.Api.Helpers;

public class FolderHelper
{
    protected string wwwRootPath;

    protected string mainFolderName;

    public FolderHelper(IWebHostEnvironment env, string mainFolder)
    {
        wwwRootPath = Path.Combine(env.WebRootPath, mainFolder);

        mainFolderName = mainFolder;

        if (!Directory.Exists(wwwRootPath))
            Directory.CreateDirectory(wwwRootPath);
    }

    public string GetMainFoldername()
    {
        return mainFolderName;
    }

    public List<string> GetFolders(string folder = "/")
    {
        string path = ResolvePathOrThrow(CleanName(folder));

        return Directory.EnumerateDirectories(path).Select(Path.GetFileName).ToList()!;
    }

    public string GetFolderPath(string folder)
    {
        folder = CleanName(folder);

        return ResolvePathOrThrow(folder);
    }

    public bool CreateFolder(string path, string folderName)
    {
        path = CleanName(path);

        string parentPath = path = Path.GetFullPath(Path.Combine(wwwRootPath, path));

        EnsureInsideRoot(parentPath);

        if (!Directory.Exists(parentPath))
            throw new DirectoryNotFoundException($"Parent folder not found: {path}");

        string cleanName = CleanName(folderName);

        string target = Path.Combine(parentPath, cleanName);
        string absoluteTarget = Path.GetFullPath(target);
        EnsureInsideRoot(absoluteTarget);

        if (Directory.Exists(absoluteTarget))
            return false;

        Directory.CreateDirectory(absoluteTarget);
        return true;
    }

    private string CleanName(string path)
    {
        path =
            path?.Trim(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) ?? string.Empty;

        var segments = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        foreach (var segment in segments)
        {
            if (segment.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException($"Invalid characters in path segment: {segment}");
        }
        return path;
    }

    protected void EnsureInsideRoot(string fullPath)
    {
        if (fullPath == wwwRootPath)
        {
            return;
        }

        string root = Path.GetFullPath(wwwRootPath);
        if (!fullPath.StartsWith(root + Path.DirectorySeparatorChar, StringComparison.Ordinal))
            throw new UnauthorizedAccessException("Access denied: path escapes wwwroot directory.");
    }

    public string ResolvePathOrThrow(string path)
    {
        string combinedPath = Path.GetFullPath(Path.Combine(wwwRootPath, path));

        if (!combinedPath.StartsWith(wwwRootPath, StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedAccessException("Access denied: outside of root.");
        if (!Directory.Exists(combinedPath))
            throw new DirectoryNotFoundException($"Directory not found: {combinedPath}");

        return combinedPath;
    }

    public bool DeleteFolder(string path)
    {
        string cleanPath = CleanName(path);
        string fullPath = ResolvePathOrThrow(cleanPath);

        if (string.Equals(fullPath, wwwRootPath, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot delete root directory.");

        try
        {
            Directory.Delete(fullPath, recursive: true);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            throw new UnauthorizedAccessException("Access denied while deleting folder.");
        }
        catch (IOException)
        {
            throw new IOException(
                "The folder or one of its files is in use and cannot be deleted."
            );
        }
    }

    public bool CheckIfFolderEmpty(string folderName)
    {
        folderName =
            folderName?.Trim(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            ?? string.Empty;

        if (string.IsNullOrWhiteSpace(folderName))
            throw new ArgumentException("Folder name cannot be empty.", nameof(folderName));

        string combinedPath = Path.Combine(wwwRootPath, folderName);
        string fullPath = Path.GetFullPath(combinedPath);

        if (!fullPath.StartsWith(wwwRootPath, StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedAccessException("Access denied: path escapes wwwroot directory.");

        if (!Directory.Exists(fullPath))
            throw new DirectoryNotFoundException($"The directory '{folderName}' does not exist.");

        return !Directory.EnumerateFileSystemEntries(fullPath).Any();
    }

    public bool RenameFolder(string path, string oldName, string newName)
    {
        try
        {
            path = CleanName(path);
            oldName = CleanName(oldName);
            newName = CleanName(newName);

            string oldPath = ResolvePathOrThrow(Path.Combine(path, oldName));

            string newPath = Path.GetFullPath(Path.Combine(wwwRootPath, path, newName));

            EnsureInsideRoot(newPath);

            if (Directory.Exists(newPath))
                throw new IOException($"A directory with the name '{newName}' already exists.");

            Directory.Move(oldPath, newPath);
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }
}
