using SpanJson;
using SpanJson.Formatters;
using SpanJson.Helpers;
using System;
using System.Buffers;
using System.Globalization;

namespace BitbankDotNet.Formatters
{
    sealed class DoubleAsStringArrayFormatter : ICustomJsonFormatter<double[]>
    {
        public static readonly DoubleAsStringArrayFormatter Default = new DoubleAsStringArrayFormatter();
        static readonly DoubleAsStringFormatter ElementFormatter = DoubleAsStringFormatter.Default;

        public double[] Deserialize(ref JsonReader<byte> reader)
        {
            double[] temp = null;
            double[] result;
            try
            {
                temp = ArrayPool<double>.Shared.Rent(4);
                reader.ReadUtf8BeginArrayOrThrow();
                var count = 0;
                while (!reader.TryReadUtf8IsEndArrayOrValueSeparator(ref count))
                {
                    if (count == temp.Length)                    
                        FormatterUtils.GrowArray(ref temp);                  

                    temp[count - 1] = ElementFormatter.Deserialize(ref reader);
                }

                result = count == 0 ? Array.Empty<double>() : FormatterUtils.CopyArray(temp, count);
            }
            finally
            {
                if (temp != null)               
                    ArrayPool<double>.Shared.Return(temp);            
            }

            return result;
        }

        public double[] Deserialize(ref JsonReader<char> reader)
        {
            double[] temp = null;
            double[] result;
            try
            {
                temp = ArrayPool<double>.Shared.Rent(4);
                reader.ReadUtf16BeginArrayOrThrow();
                var count = 0;
                while (!reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count))
                {
                    if (count == temp.Length)
                        FormatterUtils.GrowArray(ref temp);

                    temp[count - 1] = ElementFormatter.Deserialize(ref reader);
                }

                result = count == 0 ? Array.Empty<double>() : FormatterUtils.CopyArray(temp, count);
            }
            finally
            {
                if (temp != null)
                    ArrayPool<double>.Shared.Return(temp);
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, double[] value, int nestingLimit)
            => StringUtf8ArrayFormatter.Default.Serialize(ref writer,
                Array.ConvertAll(value, d => d.ToString(CultureInfo.InvariantCulture)), nestingLimit);

        public void Serialize(ref JsonWriter<char> writer, double[] value, int nestingLimit)
            => StringUtf16ArrayFormatter.Default.Serialize(ref writer,
                Array.ConvertAll(value, d => d.ToString(CultureInfo.InvariantCulture)), nestingLimit);
    }
}