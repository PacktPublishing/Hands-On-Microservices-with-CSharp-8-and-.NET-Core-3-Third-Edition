using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FlixOne.BooStore.ReviewService.Extensions
{
    public static class StreamExtension
    {
        public static byte[] ToByteArray(this object @object)
        {
            if (@object == null)
                return null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, @object);
            return memoryStream.ToArray();
        }

        public static T FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
                return default(T);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using MemoryStream memoryStream = new MemoryStream(byteArray);
            return binaryFormatter.Deserialize(memoryStream) as T;
        }
    }
}
