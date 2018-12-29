using System.Text;

namespace BitbankDotNet.InternalShared.Helpers
{
    /// <summary>
    /// <see cref="byte"/>配列のヘルパークラス
    /// </summary>
    public static class ByteArrayHelper
    {
        /// <summary>
        /// UTF-8のbyte配列を作成します。
        /// </summary>
        /// <param name="length">文字列の長さ</param>
        /// <returns>UTF-8のbyte配列を返します。</returns>
        public static byte[] CreateUtf8Bytes(int length)
            => Encoding.UTF8.GetBytes(StringHelper.CreateUtf16String(length));
    }
}