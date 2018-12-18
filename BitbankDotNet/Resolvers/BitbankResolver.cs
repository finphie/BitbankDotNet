using System;
using System.Diagnostics.CodeAnalysis;
using BitbankDotNet.Entities;
using BitbankDotNet.Formatters;
using SpanJson.Resolvers;

namespace BitbankDotNet.Resolvers
{
    /// <summary>
    /// <see cref="Entities"/>名前空間内にあるクラスのResolver
    /// </summary>
    /// <typeparam name="TSymbol"><see cref="byte"/>または<see cref="char"/></typeparam>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "SpanJson Resolver")]
    sealed class BitbankResolver<TSymbol> : ResolverBase<TSymbol, BitbankResolver<TSymbol>>
        where TSymbol : struct
    {
        public BitbankResolver()
            : base(new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            NamingConvention = NamingConventions.CamelCase,
            EnumOption = EnumOptions.String
        })
        {
            RegisterGlobalCustomFormatter<decimal, DecimalAsStringFormatter>();
            RegisterGlobalCustomFormatter<DateTime, DateTimeAsLongFormatter>();
            RegisterGlobalCustomFormatter<BoardOrder, BitbankBoardOrderFormatter>();
            RegisterGlobalCustomFormatter<Ohlcv, BitbankOhlcvFormatter>();
        }
    }
}