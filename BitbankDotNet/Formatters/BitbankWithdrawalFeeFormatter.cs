using BitbankDotNet.Entities;
using BitbankDotNet.Resolvers;
using SpanJson;
using SpanJson.Formatters;

namespace BitbankDotNet.Formatters
{
    /// <summary>
    /// <see cref="Asset.WithdrawalFee"/>クラスのフォーマッター
    /// </summary>
    sealed class BitbankWithdrawalFeeFormatter : ICustomJsonFormatter<Asset.WithdrawalFeeObject>
    {
        public static readonly BitbankWithdrawalFeeFormatter Default = new BitbankWithdrawalFeeFormatter();

        public Asset.WithdrawalFeeObject Deserialize(ref JsonReader<byte> reader)
        {
            // JPYの場合、オブジェクトが返ってくるためチェックする。
            if (reader.ReadUtf8NextToken() == JsonToken.BeginObject)
                return ComplexClassFormatter<Asset.WithdrawalFeeObject, byte, BitbankResolver<byte>>.Default
                    .Deserialize(ref reader);

            // JPY以外の場合
            var value = DoubleAsStringFormatter.Default.Deserialize(ref reader);
            return new Asset.WithdrawalFeeObject {Threshold = 0, Under = value, Over = value};
        }

        public Asset.WithdrawalFeeObject Deserialize(ref JsonReader<char> reader)
        {
            // JPYの場合、オブジェクトが返ってくるためチェックする。
            if (reader.ReadUtf16NextToken() == JsonToken.BeginObject)
                return ComplexClassFormatter<Asset.WithdrawalFeeObject, char, BitbankResolver<char>>.Default
                    .Deserialize(ref reader);

            // JPY以外の場合
            var value = DoubleAsStringFormatter.Default.Deserialize(ref reader);
            return new Asset.WithdrawalFeeObject {Threshold = 0, Under = value, Over = value};
        }

        public void Serialize(ref JsonWriter<byte> writer, Asset.WithdrawalFeeObject value, int nestingLimit)
        {
            // JPYの場合は0ではないはず。
            // また、小数点以下は無視できる。
            if ((int)value.Threshold != 0)
                ComplexClassFormatter<Asset.WithdrawalFeeObject, byte, BitbankResolver<byte>>.Default
                    .Serialize(ref writer, value, nestingLimit);
            else
                DoubleAsStringFormatter.Default.Serialize(ref writer, value.Under, nestingLimit);
        }

        public void Serialize(ref JsonWriter<char> writer, Asset.WithdrawalFeeObject value, int nestingLimit)
        {
            // JPYの場合は0ではないはず。
            // また、小数点以下は無視できる。
            if ((int)value.Threshold != 0)
                ComplexClassFormatter<Asset.WithdrawalFeeObject, char, BitbankResolver<char>>.Default
                    .Serialize(ref writer, value, nestingLimit);
            else
                DoubleAsStringFormatter.Default.Serialize(ref writer, value.Under, nestingLimit);
        }
    }
}