## テーブルデータをエクスポートする

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

#各テーブルをエクスポートする
# データベース接続情報
$serverName = "DESKTOP-9NUP2PU\SQLEXPRESS"
$databaseName = "fanatic_serve"
$authType = "-T"  # Windows認証を使用する場合

# 各テーブルをエクスポートする
$tables | ForEach-Object {
    $tableName = $_
    $outputFile = "./$tableName.dat"
    $bcpCommand = "bcp $databaseName.dbo.$tableName out $outputFile -c -C 65001 -S $serverName $authType"
    
    Write-Output "Exporting table: $tableName to file: $outputFile"
    Invoke-Expression $bcpCommand
}