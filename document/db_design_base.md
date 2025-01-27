# DB設計のメモ

- 日付(date)の項目は末尾に _on
- 日時(datetime)の項目は末尾に _at
- 登録日時 created_at datetime
- 更新日時 modified_at datetime
- 名前 name verchar(256)
- カナ kana verchar(256) カタカナのみ

## 雑記

- データ登録
SQLServer ユニークIDの発番

<https://learn.microsoft.com/ja-jp/sql/t-sql/functions/newid-transact-sql?view=sql-server-ver16>

NEWID() 関数

## ダイアグラム

```mermaid

erDiagram

id{
uuid id pk
datetime created_at
}

song{
uuid song_id pk
string title
string kana
datetime created_at
datetime modified_at
}

person{
uuid person_id pk
string name
string kana
datetime created_at
datetime modified_at
}

organization{
uuid organization_id pk
string name
string kana
datetime created_at
datetime modified_at
}

label{
  uuid label_id pk
  uuid organization_id
  string name
}

site{
uuid site_id pk
int sequence pk
string displayName
string url
datetime created_at
datetime modified_at
}

media{
    int media_type pk
    string name
}

abustract_album{
  uuid abustract_album_id pk
  string title
}

album{
  uuid album_id pk
  uuid abustract_album_id
  string title
  int media_type
  uuid label_id
  date release_on
}

abustract_album ||--|{ album:abustract_album_id

track{
  uuid tack_id pk
  uuid alubm_id 
  number track_no 
  string title
  int length
  uuid song_id
}

role {
  int id pk
  string name
  datetime created_at
  datetime modified_at
}

roleOnSong{
  uuid song_id
  uuid role_id
  uuid person_id
}

roleOnAlbum{
  uuid album_id
  uuid roll_id
  uuid person_id
}


id ||--|| song:id
id ||--|| organization:id
id ||--|| person:id
id ||--|| label:id
id ||--o{ site:id
media ||--|{album:media_id
album ||--|{ track:album_id
song ||--|{ track:song_id 
song ||--|{ roleOnSong:song_id
album ||--|{ roleOnAlbum:album_id
person ||--|{roleOnSong:person_id
person ||--|{roleOnAlbum:person_id
role ||--|{ roleOnSong:role_id
role ||--|{ roleOnAlbum:role_id
organization ||--|{ label:organization_id
label ||--|{album:label_id
```
