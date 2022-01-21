using System.Text;
using System.Xml.Serialization;
using MusicPlayer.API.Core;

namespace MusicPlayer.API.IO
{
    public class XMLPlaylistIO : PlaylistIO
    {
        protected override List<Song> Deserialize(string data)
        {
            return XmlDeserializeFromString<List<Song>>(data);
        }

        protected override string Serialize(List<Song> songs)
        {
            return XmlSerializeToString(songs);
        }

        private static string XmlSerializeToString(object objectInstance)
        {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, objectInstance);
            }

            return sb.ToString();
        }

        private static T XmlDeserializeFromString<T>(string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        private static object XmlDeserializeFromString(string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader) ?? throw new InvalidDataException();
            }

            return result;
        }
    }
}