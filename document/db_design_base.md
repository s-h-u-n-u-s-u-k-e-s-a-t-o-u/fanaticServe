# DB設計のメモ

- 日付(date)の項目は末尾に _on
- 日時(datetime)の項目は末尾に _at
- 登録日時 created_at datetime
- 更新日時 modified_at datetime
- 名前 name nverchar(256)
- カナ kana nverchar(256) カタカナのみ
- 備考 note nvarchar(max)

<https://learn.microsoft.com/ja-jp/sql/t-sql/functions/newid-transact-sql?view=sql-server-ver16>

NEWID() 関数

## ERダイアグラム

```mermaid

erDiagram

song{
  uniqueidentifier song_id pk
  nvarchar title
  nvarchar kana
  datetime created_at
  datetime modified_at
}

song_note{
  uniqueidentifier song_id pk
  nvarchar note
  datetime created_at
  datetime modified_at
}

organization{
  uniqueidentifier organization_id pk
  nvarchar name
  nvarchar kana
  datetime created_at
  datetime modified_at
}

person{
  uniqueidentifier person_id pk
  nvarchar name
  nvarchar kana
  datetime created_at
  datetime modified_at
}

label{
  uniqueidentifier label_id pk
  uniqueidentifier organization_id
  nvarchar name
  datetime created_at
  datetime modified_at
}

site{
  uniqueidentifier site_id pk
  int sequence
  nvarchar display_name
  nvarchar url
  datetime created_at
  datetime modified_at
}

media{
  int media_type pk
  nvarchar name
  datetime created_at
  datetime modified_at
}

abstract_album{
  uniqueidentifier abstract_album_id pk
  nvarchar title
  datetime created_at
  datetime modified_at
}

abstract_album_note{
  uniqueidentifier abstract_album_id
  nvarchar note
  datetime created_at
  datetime modified_at
}

album{
  uniqueidentifier album_id pk
  nvarchar code
  nvarchar title
  int media_type
  date release_on
  uniqueidentifier label_id
  datetime created_at
  datetime modified_at
}

album_note{
  uniqueidentifier album_id pk
  nvarchar note
  datetime created_at
  datetime modified_at
}

abstract_album_link{
  int_IDENTITY id pk
  uniqueidentifier abstract_album_id
  uniqueidentifier album_id
  datetime created_at
  datetime modified_at
}

track{
  uniqueidentifier tack_id pk
  uniqueidentifier alubm_id
  number track_no
  nvarchar title
  int length
  uniqueidentifier song_id
  datetime created_at
  datetime modified_at
}

track_note{
  uniqueidentifier tack_id pk
  nvarchar note
  datetime created_at
  datetime modified_at  
}

abstract_event{
  uniqueidentifier abstract_event_id pk
  nvarchar title
  nvarchar note
  datetime created_at
  datetime modified_at
}

abstract_event_note{
  uniqueidentifier abstract_event_id
  nvarchar note
  datetime created_at
  datetime modified_at
}

live_event{
  uniqueidentifier live_event_id pk
  nvarchar title
  nvarchar place
  datetime perform_at
  datetime created_at
  datetime modified_at
}

live_event_note{
  uniqueidentifier live_event_id pk
  nvarchar note
  datetime created_at
  datetime modified_at
}

abstract_event_link{
  int_IDENTITY id pk
  uniqueidentifier abstract_event_id
  uniqueidentifier event_id
  datetime created_at
  datetime modified_at
}

set_list{
  uniqueidentifier set_list_id pk
  uniqueidentifier live_event_id
  int set_list_no
  nvarchar title
  uniqueidentifier song_id
  datetime created_at
  datetime modified_at  
}

set_list_note{
  uniqueidentifier set_list_id pk
  nvarchar note
  datetime created_at
  datetime modified_at  
}

role {
  int_IDENTITY id pk
  nvarchar name
  datetime created_at
  datetime modified_at
}

roleOnSong{
  int_IDENTITY id
  uniqueidentifier song_id
  uniqueidentifier role_id
  uniqueidentifier person_id
  datetime created_at
  datetime modified_at
}

roleOnAlbum{
  int_IDENTITY id
  uniqueidentifier album_id
  uniqueidentifier roll_id
  uniqueidentifier person_id
  datetime created_at
  datetime modified_at
}

abstract_album ||--|{ abstract_album_link:abstract_album_id
abstract_album ||--o| abstract_album_note:abstract_album_id
album ||--|| abstract_album_link:album_id
album ||--o| album_note:album_id

abstract_event ||--|{ abstract_event_link:abstract_event_id
abstract_event ||--o| abstract_event_note:abstract_event_id
live_event ||--|| abstract_event_link:event_id
live_event ||--|{ set_list:event_id
live_event ||--o| live_event_note:event_id

set_list |o--o| song:song_id
set_list ||--|{ set_list_note:set_list_id

media ||--|{ album:media_id
album ||--|{ track:album_id
song ||--|{ track:song_id 
song ||--o| song_note:song_id
track ||--o| track_note:song_id

song ||--|{ roleOnSong:song_id
album ||--|{ roleOnAlbum:album_id

person ||--|{roleOnSong:person_id
person ||--|{roleOnAlbum:person_id
role ||--|{ roleOnSong:role_id
role ||--|{ roleOnAlbum:role_id

organization ||--|{ label:organization_id
label ||--|{ album:label_id
```
