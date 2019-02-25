using System;
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
            DynamicGenericResolver.Instance, // T[], List<T>, etc...
            AttributeFormatterResolver.Instance // [JsonFormatter]
        };
    }

    internal sealed class DefaultStandardResolver : JsonFormatterResolverBase
    {
        // configure
        public static readonly IJsonFormatterResolver Instance = new DefaultStandardResolver();

        private DefaultStandardResolver()
        {
        }

        protected override IJsonFormatter FindFormatter(Type t)
        {
            return InnerResolver.Instance.GetFormatter(t);
        }

        sealed class InnerResolver : JsonFormatterResolverBase
        {
            public static readonly IJsonFormatterResolver Instance = new InnerResolver();

            static readonly IJsonFormatterResolver[] resolvers = StandardResolverHelper.CompositeResolverBase.ToArray();

            private InnerResolver()
            {
            }

            protected override IJsonFormatter FindFormatter(Type t)
            {
                return resolvers.Select(item => item.GetFormatter(t)).FirstOrDefault(f => f != null);
            }
        }
    }
}