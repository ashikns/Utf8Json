using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Utf8Json
{
    public interface IJsonFormatterResolver
    {
        IJsonFormatter<T> GetFormatter<T>();
        IJsonFormatter GetFormatter(Type t);
    }

    public static class JsonFormatterResolverExtensions
    {
        public static IJsonFormatter<T> GetFormatterWithVerify<T>(this IJsonFormatterResolver resolver)
        {
            IJsonFormatter<T> formatter;
            try
            {
                formatter = resolver.GetFormatter<T>();
            }
            catch (TypeInitializationException ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                {
                    inner = inner.InnerException;
                }

                throw inner;
            }

            if (formatter == null)
            {
                throw new FormatterNotRegisteredException(typeof(T).FullName + " is not registered in this resolver. resolver:" + resolver.GetType().Name);
            }

            return formatter;
        }

        public static object GetFormatterDynamic(this IJsonFormatterResolver resolver, Type type)
        {
            var methodInfo = typeof(IJsonFormatterResolver).GetRuntimeMethod("GetFormatter", Type.EmptyTypes);

            var formatter = methodInfo.MakeGenericMethod(type).Invoke(resolver, null);
            return formatter;
        }
    }

    public class FormatterNotRegisteredException : Exception
    {
        public FormatterNotRegisteredException(string message) : base(message)
        {
        }
    }

    public abstract class JsonFormatterResolverBase : IJsonFormatterResolver
    {
        protected ConcurrentDictionary<Type, IJsonFormatter> FormatterCache { get; private set; }

        protected JsonFormatterResolverBase()
        {
            FormatterCache = new ConcurrentDictionary<Type, IJsonFormatter>();
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return (IJsonFormatter<T>)GetFormatter(typeof(T));
        }

        public IJsonFormatter GetFormatter(Type t)
        {
            if (!FormatterCache.TryGetValue(t, out var formatter))
            {
                formatter = FindFormatter(t);
                FormatterCache.TryAdd(t, formatter);
            }

            return formatter;
        }

        protected abstract IJsonFormatter FindFormatter(Type t);
    }
}