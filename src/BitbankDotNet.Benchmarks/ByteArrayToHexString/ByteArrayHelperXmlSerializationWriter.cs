using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace BitbankDotNet.Benchmarks.ByteArrayToHexString
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "XmlSerializationWriter.FromByteArrayHexを利用")]
    sealed class ByteArrayHelperXmlSerializationWriter : XmlSerializationWriter
    {
        public static string ToHexString(byte[] value) => FromByteArrayHex(value);

        protected override void InitCallbacks() => throw new NotSupportedException();
    }
}