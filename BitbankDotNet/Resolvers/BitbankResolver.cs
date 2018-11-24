using BitbankDotNet.Entities;
using BitbankDotNet.Formatters;
using SpanJson.Resolvers;
using System;

namespace BitbankDotNet.Resolvers
{
    /// <summary>
    /// <see cref="Entities"/>名前空間内にあるクラスのResolver
    /// </summary>
    /// <typeparam name="TSymbol"><see cref="byte"/>または<see cref="char"/></typeparam>
    sealed class BitbankResolver<TSymbol> : ResolverBase<TSymbol, BitbankResolver<TSymbol>>
        where TSymbol : struct
    {
        public BitbankResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            NamingConvention = NamingConventions.CamelCase,
            EnumOption = EnumOptions.String
        })
        {
            RegisterGlobalCustomFormatter<double, DoubleAsStringFormatter>();
            RegisterGlobalCustomFormatter<DateTime, DateTimeAsLongFormatter>();
            RegisterGlobalCustomFormatter<BoardOrder, BitbankBoardOrderFormatter>();
            RegisterGlobalCustomFormatter<Ohlcv, BitbankOhlcvFormatter>();
        }
    }
}