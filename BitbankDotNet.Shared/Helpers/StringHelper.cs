using System;
using System.Text;

namespace BitbankDotNet.Shared.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// UTF-16文字列を作成します。
        /// </summary>
        /// <param name="length">文字列の長さ</param>
        /// <returns>UTF-16文字列を返します。</returns>
        public static string CreateUtf16String(int length)
        {
            // UTF-16文字列作成
            string CreateUtf16String() => Guid.NewGuid().ToString("N");

            var sb = new StringBuilder(64);
            sb.Append(CreateUtf16String());

            var count = length / sb.Length;
            for (var i = 0; i < count; i++)
                sb.Append(CreateUtf16String());

            return sb.ToString().Substring(0, length);
        }
    }
}