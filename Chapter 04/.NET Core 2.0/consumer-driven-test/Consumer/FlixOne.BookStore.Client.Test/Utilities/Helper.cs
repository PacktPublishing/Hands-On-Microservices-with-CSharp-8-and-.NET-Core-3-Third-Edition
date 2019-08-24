using System.IO;

namespace FlixOne.BookStore.Client.Test.Utilities
{
    public class Helper
    {
        public static string SpecifyDirectory(string dirName) =>
            $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}{dirName}{Path.DirectorySeparatorChar}";

    }
}