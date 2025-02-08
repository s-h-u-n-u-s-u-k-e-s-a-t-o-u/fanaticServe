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

abstract_album{
  uuid abstract_album_id pk
  string title
}

album{
  uuid album_id pk
  string code
  string title
  int media_type
  uuid label_id
  date release_on
}

abstract_album_link{
  int id pk
  uuid abstract_album_id
  uuid album_id
}

track{
  uuid tack_id pk
  uuid alubm_id 
  number track_no 
  string title
  int length
  uuid song_id
}

abstract_event{
  uuid abstract_event_id pk
  strting title
  string note
}

event{
  uuid event_id pk
  strting title
  date perform_at
}

abstract_event_link{
  uuid id pk
    uuid abstract_event_id
    uuid event_id
}

set_list{
  uuid set_list_id pk
  string title
  uuid song_id
  string note
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

event ||--|{ set_list:event_id

set_list |o--o| song:song_id


abstract_album ||--|{abstract_album_link:abstract_album_id
album ||--||abstract_album_link:album_id

abstract_event ||--|{abstract_event_link:abstract_event_id
event ||--|| abstract_event_link:event_id

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
