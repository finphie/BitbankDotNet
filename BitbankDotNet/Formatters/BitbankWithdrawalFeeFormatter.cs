﻿using BitbankDotNet.Entities;
using BitbankDotNet.Resolvers;
using SpanJson;
using SpanJson.Formatters;

namespace BitbankDotNet.Formatters
{
#pragma warning disable SA1200 // Using directives should be placed correctly
    using WithdrawalFeeUtf16Formatter = ComplexClassFormatter<WithdrawalFee, char, BitbankResolver<char>>;
    using WithdrawalFeeUtf8Formatter = ComplexClassFormatter<WithdrawalFee, byte, BitbankResolver<byte>>;
#pragma warning restore SA1200 // Using directives should be placed correctly

    /// <summary>
    /// <see cref="Asset.WithdrawalFee"/>クラスのフォーマッター
    /// </summary>
    sealed class BitbankWithdrawalFeeFormatter : ICustomJsonFormatter<WithdrawalFee>
    {
        public static readonly BitbankWithdrawalFeeFormatter Default = new BitbankWithdrawalFeeFormatter();
        static readonly DoubleAsStringFormatter ElementDoubleAsStringFormatter = DoubleAsStringFormatter.Default;
        static readonly WithdrawalFeeUtf16Formatter ElementComplexClassUtf16Formatter = WithdrawalFeeUtf16Formatter.Default;
        static readonly WithdrawalFeeUtf8Formatter ElementComplexClassUtf8Formatter = WithdrawalFeeUtf8Formatter.Default;

        public WithdrawalFee Deserialize(ref JsonReader<byte> reader)
        {
            // JPYの場合、オブジェクトが返ってくるためチェックする。
            if (reader.ReadUtf8NextToken() == JsonToken.BeginObject)
                return ElementComplexClassUtf8Formatter.Deserialize(ref reader);

            // JPY以外の場合
            var value = ElementDoubleAsStringFormatter.Deserialize(ref reader);
            return new WithdrawalFee { Threshold = 0, Under = value, Over = value };
        }

        public WithdrawalFee Deserialize(ref JsonReader<char> reader)
        {
            // JPYの場合、オブジェクトが返ってくるためチェックする。
            if (reader.ReadUtf16NextToken() == JsonToken.BeginObject)
                return ElementComplexClassUtf16Formatter.Deserialize(ref reader);

            // JPY以外の場合
            var value = ElementDoubleAsStringFormatter.Deserialize(ref reader);
            return new WithdrawalFee { Threshold = 0, Under = value, Over = value };
        }

        public void Serialize(ref JsonWriter<byte> writer, WithdrawalFee value, int nestingLimit)
        {
            // JPYの場合は0ではないはず。
            // また、小数点以下は無視できる。
            if ((int)value.Threshold != 0)
                ElementComplexClassUtf8Formatter.Serialize(ref writer, value, nestingLimit);
            else
                ElementDoubleAsStringFormatter.Serialize(ref writer, value.Under, nestingLimit);
        }

        public void Serialize(ref JsonWriter<char> writer, WithdrawalFee value, int nestingLimit)
        {
            // JPYの場合は0ではないはず。
            // また、小数点以下は無視できる。
            if ((int)value.Threshold != 0)
                ElementComplexClassUtf16Formatter.Serialize(ref writer, value, nestingLimit);
            else
                ElementDoubleAsStringFormatter.Serialize(ref writer, value.Under, nestingLimit);
        }
    }
}