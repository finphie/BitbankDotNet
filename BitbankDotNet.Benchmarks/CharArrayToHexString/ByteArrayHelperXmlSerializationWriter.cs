﻿using System;
using System.Xml.Serialization;

namespace BitbankDotNet.Benchmarks.CharArrayToHexString
{
    sealed class ByteArrayHelperXmlSerializationWriter : XmlSerializationWriter
    {
        public static string ToHexString(byte[] value) => FromByteArrayHex(value);
        protected override void InitCallbacks() => throw new NotSupportedException();
    }
}