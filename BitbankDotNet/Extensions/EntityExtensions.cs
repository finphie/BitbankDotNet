using BitbankDotNet.Entities;
using BitbankDotNet.Resolvers;
using SpanJson;

namespace BitbankDotNet.Extensions
{
    public static class EntityExtensions
    {
        public static string ToJsonString<T>(this T entity)
            where T : class, IEntity
            => JsonSerializer.Generic.Utf16.Serialize<T, BitbankResolver<char>>(entity);
    }
}