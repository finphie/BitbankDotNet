# 文字列配列を指定の長さ毎にカンマ区切りで連結します。

function Format-Array {
    param (
        [Parameter(Mandatory)]
        [ValidateCount(1, [int]::MaxValue)]
        [string[]] $Array,
        [ValidateCount(1, [int]::MaxValue)]
        [int] $Count = 10
    )

    $i = 0
    do {
        $array10 = $Array[$i..($i+$Count-1)]
        $result = $array10 -join ", "
        if ($array10.Length -eq $Count) {
            $result + ","
            $i += $Count
        } else {
            $result
            break
        }
    } while ($true)
}

Export-ModuleMember -Function Format-Array