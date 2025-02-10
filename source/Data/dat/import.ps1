## Azure SqlServerのテーブルに
## データファイルからインポートする

#テーブル名
$tables = @(
"abstract_event_link"
,"abstract_album_link"
,"abstract_album"
,"abstract_event"
,"album"
,"live_event"
,"label"
,"media"
,"organization"
,"person"
,"role"
,"roleOnAlbum"
,"roleOnSong"
,"set_list"
,"site"
,"song"
,"track"
,"set_list_note"
)

$serverName = "oohashi-ayaka.database.windows.net"
$databaseName = "OohashiAyakaDatabase"
$username = "sankeihall"
$password = "iinoHall4th0125"

# 各テーブルをエクスポートする
$tables | ForEach-Object {
    $tableName = $_
    $dataFile = "./$tableName.dat"

    # bcpコマンドの構築
    $bcpCommand = "bcp $databaseName.dbo.$tableName in $dataFile -c -C 65001 -S $serverName -U $username -P $password" -E

    # データのインポート
    Write-Output "Importing data from file: $dataFile to table: $tableName"
    Invoke-Expression $bcpCommand
}
