# byte配列を16進数のstringに変換する際に利用するテーブルを生成します。

Import-Module "./Modules/FormatArray"

$table = 0..255 | ForEach-Object { $s = $_.ToString("x2"); "0x" + ([int]$s[0] + ([int]$s[1] -shl 16)).ToString("X") }
Format-Array -Array $table