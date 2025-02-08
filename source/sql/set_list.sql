USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[set_list];
GO

CREATE TABLE [dbo].[set_list](
	[set_list_id] [uniqueidentifier] NOT NULL,
    [live_event_id] [uniqueidentifier] NOT NULL,
	[set_list_no] [int] NOT NULL,
	[title] [nvarchar](256) NOT NULL,
	[song_id] [uniqueidentifier] NULL,
	[created_at] [datetime] NOT NULL,
    [modified_at] [datetime] NOT NULL,
 CONSTRAINT [PK_set_list] PRIMARY KEY CLUSTERED 
(
	[set_list_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'セットリストID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list',
 @level2type=N'COLUMN', @level2name=N'set_list_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'イベントID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list',
 @level2type=N'COLUMN', @level2name=N'live_event_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'曲順' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list',
 @level2type=N'COLUMN', @level2name=N'set_list_no'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'タイトル' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list',
 @level2type=N'COLUMN', @level2name=N'title'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'楽曲ID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list',
 @level2type=N'COLUMN', @level2name=N'song_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'登録日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'更新日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'set_list',
 @level2type=N'COLUMN', @level2name=N'modified_at'
GO
