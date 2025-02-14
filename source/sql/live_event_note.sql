USE [Fanatic_Serve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[live_event_note];
GO

CREATE TABLE [dbo].[live_event_note](
	[live_event_id] [uniqueidentifier] NOT NULL,
	[note] [nvarchar](max) NULL,
	[created_at] [datetime] NOT NULL,
	[modified_at] [datetime] NOT NULL,
     CONSTRAINT [PK_live_event_note] PRIMARY KEY CLUSTERED 
(
	[live_event_id] ASC
)
);

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'ライブイベントID' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'live_event_note',
 @level2type=N'COLUMN', @level2name=N'live_event_id'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'ノート' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'live_event_note',
 @level2type=N'COLUMN', @level2name=N'note'
GO
EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'登録日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'live_event_note',
 @level2type=N'COLUMN', @level2name=N'created_at'
GO

EXEC sys.sp_addextendedproperty
 @name=N'MS_Description', @value=N'更新日時' ,
 @level0type=N'SCHEMA', @level0name=N'dbo',
 @level1type=N'TABLE', @level1name=N'live_event_note',
 @level2type=N'COLUMN', @level2name=N'modified_at'
GO