using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Volta.Common
{
    public static class SerializationUtils
    {
        public static Stream Serialize(object obj) {
            var stream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, obj);

            return stream;
        }

        public static T Deserialize<T>(byte[] bytes) {
            using var ms = new MemoryStream(bytes);
            return Deserialize<T>(ms);
        }

        public static T Deserialize<T>(Stream stream) {
            var binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
}