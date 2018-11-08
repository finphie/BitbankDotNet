# 文字列をUTF-8のbyte配列に変換します。

using namespace System.Text

param($text)

$utf8Bytes = [Encoding]::UTF8.GetBytes($text)
$utf8Strings = $utf8Bytes | ForEach-Object { "0x{0:x2}" -f $_ }

$utf8Strings -join ", "
