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
        $array10 -join ", "
        $i += $Count
    } while ($array10.Length -eq $Count)
}

Export-ModuleMember -Function Format-Array