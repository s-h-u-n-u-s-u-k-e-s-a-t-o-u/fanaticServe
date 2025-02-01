function sub() {

    $dir = "\sql"
    $fileType = "*.sql"

    # カレントディレクトリを取得
    $curretnDir = Get-Location

    # 指定のディレクトリであるか
    if ($curretnDir.Path.EndsWith($dir)) {
        $SqlDir = $curretnDir.Path
    }
    else {
        $SqlDir = Join-Path -Path $curretnDir.Path -ChildPath $dir
    }

    # ディレクトリが存在しない場合はエラー 
    if (!(Test-Path -PathType Container $SqlDir)) {
        throw  "ディレクトリが存在しない:$SqlDir"
    }

    # SQLファイルが存在するか
    if (!(Test-Path -PathType Leaf "$SqlDir\$fileType")) {
        throw  'SQLファイルが存在しない'
    }

    # SQLファイルを取得
    $SqlFiles = Get-ChildItem -Path $SqlDir -Filter $fileType

    # SQLファイルを実行

    $DBServer = 'localhost\SQLEXPRESS'
    $DabaseName = 'Fanatic_Serve'
    foreach ($SqlFile in $SqlFiles) {
        Write-Host " SQLファイルを実行: $($SqlFile.FullName) "

        # -E windwos認証
        sqlcmd -S $DBServer -E -d $DabaseName -i $SqlFile.FullName -f 65001
    }
}

function main() {
    try {
        $OutputEncoding.EncodingName
        sub  
    }
    catch {
        Write-Error $_.Exception.Message
        exit 1
    }
}

# 実行
main
