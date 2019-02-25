using System;

namespace Utf8Json.Formatters
{
    public sealed class IgnoreFormatter<T> : JsonFormatterBase<T>
    {
        public override void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteNull();
        }

        public override T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            reader.ReadNextBlock();
            return default(T);
        }
    }
}
