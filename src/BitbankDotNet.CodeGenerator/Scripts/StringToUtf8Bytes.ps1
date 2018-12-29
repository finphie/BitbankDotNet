# 文字列をUTF-8のbyte配列に変換します。

using namespace System.Text

param($text)

Import-Module "./Modules/FormatArray"

$utf8Bytes = [Encoding]::UTF8.GetBytes($text)
$utf8Strings = $utf8Bytes | ForEach-Object { "0x{0:X2}" -f $_ }
Format-Array -Array $utf8Strings