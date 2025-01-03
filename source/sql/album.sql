USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[album](
	[album_id] [uniqueidentifier] NOT NULL,
    [abustract_album_id] [uniqueidentifier] NOT NULL,
	[title] nvarchar(256) NOT NULL,
    [media_type] INT NOT NULL,
    [label_id] [uniqueidentifier],
	[created_at] [datetime] NOT NULL,
    [modified_at] [datetime] NOT NULL,
 CONSTRAINT [PK_album] PRIMARY KEY CLUSTERED 
(
	[album_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'アルバムID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'album',
 @level2type=N'COLUMN', @level2name=N'album_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'抽象アルバムID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'album',
 @level2type=N'COLUMN', @level2name=N'abustract_album_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'タイトル' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'album',
 @level2type=N'COLUMN', @level2name=N'title'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'メディア種別' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'album',
 @level2type=N'COLUMN', @level2name=N'media_type'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'レーベルID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'album',
 @level2type=N'COLUMN', @level2name=N'label_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'登録日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'album',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'更新日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'album',
 @level2type=N'COLUMN', @level2name=N'modified_at'
GO