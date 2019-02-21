using System.Linq;
using Utf8Json.Formatters;
using Utf8Json.Resolvers.Internal;

namespace Utf8Json.Resolvers
{
    public static class StandardResolver
    {
        /// <summary>AllowPrivate:False, ExcludeNull:False, NameMutate:Original</summary>
        public static readonly IJsonFormatterResolver Default = DefaultStandardResolver.Instance;
    }
}

namespace Utf8Json.Resolvers.Internal
{
    internal static class StandardResolverHelper
    {
        internal static readonly IJsonFormatterResolver[] CompositeResolverBase = new[]
        {
            BuiltinResolver.Instance, // Builtin
            EnumResolver.Default,     // Enum(default => string)
            DynamicGenericResolver.Instance, // T[], List<T>, etc...
            AttributeFormatterResolver.Instance // [JsonFormatter]
        };
    }

    internal sealed class DefaultStandardResolver : IJsonFormatterResolver
    {
        // configure
        public static readonly IJsonFormatterResolver Instance = new DefaultStandardResolver();

        DefaultStandardResolver()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                formatter = InnerResolver.Instance.GetFormatter<T>();
            }
        }

        sealed class InnerResolver : IJsonFormatterResolver
        {
            public static readonly IJsonFormatterResolver Instance = new InnerResolver();

            static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.ToArray();

            InnerResolver()
            {
            }

            public IJsonFormatter<T> GetFormatter<T>()
            {
                return FormatterCache<T>.formatter;
            }

            static class FormatterCache<T>
            {
                public static readonly IJsonFormatter<T> formatter;

                static FormatterCache()
                {
                    foreach (var item in resolvers)
                    {
                        var f = item.GetFormatter<T>();
                        if (f != null)
                        {
                            formatter = f;
                            return;
                        }
                    }
                }
            }
        }
    }
}