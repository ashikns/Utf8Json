using System;
using System.Linq;
using System.Reflection;
using Utf8Json.Internal;

namespace Utf8Json.Resolvers
{
    /// <summary>
    /// Get formatter from [JsonFormatter] attribute.
    /// </summary>
    public sealed class AttributeFormatterResolver : JsonFormatterResolverBase
    {
        public static IJsonFormatterResolver Instance = new AttributeFormatterResolver();

        private AttributeFormatterResolver()
        {

        }

        protected override IJsonFormatter FindFormatter(Type t)
        {
#if (UNITY_METRO || UNITY_WSA) && !NETFX_CORE
            var attr = (JsonFormatterAttribute)t.GetCustomAttributes(typeof(JsonFormatterAttribute), true).FirstOrDefault();
#else
            var attr = t.GetTypeInfo().GetCustomAttribute<JsonFormatterAttribute>();
#endif
            if (attr == null)
            {
                return null;
            }

            try
            {
                if (attr.FormatterType.IsGenericType && !attr.FormatterType.GetTypeInfo().IsConstructedGenericType())
                {
                    var tGeneric = attr.FormatterType.MakeGenericType(t); // use T self
                    return (IJsonFormatter)Activator.CreateInstance(tGeneric, attr.Arguments);
                }
                else
                {
                    return (IJsonFormatter)Activator.CreateInstance(attr.FormatterType, attr.Arguments);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Can not create formatter from JsonFormatterAttribute, check the target formatter is public and has constructor with right argument. FormatterType:" + attr.FormatterType.Name, ex);
            }
        }
    }
}
