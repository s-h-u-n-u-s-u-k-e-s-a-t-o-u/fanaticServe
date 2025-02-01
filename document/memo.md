
# markdownで書く

## サイトの目的

アーティスト活動を出来るだけ時系列で表示する。
まずはリリース物から。

サイトを生成する元データをDBで管理する。

## サイトの想定ユーザー

データのオタク

## 想定環境

- MS SQLServer
- ASP.net core
- Entity framework
- Azure ???

## 管理する項目

- メディア
  - media
  - CD、配信、nemo albumなど
- 概念アルバム
  - abustractAlbum
  - ○○盤の違いを考慮しないで表現する
- アルバム
  - album
  - 具象アルバム
  - ○○盤の違いを表現
  - 収録されている曲、順番
- トラック
  - track
  - 曲順 track number
  - 曲名 title
  - 長さ length 秒数
- 曲
  - song
  - 概念として
  - インスト、アレンジ違いなどを集約する
- 役割
  - roll
  - 曲やアルバム中での役割のマスタ
- 映像
  - Blu-ray
  - DVD
- サイト
  - 公式、snsなどサイトがあればなんにでも紐づけていく

## asp.net coreでDatrabase first

- In ASP.NET Core MVC (.NET 6) can we use DataBase first approach
<https://learn.microsoft.com/en-us/answers/questions/1103914/in-asp-net-core-mvc-(-net-6)-can-we-use-database-f>

- Asp.net core 構成
<https://learn.microsoft.com/ja-jp/aspnet/core/fundamentals/configuration/?view=aspnetcore-9.0>

visual studioのpckage manager consoleで実行する

``` MPC
Scaffold-DbContext -NoOnConfiguring 'Data Source=DESKTOP-9NUP2PU\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Database=fanatic_serve;' Microsoft.EntityFrameworkCore.SqlServer -UseDatabaseNames -DataAnnotations  -OutputDir Models -Force
```

環境変数構成プロバイダーを使用する、プレフィックス ASPNETCORE_ が付いた環境変数。

- 環境変数に変数 SSSWare_DB_CONNECTION、値 xxxx　を設定する
- <project_root>/Program.cs ファイル に以下の記載をする

builder.Configuration.AddEnvironmentVariables(prefix: "SSSWare_");
(この記述の前であること →  var app = builder.Build();)

# Sqlserver データローダー bcp

## ファイルエクスポートする場合

codepage指定していないのでs-jisで出力

``` command line
bcp dbo.album out "./album.dat" -c -S DESKTOP-9NUP2PU\SQLEXPRESS -d fanatic_serve -T
```

codepage指定(65001)してUTF-8で出力

``` command line
bcp dbo.abstract_album out "./abstract_album_2.dat" -c -C 65001 -S DESKTOP-9NUP2PU\SQLEXPRESS -d fanatic_serve -T
```

## ファイルインポートする場合

``` command line
bcp dbo.abstract_album in ".\abstract_album.dat" -c -t "\t" -C 65001 -S DESKTOP-9NUP2PU\SQLEXPRESS -d fanatic_serve -T

bcp dbo.album in ".\album.dat" -c -t "\t" -C 65001 -S DESKTOP-9NUP2PU\SQLEXPRESS -d fanatic_serve -T
```

datファイルはtab区切りテキスト
bcp dbo.media in ".\media.dat" -c -t "\t" -C 65001 -S DESKTOP-9NUP2PU\SQLEXPRESS -d fanatic_serve -T
