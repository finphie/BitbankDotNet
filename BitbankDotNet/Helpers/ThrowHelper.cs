using SpanJson;
using System.Runtime.CompilerServices;

namespace BitbankDotNet.Helpers
{
    static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowJsonParserException(JsonParserException.ParserError error, int position)
            => throw new JsonParserException(error, position);
    }
}