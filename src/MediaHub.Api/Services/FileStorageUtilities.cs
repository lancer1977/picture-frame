using System.Text.Json;

namespace MediaHub.Api.Services;

internal static class FileStorageUtilities
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public static T LoadOrCreate<T>(string filePath, Func<T> factory)
    {
        if (!File.Exists(filePath))
            return factory();

        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return JsonSerializer.Deserialize<T>(stream, JsonOptions) ?? factory();
    }

    public static void Save<T>(string filePath, T state)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        var tempPath = filePath + ".tmp";
        using (var stream = File.Open(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            JsonSerializer.Serialize(stream, state, JsonOptions);
        }

        File.Move(tempPath, filePath, true);
    }
}
