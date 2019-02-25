using System;
using Utf8Json.Internal;

namespace Utf8Json.Formatters
{
    public sealed class SByteFormatter : JsonFormatterBase<SByte>, IObjectPropertyNameFormatter<SByte>
    {
        public static readonly SByteFormatter Default = new SByteFormatter();

        public override void Serialize(ref JsonWriter writer, SByte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteSByte(value);
        }

        public override SByte Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadSByte();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, SByte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteSByte(value);
            writer.WriteQuotation();
        }

        public SByte DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadSByte(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableSByteFormatter : JsonFormatterBase<SByte?>, IObjectPropertyNameFormatter<SByte?>
    {
        public static readonly NullableSByteFormatter Default = new NullableSByteFormatter();

        public override void Serialize(ref JsonWriter writer, SByte? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteSByte(value.Value);
            }
        }

        public override SByte? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadSByte();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, SByte? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteSByte(value.Value);
            writer.WriteQuotation();
        }

        public SByte? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadSByte(key.Array, key.Offset, out _);
        }
    }

    public sealed class SByteArrayFormatter : JsonFormatterBase<SByte[]>
    {
        public static readonly SByteArrayFormatter Default = new SByteArrayFormatter();

        public override void Serialize(ref JsonWriter writer, SByte[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteSByte(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteSByte(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override SByte[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new SByte[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadSByte();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class Int16Formatter : JsonFormatterBase<Int16>, IObjectPropertyNameFormatter<Int16>
    {
        public static readonly Int16Formatter Default = new Int16Formatter();

        public override void Serialize(ref JsonWriter writer, Int16 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt16(value);
        }

        public override Int16 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt16();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Int16 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteInt16(value);
            writer.WriteQuotation();
        }

        public Int16 DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadInt16(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableInt16Formatter : JsonFormatterBase<Int16?>, IObjectPropertyNameFormatter<Int16?>
    {
        public static readonly NullableInt16Formatter Default = new NullableInt16Formatter();

        public override void Serialize(ref JsonWriter writer, Int16? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteInt16(value.Value);
            }
        }

        public override Int16? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadInt16();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Int16? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteInt16(value.Value);
            writer.WriteQuotation();
        }

        public Int16? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadInt16(key.Array, key.Offset, out _);
        }
    }

    public sealed class Int16ArrayFormatter : JsonFormatterBase<Int16[]>
    {
        public static readonly Int16ArrayFormatter Default = new Int16ArrayFormatter();

        public override void Serialize(ref JsonWriter writer, Int16[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteInt16(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteInt16(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override Int16[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new Int16[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadInt16();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class Int32Formatter : JsonFormatterBase<Int32>, IObjectPropertyNameFormatter<Int32>
    {
        public static readonly Int32Formatter Default = new Int32Formatter();

        public override void Serialize(ref JsonWriter writer, Int32 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt32(value);
        }

        public override Int32 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt32();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Int32 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteInt32(value);
            writer.WriteQuotation();
        }

        public Int32 DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadInt32(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableInt32Formatter : JsonFormatterBase<Int32?>, IObjectPropertyNameFormatter<Int32?>
    {
        public static readonly NullableInt32Formatter Default = new NullableInt32Formatter();

        public override void Serialize(ref JsonWriter writer, Int32? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteInt32(value.Value);
            }
        }

        public override Int32? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadInt32();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Int32? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteInt32(value.Value);
            writer.WriteQuotation();
        }

        public Int32? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadInt32(key.Array, key.Offset, out _);
        }
    }

    public sealed class Int32ArrayFormatter : JsonFormatterBase<Int32[]>
    {
        public static readonly Int32ArrayFormatter Default = new Int32ArrayFormatter();

        public override void Serialize(ref JsonWriter writer, Int32[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteInt32(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteInt32(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override Int32[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new Int32[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadInt32();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class Int64Formatter : JsonFormatterBase<Int64>, IObjectPropertyNameFormatter<Int64>
    {
        public static readonly Int64Formatter Default = new Int64Formatter();

        public override void Serialize(ref JsonWriter writer, Int64 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt64(value);
        }

        public override Int64 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt64();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Int64 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteInt64(value);
            writer.WriteQuotation();
        }

        public Int64 DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadInt64(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableInt64Formatter : JsonFormatterBase<Int64?>, IObjectPropertyNameFormatter<Int64?>
    {
        public static readonly NullableInt64Formatter Default = new NullableInt64Formatter();

        public override void Serialize(ref JsonWriter writer, Int64? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteInt64(value.Value);
            }
        }

        public override Int64? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadInt64();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Int64? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteInt64(value.Value);
            writer.WriteQuotation();
        }

        public Int64? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadInt64(key.Array, key.Offset, out _);
        }
    }

    public sealed class Int64ArrayFormatter : JsonFormatterBase<Int64[]>
    {
        public static readonly Int64ArrayFormatter Default = new Int64ArrayFormatter();

        public override void Serialize(ref JsonWriter writer, Int64[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteInt64(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteInt64(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override Int64[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new Int64[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadInt64();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class ByteFormatter : JsonFormatterBase<Byte>, IObjectPropertyNameFormatter<Byte>
    {
        public static readonly ByteFormatter Default = new ByteFormatter();

        public override void Serialize(ref JsonWriter writer, Byte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteByte(value);
        }

        public override Byte Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadByte();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Byte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteByte(value);
            writer.WriteQuotation();
        }

        public Byte DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadByte(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableByteFormatter : JsonFormatterBase<Byte?>, IObjectPropertyNameFormatter<Byte?>
    {
        public static readonly NullableByteFormatter Default = new NullableByteFormatter();

        public override void Serialize(ref JsonWriter writer, Byte? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteByte(value.Value);
            }
        }

        public override Byte? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadByte();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Byte? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteByte(value.Value);
            writer.WriteQuotation();
        }

        public Byte? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadByte(key.Array, key.Offset, out _);
        }
    }


    public sealed class UInt16Formatter : JsonFormatterBase<UInt16>, IObjectPropertyNameFormatter<UInt16>
    {
        public static readonly UInt16Formatter Default = new UInt16Formatter();

        public override void Serialize(ref JsonWriter writer, UInt16 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt16(value);
        }

        public override UInt16 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt16();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, UInt16 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteUInt16(value);
            writer.WriteQuotation();
        }

        public UInt16 DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadUInt16(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableUInt16Formatter : JsonFormatterBase<UInt16?>, IObjectPropertyNameFormatter<UInt16?>
    {
        public static readonly NullableUInt16Formatter Default = new NullableUInt16Formatter();

        public override void Serialize(ref JsonWriter writer, UInt16? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteUInt16(value.Value);
            }
        }

        public override UInt16? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadUInt16();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, UInt16? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteUInt16(value.Value);
            writer.WriteQuotation();
        }

        public UInt16? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadUInt16(key.Array, key.Offset, out _);
        }
    }

    public sealed class UInt16ArrayFormatter : JsonFormatterBase<UInt16[]>
    {
        public static readonly UInt16ArrayFormatter Default = new UInt16ArrayFormatter();

        public override void Serialize(ref JsonWriter writer, UInt16[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteUInt16(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteUInt16(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override UInt16[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new UInt16[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadUInt16();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class UInt32Formatter : JsonFormatterBase<UInt32>, IObjectPropertyNameFormatter<UInt32>
    {
        public static readonly UInt32Formatter Default = new UInt32Formatter();

        public override void Serialize(ref JsonWriter writer, UInt32 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt32(value);
        }

        public override UInt32 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt32();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, UInt32 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteUInt32(value);
            writer.WriteQuotation();
        }

        public UInt32 DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadUInt32(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableUInt32Formatter : JsonFormatterBase<UInt32?>, IObjectPropertyNameFormatter<UInt32?>
    {
        public static readonly NullableUInt32Formatter Default = new NullableUInt32Formatter();

        public override void Serialize(ref JsonWriter writer, UInt32? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteUInt32(value.Value);
            }
        }

        public override UInt32? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadUInt32();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, UInt32? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteUInt32(value.Value);
            writer.WriteQuotation();
        }

        public UInt32? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadUInt32(key.Array, key.Offset, out _);
        }
    }

    public sealed class UInt32ArrayFormatter : JsonFormatterBase<UInt32[]>
    {
        public static readonly UInt32ArrayFormatter Default = new UInt32ArrayFormatter();

        public override void Serialize(ref JsonWriter writer, UInt32[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteUInt32(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteUInt32(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override UInt32[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new UInt32[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadUInt32();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class UInt64Formatter : JsonFormatterBase<UInt64>, IObjectPropertyNameFormatter<UInt64>
    {
        public static readonly UInt64Formatter Default = new UInt64Formatter();

        public override void Serialize(ref JsonWriter writer, UInt64 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt64(value);
        }

        public override UInt64 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt64();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, UInt64 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteUInt64(value);
            writer.WriteQuotation();
        }

        public UInt64 DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadUInt64(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableUInt64Formatter : JsonFormatterBase<UInt64?>, IObjectPropertyNameFormatter<UInt64?>
    {
        public static readonly NullableUInt64Formatter Default = new NullableUInt64Formatter();

        public override void Serialize(ref JsonWriter writer, UInt64? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteUInt64(value.Value);
            }
        }

        public override UInt64? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadUInt64();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, UInt64? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteUInt64(value.Value);
            writer.WriteQuotation();
        }

        public UInt64? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadUInt64(key.Array, key.Offset, out _);
        }
    }

    public sealed class UInt64ArrayFormatter : JsonFormatterBase<UInt64[]>
    {
        public static readonly UInt64ArrayFormatter Default = new UInt64ArrayFormatter();

        public override void Serialize(ref JsonWriter writer, UInt64[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteUInt64(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteUInt64(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override UInt64[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new UInt64[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadUInt64();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class SingleFormatter : JsonFormatterBase<Single>, IObjectPropertyNameFormatter<Single>
    {
        public static readonly SingleFormatter Default = new SingleFormatter();

        public override void Serialize(ref JsonWriter writer, Single value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteSingle(value);
        }

        public override Single Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadSingle();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Single value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteSingle(value);
            writer.WriteQuotation();
        }

        public Single DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadSingle(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableSingleFormatter : JsonFormatterBase<Single?>, IObjectPropertyNameFormatter<Single?>
    {
        public static readonly NullableSingleFormatter Default = new NullableSingleFormatter();

        public override void Serialize(ref JsonWriter writer, Single? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteSingle(value.Value);
            }
        }

        public override Single? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadSingle();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Single? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteSingle(value.Value);
            writer.WriteQuotation();
        }

        public Single? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadSingle(key.Array, key.Offset, out _);
        }
    }

    public sealed class SingleArrayFormatter : JsonFormatterBase<Single[]>
    {
        public static readonly SingleArrayFormatter Default = new SingleArrayFormatter();

        public override void Serialize(ref JsonWriter writer, Single[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteSingle(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteSingle(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override Single[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new Single[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadSingle();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class DoubleFormatter : JsonFormatterBase<Double>, IObjectPropertyNameFormatter<Double>
    {
        public static readonly DoubleFormatter Default = new DoubleFormatter();

        public override void Serialize(ref JsonWriter writer, Double value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDouble(value);
        }

        public override Double Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDouble();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Double value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteDouble(value);
            writer.WriteQuotation();
        }

        public Double DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadDouble(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableDoubleFormatter : JsonFormatterBase<Double?>, IObjectPropertyNameFormatter<Double?>
    {
        public static readonly NullableDoubleFormatter Default = new NullableDoubleFormatter();

        public override void Serialize(ref JsonWriter writer, Double? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteDouble(value.Value);
            }
        }

        public override Double? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadDouble();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Double? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteDouble(value.Value);
            writer.WriteQuotation();
        }

        public Double? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadDouble(key.Array, key.Offset, out _);
        }
    }

    public sealed class DoubleArrayFormatter : JsonFormatterBase<Double[]>
    {
        public static readonly DoubleArrayFormatter Default = new DoubleArrayFormatter();

        public override void Serialize(ref JsonWriter writer, Double[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteDouble(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteDouble(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override Double[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new Double[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadDouble();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

    public sealed class BooleanFormatter : JsonFormatterBase<Boolean>, IObjectPropertyNameFormatter<Boolean>
    {
        public static readonly BooleanFormatter Default = new BooleanFormatter();

        public override void Serialize(ref JsonWriter writer, Boolean value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteBoolean(value);
        }

        public override Boolean Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadBoolean();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Boolean value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteBoolean(value);
            writer.WriteQuotation();
        }

        public Boolean DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadBoolean(key.Array, key.Offset, out _);
        }
    }

    public sealed class NullableBooleanFormatter : JsonFormatterBase<Boolean?>, IObjectPropertyNameFormatter<Boolean?>
    {
        public static readonly NullableBooleanFormatter Default = new NullableBooleanFormatter();

        public override void Serialize(ref JsonWriter writer, Boolean? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteBoolean(value.Value);
            }
        }

        public override Boolean? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                return reader.ReadBoolean();
            }
        }

        public void SerializeToPropertyName(ref JsonWriter writer, Boolean? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteBoolean(value.Value);
            writer.WriteQuotation();
        }

        public Boolean? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            int _;
            return NumberConverter.ReadBoolean(key.Array, key.Offset, out _);
        }
    }

    public sealed class BooleanArrayFormatter : JsonFormatterBase<Boolean[]>
    {
        public static readonly BooleanArrayFormatter Default = new BooleanArrayFormatter();

        public override void Serialize(ref JsonWriter writer, Boolean[] value, IJsonFormatterResolver formatterResolver)
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
                    writer.WriteBoolean(value[0]);
                }
                for (int i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteBoolean(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public override Boolean[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                reader.ReadIsBeginArrayWithVerify();
                var array = new Boolean[4];
                var count = 0;
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize(ref array, count * 2);
                    }
                    array[count - 1] = reader.ReadBoolean();
                }

                Array.Resize(ref array, count);
                return array;
            }
        }
    }

}