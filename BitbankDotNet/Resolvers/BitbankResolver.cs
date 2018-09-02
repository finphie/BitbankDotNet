﻿using BitbankDotNet.Formatters;
using SpanJson.Resolvers;
using System;

namespace BitbankDotNet.Resolvers
{
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
            RegisterGlobalCustomFormatter<DateTime, DateTimeAsLongFormatter>();
        }
    }
}