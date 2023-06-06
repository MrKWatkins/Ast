namespace MrKWatkins.Ast.Tests.Position;

public abstract class FileTextFixture : EqualityTestFixture
{
    protected static void WithTempFile(Action<FileInfo> action)
    {
        var tempFile = new FileInfo(Path.GetTempFileName());
        try
        {
            action(tempFile);
        }
        finally
        {
            DeleteTempFile(tempFile);
        }
    }

    // Yes, I know they're temp, but I've seen temp folders clog up. Let's at least try and be a good citizen.
    private static void DeleteTempFile(FileInfo tempFile)
    {
        try
        {
            File.Delete(tempFile.FullName);
        }
        catch
        {
            // Ignore the exception. We tried our best.
        }
    }
}