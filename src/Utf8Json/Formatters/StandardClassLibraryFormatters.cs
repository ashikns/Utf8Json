using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Utf8Json.Formatters.Internal;
using Utf8Json.Internal;
using System.Text.RegularExpressions;

#if NETSTANDARD || NET_STANDARD_2_0
using System.Dynamic;
using System.Numerics;
using System.Threading.Tasks;
#endif

namespace Utf8Json.Formatters
{
    // MEMO:should write/read base64 directly like corefxlab/System.Binary.Base64
    // https://github.com/dotnet/corefxlab/tree/master/src/System.Binary.Base64/System/Binary
    public sealed class ByteArrayFormatter : JsonFormatterBase<byte[]>
    {
        public static readonly IJsonFormatter<byte[]> Default = new ByteArrayFormatter();

        public override void Serialize(ref JsonWriter writer, byte[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteString(Convert.ToBase64String(value, Base64FormattingOptions.None));
        }

        public override byte[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var str = reader.ReadString();
            return Convert.FromBase64String(str);
        }
    }

    public sealed class ByteArraySegmentFormatter : JsonFormatterBase<ArraySegment<byte>>
    {
        public static readonly IJsonFormatter<ArraySegment<byte>> Default = new ByteArraySegmentFormatter();

        public override void Serialize(ref JsonWriter writer, ArraySegment<byte> value, IJsonFormatterResolver formatterResolver)
        {
            if (value.Array == null) { writer.WriteNull(); return; }

            writer.WriteString(Convert.ToBase64String(value.Array, value.Offset, value.Count, Base64FormattingOptions.None));
        }

        public override ArraySegment<byte> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return default(ArraySegment<byte>);

            var str = reader.ReadString();
            var bytes = Convert.FromBase64String(str);
            return new ArraySegment<byte>(bytes, 0, bytes.Length);
        }
    }

    public sealed class NullableStringFormatter : JsonFormatterBase<string>, IObjectPropertyNameFormatter<string>
    {
        public static readonly IJsonFormatter<string> Default = new NullableStringFormatter();

        public override void Serialize(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString(value);
        }

        public override string Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadString();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString(value);
        }

        public string DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadString();
        }
    }

    public sealed class NullableStringArrayFormatter : JsonFormatterBase<string[]>
    {
        public static readonly NullableStringArrayFormatter Default = new NullableStringArrayFormatter();

        public override void Serialize(ref JsonWriter writer, string[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                {
                    writer.WriteString(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteString(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override string[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new string[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadString();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class CharFormatter : JsonFormatterBase<char>
    {
        public static readonly CharFormatter Default = new CharFormatter();

        // MEMO:can be improvement write directly
        public override void Serialize(ref JsonWriter writer, char value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString(value.ToString(CultureInfo.InvariantCulture));
        }

        public override char Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadString()[0];
        }
    }

    public sealed class NullableCharFormatter : JsonFormatterBase<Char?>
    {
        public static readonly NullableCharFormatter Default = new NullableCharFormatter();

        public override void Serialize(ref JsonWriter writer, Char? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                CharFormatter.Default.Serialize(ref writer, value.Value, formatterResolver);
            }
        }

        public override Char? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return CharFormatter.Default.Deserialize(ref reader, formatterResolver);
            }
        }
    }

    public sealed class CharArrayFormatter : JsonFormatterBase<char[]>
    {
        public static readonly CharArrayFormatter Default = new CharArrayFormatter();

        public override void Serialize(ref JsonWriter writer, char[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                {
                    CharFormatter.Default.Serialize(ref writer, value[0], formatterResolver);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    CharFormatter.Default.Serialize(ref writer, value[i], formatterResolver);
                }

                writer.WriteEndArray();
            }
        }

        public override char[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new char[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = CharFormatter.Default.Deserialize(ref reader, formatterResolver);
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class GuidFormatter : JsonFormatterBase<Guid>, IObjectPropertyNameFormatter<Guid>
    {
        public static readonly IJsonFormatter<Guid> Default = new GuidFormatter();

        public override void Serialize(ref JsonWriter writer, Guid value, IJsonFormatterResolver formatterResolver)
        {
            writer.EnsureCapacity(38); // unsafe, control underlying buffer manually

            writer.WriteRawUnsafe((byte)'\"');

            var rawData = writer.GetBuffer();
            new GuidBits(ref value).Write(rawData.Array, writer.CurrentOffset); // len = 36
            writer.AdvanceOffset(36);

            writer.WriteRawUnsafe((byte)'\"');
        }

        public override Guid Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var segment = reader.ReadStringSegmentUnsafe();
            return new GuidBits(ref segment).Value;
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Guid value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, formatterResolver);
        }

        public Guid DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, formatterResolver);
        }
    }

    public sealed class DecimalFormatter : JsonFormatterBase<decimal>
    {
        public static readonly IJsonFormatter<decimal> Default = new DecimalFormatter();

        readonly bool serializeAsString;

        public DecimalFormatter()
            : this(false)
        {

        }

        public DecimalFormatter(bool serializeAsString)
        {
            this.serializeAsString = serializeAsString;
        }

        public override void Serialize(ref JsonWriter writer, decimal value, IJsonFormatterResolver formatterResolver)
        {
            if (serializeAsString)
            {
                writer.WriteString(value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                // write as number format.
                writer.WriteRaw(StringEncoding.UTF8.GetBytes(value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public override decimal Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var token = reader.GetCurrentJsonToken();
            if (token == JsonToken.Number)
            {
                var number = reader.ReadNumberSegment();
                return decimal.Parse(StringEncoding.UTF8.GetString(number.Array, number.Offset, number.Count), NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (token == JsonToken.String)
            {
                return decimal.Parse(reader.ReadString(), NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else
            {
                throw new InvalidOperationException("Invalid Json Token for DecimalFormatter:" + token);
            }
        }
    }

    public sealed class UriFormatter : JsonFormatterBase<Uri>
    {
        public static readonly IJsonFormatter<Uri> Default = new UriFormatter();

        public override void Serialize(ref JsonWriter writer, Uri value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteString(value.ToString());
            }
        }

        public override Uri Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return new Uri(reader.ReadString(), UriKind.RelativeOrAbsolute);
            }
        }
    }

    public sealed class VersionFormatter : JsonFormatterBase<Version>
    {
        public static readonly IJsonFormatter<Version> Default = new VersionFormatter();

        public override void Serialize(ref JsonWriter writer, Version value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteString(value.ToString());
            }
        }

        public override Version Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return new Version(reader.ReadString());
            }
        }
    }

    public sealed class KeyValuePairFormatter<TKey, TValue> : JsonFormatterBase<KeyValuePair<TKey, TValue>>
    {
        public override void Serialize(ref JsonWriter writer, KeyValuePair<TKey, TValue> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(StandardClassLibraryFormatterHelper.keyValuePairName[0]);
            formatterResolver.GetFormatterWithVerify<TKey>().Serialize(ref writer, value.Key, formatterResolver);
            writer.WriteRaw(StandardClassLibraryFormatterHelper.keyValuePairName[1]);
            formatterResolver.GetFormatterWithVerify<TValue>().Serialize(ref writer, value.Value, formatterResolver);

            writer.WriteEndObject();
        }

        public override KeyValuePair<TKey, TValue> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, KeyValuePair can not be null.");

            TKey resultKey = default(TKey);
            TValue resultValue = default(TValue);

            reader.ReadIsBeginObjectWithVerify();

            var count = 0;
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD || NET_STANDARD_2_0
                StandardClassLibraryFormatterHelper.keyValuePairAutomata.TryGetValue(keyString, out key);
#else
                StandardClassLibraryFormatterHelper.keyValuePairAutomata.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        resultKey = formatterResolver.GetFormatterWithVerify<TKey>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        resultValue = formatterResolver.GetFormatterWithVerify<TValue>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }

            return new KeyValuePair<TKey, TValue>(resultKey, resultValue);
        }
    }

    public sealed class StringBuilderFormatter : JsonFormatterBase<StringBuilder>
    {
        public static readonly IJsonFormatter<StringBuilder> Default = new StringBuilderFormatter();

        public override void Serialize(ref JsonWriter writer, StringBuilder value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }
            writer.WriteString(value.ToString());
        }

        public override StringBuilder Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;
            return new StringBuilder(reader.ReadString());
        }
    }

    // BitArray can be represents other format...
    public sealed class BitArrayFormatter : JsonFormatterBase<BitArray>
    {
        public static readonly IJsonFormatter<BitArray> Default = new BitArrayFormatter();

        public override void Serialize(ref JsonWriter writer, BitArray value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteBeginArray();
            for (int i = 0; i < value.Length; i++)
            {
                if (i != 0) writer.WriteValueSeparator();
                writer.WriteBoolean(value[i]);
            }
            writer.WriteEndArray();
        }

        public override BitArray Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;
            reader.ReadIsBeginArrayWithVerify();
            var c = 0;
            var buffer = new ArrayBuffer<bool>(4);
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref c))
            {
                buffer.Add(reader.ReadBoolean());
            }
            return new BitArray(buffer.ToArray());
        }
    }

    public sealed class TypeFormatter : JsonFormatterBase<Type>
    {
        public static readonly TypeFormatter Default = new TypeFormatter();

#if NETSTANDARD || NET_STANDARD_2_0
        static readonly Regex SubtractFullNameRegex = new Regex(@", Version=\d+.\d+.\d+.\d+, Culture=\w+, PublicKeyToken=\w+", RegexOptions.Compiled);
#else
        static readonly Regex SubtractFullNameRegex = new Regex(@", Version=\d+.\d+.\d+.\d+, Culture=\w+, PublicKeyToken=\w+");
#endif

        bool serializeAssemblyQualifiedName;
        bool deserializeSubtractAssemblyQualifiedName;
        bool throwOnError;

        public TypeFormatter()
            : this(true, true, true)
        {

        }

        public TypeFormatter(bool serializeAssemblyQualifiedName, bool deserializeSubtractAssemblyQualifiedName, bool throwOnError)
        {
            this.serializeAssemblyQualifiedName = serializeAssemblyQualifiedName;
            this.deserializeSubtractAssemblyQualifiedName = deserializeSubtractAssemblyQualifiedName;
            this.throwOnError = throwOnError;
        }

        public override void Serialize(ref JsonWriter writer, Type value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }
            if (serializeAssemblyQualifiedName)
            {
                writer.WriteString(value.AssemblyQualifiedName);
            }
            else
            {
                writer.WriteString(value.FullName);
            }
        }

        public override Type Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var s = reader.ReadString();
            if (deserializeSubtractAssemblyQualifiedName)
            {
                s = SubtractFullNameRegex.Replace(s, "");
            }

            return Type.GetType(s, throwOnError);
        }
    }


#if NETSTANDARD || NET_STANDARD_2_0

    public sealed class BigIntegerFormatter : JsonFormatterBase<BigInteger>
    {
        public static readonly IJsonFormatter<BigInteger> Default = new BigIntegerFormatter();

        public override void Serialize(ref JsonWriter writer, BigInteger value, IJsonFormatterResolver formatterResolver)
        {
            // JSON.NET writes Integer format, not compatible.
            writer.WriteString(value.ToString(CultureInfo.InvariantCulture));
        }

        public override BigInteger Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var s = reader.ReadString();
            return BigInteger.Parse(s, CultureInfo.InvariantCulture);
        }
    }

    // Convert to [Real, Imaginary]
    public sealed class ComplexFormatter : JsonFormatterBase<Complex>
    {
        public static readonly IJsonFormatter<Complex> Default = new ComplexFormatter();

        public override void Serialize(ref JsonWriter writer, Complex value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteBeginArray();
            writer.WriteDouble(value.Real);
            writer.WriteValueSeparator();
            writer.WriteDouble(value.Imaginary);
            writer.WriteEndArray();
        }

        public override Complex Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            reader.ReadIsBeginArrayWithVerify();
            var real = reader.ReadDouble();
            reader.ReadIsValueSeparatorWithVerify();
            var imaginary = reader.ReadDouble();
            reader.ReadIsEndArrayWithVerify();

            return new Complex(real, imaginary);
        }
    }

    public sealed class ExpandoObjectFormatter : JsonFormatterBase<ExpandoObject>
    {
        public static readonly IJsonFormatter<ExpandoObject> Default = new ExpandoObjectFormatter();

        public override void Serialize(ref JsonWriter writer, ExpandoObject value, IJsonFormatterResolver formatterResolver)
        {
            var formatter = formatterResolver.GetFormatterWithVerify<IDictionary<string, object>>();
            formatter.Serialize(ref writer, (IDictionary<string, object>)value, formatterResolver);
        }

        public override ExpandoObject Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var result = new ExpandoObject() as IDictionary<string, object>;

            var objectFormatter = formatterResolver.GetFormatterWithVerify<object>();
            var c = 0;
            while (reader.ReadIsInObject(ref c))
            {
                var propName = reader.ReadPropertyName();
                var value = objectFormatter.Deserialize(ref reader, formatterResolver);
                result.Add(propName, value);
            }

            return (ExpandoObject)result;
        }
    }

    public sealed class LazyFormatter<T> : JsonFormatterBase<Lazy<T>>
    {
        public override void Serialize(ref JsonWriter writer, Lazy<T> value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }
            formatterResolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Value, formatterResolver);
        }

        public override Lazy<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            // deserialize immediately(no delay, because capture byte[] causes memory leak)
            var v = formatterResolver.GetFormatterWithVerify<T>().Deserialize(ref reader, formatterResolver);
#if NETSTANDARD || NET_STANDARD_2_0
            return new Lazy<T>(v.AsFunc());
#else
            return new Lazy<T>(() => v);
#endif
        }
    }

    public sealed class TaskUnitFormatter : JsonFormatterBase<Task>
    {
        public static readonly IJsonFormatter<Task> Default = new TaskUnitFormatter();
        static readonly Task CompletedTask = Task.FromResult<object>(null);

        public override void Serialize(ref JsonWriter writer, Task value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            value.Wait(); // wait!
            writer.WriteNull();
        }

        public override Task Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (!reader.ReadIsNull()) throw new InvalidOperationException("Invalid input");

            return CompletedTask;
        }
    }

    public sealed class TaskValueFormatter<T> : JsonFormatterBase<Task<T>>
    {
        public override void Serialize(ref JsonWriter writer, Task<T> value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            // value.Result -> wait...!
            formatterResolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Result, formatterResolver);
        }

        public override Task<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var v = formatterResolver.GetFormatterWithVerify<T>().Deserialize(ref reader, formatterResolver);
            return Task.FromResult(v);
        }
    }

    public sealed class ValueTaskFormatter<T> : JsonFormatterBase<ValueTask<T>>
    {
        public override void Serialize(ref JsonWriter writer, ValueTask<T> value, IJsonFormatterResolver formatterResolver)
        {
            // value.Result -> wait...!
            formatterResolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Result, formatterResolver);
        }

        public override ValueTask<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var v = formatterResolver.GetFormatterWithVerify<T>().Deserialize(ref reader, formatterResolver);
            return new ValueTask<T>(v);
        }
    }

#endif
}

namespace Utf8Json.Formatters.Internal
{
    internal static class StandardClassLibraryFormatterHelper
    {
        internal static readonly byte[][] keyValuePairName;
        internal static readonly AutomataDictionary keyValuePairAutomata;

        static StandardClassLibraryFormatterHelper()
        {
            keyValuePairName = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Key"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Value"),
            };
            keyValuePairAutomata = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Key"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Value"), 1 },
            };
        }
    }
}