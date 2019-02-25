using System;

namespace Utf8Json
{
    public delegate void JsonSerializeAction<T>(ref JsonWriter writer, T value, IJsonFormatterResolver resolver);
    public delegate T JsonDeserializeFunc<T>(ref JsonReader reader, IJsonFormatterResolver resolver);

    public interface IJsonFormatter
    {
        void SerializeNonGeneric(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver);
        object DeserializeNonGeneric(ref JsonReader reader, Type type, IJsonFormatterResolver formatterResolver);
    }

    public interface IJsonFormatter<T> : IJsonFormatter
    {
        void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }

    public interface IObjectPropertyNameFormatter<T>
    {
        void SerializeToPropertyName(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        T DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }

    public abstract class JsonFormatterBase<T> : IJsonFormatter<T>
    {
        public void SerializeNonGeneric(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
        {
            if (!(value is T))
            {
                throw new Exception($"{nameof(value)} should be of type {typeof(T)}");
            }
            Serialize(ref writer, (T)value, formatterResolver);
        }

        public object DeserializeNonGeneric(ref JsonReader reader, Type type, IJsonFormatterResolver formatterResolver)
        {
            if (type != typeof(T))
            {
                throw new Exception($"{nameof(type)} should be of type {typeof(T)}");
            }
            return Deserialize(ref reader, formatterResolver);
        }

        public abstract void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        public abstract T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }

    public static class JsonFormatterExtensions
    {
        public static string ToJsonString<T>(this IJsonFormatter<T> formatter, T value, IJsonFormatterResolver formatterResolver)
        {
            var writer = new JsonWriter();
            formatter.Serialize(ref writer, value, formatterResolver);
            return writer.ToString();
        }
    }
}